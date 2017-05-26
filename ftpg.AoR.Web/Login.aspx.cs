using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ftpg.AoR.Entity;

namespace ftpg.AoR.Web
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var user = new User { Nick = Request.QueryString["id"] };
            SessionHelper.User = user;
        }
    }
}