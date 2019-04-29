using db.users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.repositories
{
    public class AuthRepository : IAuthRepository
    {
        public string login(string userID, string password, string token)
        {
            var userDB = new Users();
            string user = userDB.Login(userID, password);
            if(!String.IsNullOrEmpty(user))
            {
                userDB.SetToken(userID, token);
            }
            return user;
        }

        public void signup(string userID, string password)
        {
            var userDB = new Users();
            userDB.Signup(userID, password);
        }
    }

    public interface IAuthRepository : IRepository
    {
        string login(string userID, string password, string token);
        void signup(string userID, string password);
    }
}
