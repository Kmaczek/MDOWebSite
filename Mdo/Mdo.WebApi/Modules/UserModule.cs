using System;
using System.Collections.Generic;
using Mdo.WebApi.Models;
using Mdo.WebApi.Repos;
using Mdo.WebApi.Repos.Mocks;
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
        public UserModule()
        {
            Initialize();

            Post["/login"] = o =>
            {
                var user = this.Request.Form.UserName;
                var pass = this.Request.Form.Password;

                var isLoginSuccessfull = userRepo.Login(user, pass);

                if (isLoginSuccessfull)
                {
                    var token = new Token();
                    TokenStore.AddToken(token);

                    return Response.AsJson(new LoginModel()
                    {
                        Token = token.CreateBearerToken()
                    },
                        HttpStatusCode.Accepted);
                }

                return Response.AsJson(new ResponseMessage()
                {
                    Message = "Authentication Failed"
                }, HttpStatusCode.Unauthorized);
            };

            Post["/userSettings"] = o =>
            {
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

        private void Initialize()
        {
            userRepo = new MockedUserRepository();
            tokens = new List<CsrfToken>();

            //this.RequiresHttps();

        }
    }
}