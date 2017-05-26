using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ftpg.Aor.Engine;
using ftpg.AoR.Entity;

namespace ftpg.AoR.Web.Models
{
    public class AoRModel
    {
        public List<string> Games { get; set; }
        public User User { get; set; }

        public static List<string> GetGames()
        {
            return Storage.GetGames("Common");
        }
    }
}