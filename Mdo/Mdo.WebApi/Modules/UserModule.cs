using System;
using System.Collections.Generic;
using Mdo.Core;
using Mdo.Persistence.Domain;
using Mdo.Persistence.Repositories;
using Mdo.Persistence.Repositories.Interfaces;
using Mdo.WebApi.Dtos;
using Mdo.WebApi.Security;
using Nancy;
using Nancy.Extensions;
using Nancy.ModelBinding;
using Nancy.Security;

namespace Mdo.WebApi.Modules
{
    public class UserModule : NancyModule
    {
        private IUserRepository userRepository;
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
                        return Response.AsJson(new ResponseMessage() { Message = "Username already exists" }, HttpStatusCode.BadRequest);
                    }

                    var emailUsed = userRepository.GetByEmail(model.Email);
                    if (emailUsed != null)
                    {
                        return Response.AsJson(new ResponseMessage() { Message = "Provided email is already used" }, HttpStatusCode.BadRequest);
                    }

                    var passwordToStore = MdoSecurity.CreateHashedPassword(model.Password);
                    var user = new User()
                    {
                        Username = model.Username,
                        Password = passwordToStore,
                        Email = model.Email
                    };

                    userRepository.CreateUser(user);

                    return Response.AsJson(new ResponseMessage() { Message = "Registration Successfull" });
                }

                return Response.AsJson(new ResponseMessage() { Message = "Cannot bind data from body" }, HttpStatusCode.BadRequest);
            };
        }

        private void Initialize()
        {
//            userRepository = new UserRepository();
            tokens = new List<CsrfToken>();

            //this.RequiresHttps();
        }
    }
}