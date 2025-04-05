using System.Collections.Generic;
using Southwest_Airlines.Models;
using System.Linq;
namespace Southwest_Airlines.Services
{
    public class UserListService
    {
        private readonly List<Registration> _userList = new List<Registration>();
        public void AddUser(Registration user)
        {
            _userList.Add(user);
        }

        public List<Registration> GetUsers()
        {
            return _userList;

        }
        public Registration? GetUserByName(string name)
        {
            return _userList.FirstOrDefault(user => user.TBuser == name);
        }

        public bool VerifyPassword(string username, string password)
        {
            var user = GetUserByName(username);
            if (user != null)
            {
                return user.TBpass == password;
            }
            return false;
        }
    }
}
