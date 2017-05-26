using System.Web.Mvc;
using ftpg.Aor.Engine;
using ftpg.AoR.Entity;
using ftpg.AoR.Web.Models;

namespace ftpg.AoR.Web.Controllers
{
    public class AoRController : Controller
    {
        public ActionResult Index()
        {
            var aorModel = new AoRModel { Games = Storage.GetGames("Common"), User = SessionHelper.User };
            return View(aorModel);
        }
        public ActionResult Login(string id)
        {
            var user = new User() { Nick = id };
            SessionHelper.User = user;
            if (SessionHelper.Game != null)
            {
                return RedirectToAction("Play", "Game", new { area = "" });
            }
            var aorModel = new AoRModel { Games = Storage.GetGames("Common"), User = SessionHelper.User };
            return View("Index", aorModel);
        }

        [HttpPost]
        public ActionResult StoragePath(FormCollection form)
        {
            SessionHelper.StoragePath = form["StoragePath"];
            var aorModel = new AoRModel { Games = Storage.GetGames("Common"), User = SessionHelper.User };
            return View("Index", aorModel);
        }
    }
}
