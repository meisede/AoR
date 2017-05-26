using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ftpg.AoR.Entity;

namespace ftpg.AoR.Web
{
    public class SessionHelper
    {
        public static Game Game
        {
            get { return (Game)HttpContext.Current.Session["Game"]; }
            set { HttpContext.Current.Session["Game"] = value; }
        }
        public static User User
        {
            get { return (User)HttpContext.Current.Session["User"]; }
            set { HttpContext.Current.Session["User"] = value; }
        }
        public static string StoragePath
        {
            get { return (string)HttpContext.Current.Session["StoragePath"]; }
            set { HttpContext.Current.Session["StoragePath"] = value; }
        }
        public static string CardInPlay
        {
            get { return (string)HttpContext.Current.Session["CardInPlay"]; }
            set { HttpContext.Current.Session["CardInPlay"] = value; }
        }
    }
}