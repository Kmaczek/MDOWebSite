using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mdo.DB.Entities;
using Mdo.Models;

namespace Mdo.Persistence.Adapters
{
    public class UserAdapter
    {
        private UserModel _userModel;
        private UserEntity _userEntity;

        public UserAdapter(UserModel userModel)
        {
            _userModel = userModel;
            _userEntity = CreateEntity();
        }

        public UserAdapter(UserEntity userEntity)
        {
            _userEntity = userEntity;
            _userModel = CreateModel();
        }

        public UserModel UserModel
        {
            get { return _userModel; }
        }

        public UserEntity UserEntity
        {
            get { return _userEntity; }
        }

        private UserModel CreateModel()
        {
            if (_userEntity == null)
            {
                return null;
            }

            IEnumerable<string> roles = new List<string>();
            if (_userEntity.Roles != null)
            {
                roles = _userEntity.Roles.Select(x => x.Name);
            }
            
            var userModel = new UserModel(
                _userEntity.Username,
                _userEntity.Email,
                _userEntity.Password,
                _userEntity.Secret,
                roles);

            return userModel;
        }

        private UserEntity CreateEntity()
        {
            if (_userModel == null)
            {
                return null;
            }

            var userEntity = new UserEntity()
            {
                Username = _userModel.Username,
                Email = _userModel.Email,
                Password = _userModel.Password,
                Secret = _userModel.Secret,
                Roles = new List<RoleEntity>()
            };

            foreach (var role in _userModel.Roles)
            {
                userEntity.Roles.Add(new RoleEntity() { Name = role });
            }

            return userEntity;
        }
    }
}
