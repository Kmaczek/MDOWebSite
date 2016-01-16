using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Mdo.Core;
using Mdo.Persistence.Entities;
using Nancy.Json;

namespace PersistenceMocks
{
    public static class UserWarehouse
    {
        private static readonly string directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Mdo");
        private static readonly string filePath = Path.Combine(directoryPath, "UsersStore.json");
        private static bool directoryExists;
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

        private static void CreateDirectoryIfDoesntExist()
        {
            if (!directoryExists)
            {
                directoryExists = Directory.Exists(directoryPath);

                if (!directoryExists)
                    Directory.CreateDirectory(directoryPath);
            }
        }

        public static void Save()
        {
            CreateDirectoryIfDoesntExist();
            var jUsers = new JavaScriptSerializer().Serialize(Users);
            File.WriteAllText(filePath, jUsers);
        }

        private static void Load()
        {
            CreateDirectoryIfDoesntExist();
            var jUsers = File.ReadAllText(filePath);
            Users.Clear();
            Users.AddRange(new JavaScriptSerializer().Deserialize<User[]>(jUsers));
        }

        public static User GetStandardUser()
        {
            Load();
            return Users[0];
        }

        public static User GetUser(int id)
        {
            Load();
            return Users.Find(u => u.Id == id);
        }

        public static User GetUser(Predicate<User> predicate)
        {
            Load();
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
            Save();
        }

        public static void CoppyUserData(User fromUser, User toUser)
        {
            toUser.Id = fromUser.Id;
            toUser.Email = fromUser.Email;
            toUser.Password = fromUser.Password;
            toUser.Username = fromUser.Username;
        }

        public static User GenerateUser(string common)
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
