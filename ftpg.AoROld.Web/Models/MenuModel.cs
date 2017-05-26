using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ftpg.AoR.Web.Models
{
    public class MenuModel
    {
        public List<string> Games { get; set; }
        public string Test { get; set; }
        public string LoggedInPlayer { get; set; }
    }
}