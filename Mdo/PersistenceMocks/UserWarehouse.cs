using System;
using System.Collections.Generic;
using System.Linq;
using Mdo.Core;
using Mdo.Persistence.Domain;

namespace PersistenceMocks
{
    public static class UserWarehouse
    {
        public static readonly string StdPassword = "stdPass";
        public static readonly string StdUsername = "stdUsername";
        public static readonly string StdEmail = "std@email.com";
        public static readonly int StdId = 1;

        public static readonly string FakeUsername = "fakeUsername";
        public static readonly string FakePassword = "fakePass";

        public static List<User> Users = new List<User>();

        static UserWarehouse()
        {
            var passwordToStore = MdoSecurity.CreateHashedPassword(StdPassword);
            Users.Add(new User()
            {
                Email = StdEmail,
                Username = StdUsername,
                Id = StdId,
                Password = passwordToStore
            });
        }

        public static User GetStandardUser()
        {
            return Users[0];
        }

        public static User GetUser(int id)
        {
            return Users.Find(u => u.Id == id);
        }

        public static User GetUser(Predicate<User> predicate)
        {
            return Users.Find(predicate);
        }

        public static void AddOrUpdate(User user)
        {
            if (Users.Any(u => u.Id == user.Id))
            {
                var storedUser = Users.Find(u => u.Id == user.Id);
                CoppyUserData(user, storedUser);

                return;
            }

            Users.Add(user);
        }

        public static void CoppyUserData(User fromUser, User toUser)
        {
            toUser.Id = fromUser.Id;
            toUser.Email = fromUser.Email;
            toUser.Password = fromUser.Password;
            toUser.Username = fromUser.Username;
        }
    }
}
