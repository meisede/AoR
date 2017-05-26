using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ftpg.AoR.Entity;
using ftpg.Aor.Engine;

namespace ftpg.AoR.Web.Game
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                var game = new Entity.Game(Request.Form["tbName"], Convert.ToInt32(Request.Form["selNoPlayers"]));
                switch (Request.Form["selGameType"])
                {
                    case "Training":
                        game.GameType = Enums.GameType.Training;
                        break;
                    case "Casual":
                        game.GameType = Enums.GameType.Relaxed;
                        break;
                    default:
                        game.GameType = Enums.GameType.Competetive;
                        break;
                }
                Storage.StoreGame(game, "Common");
                SessionHelper.Game = game;
            }
        }
    }
}