using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AoR.Entity;
using AoR.Entity.Interface;

namespace AoRBL
{
    public class UserAccess
    {
        public static IUser CreateUser(string nick, string password, string name, string email)
        {
            var user = new User();
            user.Nick = nick;
            user.Password = password;
            user.Name = name;
            user.Email = email;

            var ret = UserRepository.Create(user);
            return user;
        }

        public List<IUser> GetUserList()
        {
            return null;
        }

    }
}
