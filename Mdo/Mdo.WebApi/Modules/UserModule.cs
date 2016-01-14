using System;
using System.Collections.Generic;
using Mdo.Core;
using Mdo.Models;
using Mdo.Models.Dtos;
using Mdo.Persistence.Repositories.Interfaces;
using Mdo.WebApi.Security;
using Nancy;
using Nancy.Extensions;
using Nancy.ModelBinding;
using Nancy.Security;

namespace Mdo.WebApi.Modules
{
    public class UserModule : NancyModule
    {
        private readonly IUserRepository userRepository;
        private List<CsrfToken> tokens;
        public UserModule(IUserRepository userRepo) : base("/user")
        {
            userRepository = userRepo;
            Initialize();

            LoginUser();
            RegisterUser();

            
            Get["/{username}"] = o =>
            {
                return Response.AsJson(new UserDto() { Rank = "rookie", Username = o.username });
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

                return Response.AsJson(new ResponseMessage() { Message = "Not Authenticated" }, HttpStatusCode.Unauthorized);
            };
        }

        private void LoginUser()
        {
            Post["/login"] = o =>
            {
                var model = this.Bind<LoginDto>();

                var user = userRepository.GetUser(model.UsernameOrEmail);
                if (user == null)
                {
                    return LoginFailResponse();
                }

                try
                {
                    if (MdoSecurity.CheckPassword(model.Password, user.Password))
                    {
                        return Response.AsJson(new
                        {
                            Message = "Login successful",
                            user.Username
                        });
                    }
                }
                catch (Exception)
                {
//                    LogMessage
                    throw;
                }

                return LoginFailResponse();
            };
        }

        private Response LoginFailResponse()
        {
            return Response.AsJson(new 
            {
                Message = "Incorrect login, e-mail or password"
            }, HttpStatusCode.Unauthorized);
        }

        private void RegisterUser()
        {
            Post["/register"] = o =>
            {
                var model = this.Bind<UserRegistrationDto>();

                if (model != null)
                {
                    var usernameExists = userRepository.GetByName(model.Username);
                    if (usernameExists != null)
                    {
                        return ReturnInvalidInput("Username already exists");
                    }

                    var emailUsed = userRepository.GetByEmail(model.Email);
                    if (emailUsed != null)
                    {
                        return ReturnInvalidInput("Provided email is already used");
                    }

                    if (!MdoSecurity.IsPsswordValid(model.Password))
                    {
                        return ReturnInvalidInput("Password is to short");
                    }

                    var user = new UserModel(model);
                    userRepository.CreateUser(user.ToUser());

                    return Response.AsJson(new ResponseMessage() { Message = "Registration Successfull" });
                }

                return Response.AsJson(new ResponseMessage() { Message = "Cannot process passed data. Most likely invalid format" }, HttpStatusCode.BadRequest);
            };
        }

        private Response ReturnInvalidInput(string message)
        {
            return Response.AsJson(new ResponseMessage() { Message = message }, HttpStatusCode.BadRequest);
        }

        private void Initialize()
        {
//            userRepository = new UserRepository();
            tokens = new List<CsrfToken>();

            //this.RequiresHttps();
        }
    }
}