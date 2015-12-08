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
        private IUserRepository userRepo;
        private List<CsrfToken> tokens;
        public UserModule() : base("/user")
        {
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
                        UserName = "Kmaczek"
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

                var user = userRepo.GetUser(model.UsernameOrEmail);
                if (user == null)
                {
                    return Response.AsJson(new ResponseMessage
                    {
                        Message = "No user with this name or email."
                    }, HttpStatusCode.NotFound);
                }

                try
                {
                    if (MdoSecurity.CheckPassword(model.Password, user.Password))
                    {
                        return Response.AsJson(new
                        {
                            Message = "Login succesful",
                            user.Username
                        });
                    }
                }
                catch (Exception)
                {
//                    LogMessage
                    throw;
                }

                return Response.AsJson(new ResponseMessage()
                {
                    Message = "Authentication Failed"
                }, HttpStatusCode.Unauthorized);
            };
        }

        private void RegisterUser()
        {
            Post["/register"] = o =>
            {
                var model = this.Bind<UserRegistrationDto>();

                if (model != null)
                {
                    var usernameExists = userRepo.GetByName(model.Username);
                    if (usernameExists != null)
                    {
                        return Response.AsJson(new ResponseMessage() { Message = "Username already exists" }, HttpStatusCode.BadRequest);
                    }

                    var emailUsed = userRepo.GetByEmail(model.Email);
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

                    userRepo.CreateUser(user);

                    return Response.AsJson(new ResponseMessage() { Message = "Registration Successfull" });
                }

                return Response.AsJson(new ResponseMessage() { Message = "Cannot bind data from body" }, HttpStatusCode.BadRequest);
            };
        }

        private void Initialize()
        {
            userRepo = new UserRepository();
            tokens = new List<CsrfToken>();

            //this.RequiresHttps();
        }

        
    }
}