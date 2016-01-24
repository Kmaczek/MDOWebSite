using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Channels.Tcp;
using Mdo.Core;
using Mdo.Persistence.Entities;

namespace PersistenceMocks
{
    [Serializable]
    public class UserWarehouse : MarshalByRefObject
    {
        public static readonly string StdPassword = "stdPassword";
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

        public static UserWarehouse GetInstance()
        {
            //            TcpChannel channel = new TcpChannel();
            //            ChannelServices.RegisterChannel(channel, false);
            HttpChannel httpChannel = new HttpChannel();
            ChannelServices.RegisterChannel(httpChannel, false);

            return (UserWarehouse)Activator.GetObject(typeof(UserWarehouse), "http://localhost:5000/UserWarehouse");
        }

        private void PopulateUsers()
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

        public void ResetUsers()
        {
            Users.Clear();
            PopulateUsers();
        }

        public User GetStandardUser()
        {
            return Users[0];
        }

        public User GetUser(int id)
        {
            return Users.Find(u => u.Id == id);
        }

        public User GetUser(Predicate<User> predicate)
        {
            return Users.Find(predicate);
        }

        public User GetUserByName(string username)
        {
            return Users.Find(x => x.Username == username);
        }

        public User GetUserByEmail(string email)
        {
            return Users.Find(x => x.Email == email);
        }

        public void AddOrUpdate(User user)
        {
            if (Users.Any(u => u.Id == user.Id))
            {
                var storedUser = Users.Find(u => u.Id == user.Id);
                CopyUserData(user, storedUser);

                return;
            }

            Users.Add(user);
        }

        public void CopyUserData(User fromUser, User toUser)
        {
            toUser.Id = fromUser.Id;
            toUser.Email = fromUser.Email;
            toUser.Password = fromUser.Password;
            toUser.Username = fromUser.Username;
        }

        public User GenerateUser(string common)
        {
            return new User()
            {
                Email = common+"@email.com",
                Password = "qqqqqqqq".Insert(0, common),
                Username = "uuu".Insert(0, common)
            };
        }
    }
}
