using System.Collections.Generic;
using System.Linq;

namespace Mdo.WebApi.Repos.Mocks
{
    public class MockedUserRepository: IUserRepository
    {
        private List<User> Users = new List<User>();

        public MockedUserRepository()
        {
            Users.Add(new User("damis", "kot"));
            Users.Add(new User("a", "a"));
        }

        public bool Login(string user, string password)
        {
            bool successfullLogin = Users.Any(x => x.Name == user && x.Pass == password);

            return successfullLogin;
        }

        private class User
        {
            public User(string name, string pass)
            {
                Name = name;
                Pass = pass;
            }

            public string Name { get; set; }
            public string Pass { get; set; }
        }
    }
}