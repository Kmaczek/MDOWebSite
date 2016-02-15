using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using Mdo.Core;
using Mdo.Models;
using Mdo.Models.Dtos;
using Mdo.Persistence.Interfaces;
using Mdo.WebApi.Security;
using Nancy;
using Nancy.Extensions;
using Nancy.ModelBinding;
using Nancy.Security;
using NLog;

namespace Mdo.WebApi.Modules
{
    public class UserModule : NancyModule
    {
        private readonly IUserRepository userRepository;
        private List<CsrfToken> tokens;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public UserModule(IUserRepository userRepo) : base("/user")
        {
            userRepository = userRepo;
            Initialize();

            LoginUser();
            RegisterUser();


            Get["/{username}"] = o =>
            {
                logger.Info("GET /user/{0} invoked", o.username);
                var user = userRepository.GetByName(o.username);
                if (user == null)
                {
                    logger.Debug("user {0} not found", o.username);
                    return Response.AsJson(new { Message = String.Format("Resource [{0}] was not found", o.username) }, HttpStatusCode.NotFound);
                }
                return Response.AsJson(new UserDto { Username = o.username });
            };

            Get["/email/{email}"] = o =>
            {
                logger.Info("GET /email/{0} invoked", o.email);
                var email = userRepository.GetByEmail(o.email);
                if (email == null)
                {
                    logger.Debug("email {0} not found", o.email);
                    return Response.AsJson(new { Message = String.Format("Resource [{0}] was not found.", o.email) }, HttpStatusCode.NotFound);
                }
                return Response.AsJson(new { Email = o.email });
            };

            Post["/settings"] = o =>
            {
                this.RequiresAuthentication();

                var userToken = this.Bind<BearerToken>();
                var body = this.Request.Body.AsString();
                var tokenFind = TokenStore.FindMatching(userToken);

                if (tokenFind?.CreationTime.Add(tokenFind.TokenSpan) > DateTime.Now)
                {
                    return Response.AsJson(new UserSettings()
                    {
                        Email = "flosbox@gmail.com",
                        UserName = "DK"
                    });
                }

                return Response.AsJson(new ResponseMessage() { Message = "Not Authenticated." }, HttpStatusCode.Unauthorized);
            };
        }

        private void LoginUser()
        {
            Post["/login"] = o =>
            {
                try
                {
                    logger.Info("POST /login invoked");
                    var model = this.Bind<LoginDto>();

                    var user = userRepository.GetUser(model.UsernameOrEmail);
                    if (user == null)
                    {
                        logger.Debug("Cant find user/email {0}, login failed", model.UsernameOrEmail);
                        return LoginFailResponse();
                    }

                    if (MdoSecurity.CheckPassword(model.Password, user.Password))
                    {
                        return Response.AsJson(new
                        {
                            Message = "Login successful",
                            user.Username,
                            user.Secret,
                            user.Roles
                        });
                    }
                }
                catch (Exception e)
                {
                    logger.Error(e, "Login failed. Server error {0}", e.Message);
                    return LoginFailResponse();
                }

                return Response.AsJson("");
            };
        }

        private void RegisterUser()
        {
            Post["/register"] = o =>
            {
                logger.Info("POST /register invoked");
                var model = this.Bind<UserRegistrationDto>();

                if (model != null)
                {
                    try
                    {
                        var usernameExists = userRepository.GetByName(model.Username);
                        if (usernameExists != null)
                        {
                            logger.Debug("username {0} already exists.", model.Username);
                            return ReturnInvalidInput("Username already exists");
                        }

                        var emailUsed = userRepository.GetByEmail(model.Email);
                        if (emailUsed != null)
                        {
                            logger.Debug("email {0} already exists.", model.Email);
                            return ReturnInvalidInput("Provided email is already used");
                        }

                        if (!MdoSecurity.IsPsswordValid(model.Password))
                        {
                            logger.Debug("invalid password.");
                            return ReturnInvalidInput("Password is to short");
                        }

                        var user = new UserModel(model);
                        userRepository.CreateUser(user);
                    }
                    catch (Exception e)
                    {
                        logger.Error(e, "Could not add new user. Server error: {0}. ", e.Message);
                        return Response.AsJson(new ResponseMessage() { Message = "Could not add user" }, HttpStatusCode.InternalServerError);
                    }

                    return Response.AsJson(new ResponseMessage() { Message = "Registration Successfull" });
                }

                logger.Debug("problem with parsing data from request body.");
                return Response.AsJson(new ResponseMessage() { Message = "Cannot process passed data. Most likely invalid format" }, HttpStatusCode.BadRequest);
            };
        }

        private Response LoginFailResponse()
        {
            return Response.AsJson(new
            {
                Message = "Incorrect login, e-mail or password"
            }, HttpStatusCode.Unauthorized);
        }

        private Response ReturnInvalidInput(string message)
        {
            return Response.AsJson(new ResponseMessage() { Message = message }, HttpStatusCode.BadRequest);
        }

        private void Initialize()
        {
            tokens = new List<CsrfToken>();
            //this.RequiresHttps();
        }
    }
}