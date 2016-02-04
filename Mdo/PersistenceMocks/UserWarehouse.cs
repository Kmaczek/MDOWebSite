using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using Mdo.Core;
using Mdo.DB.Entities;

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

        public static List<UserEntity> Users = new List<UserEntity>();

        static UserWarehouse()
        {
            var passwordToStore = MdoSecurity.CreateHashedPassword(StdPassword);
            Users.Add(new UserEntity()
            {
                Email = StdEmail,
                Username = StdUsername,
                UserId = StdId,
                Password = passwordToStore
            });
        }

        public static UserWarehouse GetInstance()
        {
            var clientFormatter = new BinaryClientFormatterSinkProvider();
            HttpChannel httpChannel = new HttpChannel(null, clientFormatter, null);
            ChannelServices.RegisterChannel(httpChannel, false);

            return (UserWarehouse)Activator.GetObject(typeof(UserWarehouse), "http://localhost:5000/UserWarehouse");
        }

        private void PopulateUsers()
        {
            var passwordToStore = MdoSecurity.CreateHashedPassword(StdPassword);
            Users.Add(new UserEntity()
            {
                Email = StdEmail,
                Username = StdUsername,
                UserId = StdId,
                Password = passwordToStore
            });
        }

        public void ResetUsers()
        {
            Users.Clear();
            PopulateUsers();
        }

        public UserEntity[] GetAllUsers()
        {
            return Users.ToArray();
        }

        public UserEntity GetStandardUser()
        {
            return Users[0];
        }

        public UserEntity GetUser(int id)
        {
            return Users.Find(u => u.UserId == id);
        }

        public UserEntity GetUser(Predicate<UserEntity> predicate)
        {
            return Users.Find(predicate);
        }

        public UserEntity GetUserByName(string username)
        {
            return Users.Find(x => x.Username == username);
        }

        public UserEntity GetUserByEmail(string email)
        {
            return Users.Find(x => x.Email == email);
        }

        public void AddOrUpdate(UserEntity user)
        {
            if (Users.Any(u => u.UserId == user.UserId))
            {
                var storedUser = Users.Find(u => u.UserId == user.UserId);
                CopyUserData(user, storedUser);

                return;
            }

            Users.Add(user);
        }

        public void CopyUserData(UserEntity fromUser, UserEntity toUser)
        {
            toUser.UserId = fromUser.UserId;
            toUser.Email = fromUser.Email;
            toUser.Password = fromUser.Password;
            toUser.Username = fromUser.Username;
        }

        public UserEntity GenerateUser(string common)
        {
            return new UserEntity()
            {
                Email = common+"@email.com",
                Password = "qqqqqqqq".Insert(0, common),
                Username = "uuu".Insert(0, common)
            };
        }
    }
}
