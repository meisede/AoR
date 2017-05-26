using System;

namespace ftpg.AoR.Entity
{
    public class User 
    {
        public int UserId { get; set; }
        public string Password { get; set; }
        public string Nick { get; set; }
        public string Email { get; set; }
        public DateTime LatestLogin { get; set; }

        public void CreateUser(string nick, string password, string email)
        {

        }
    }
}
