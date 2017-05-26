using System;
using System.Collections.Generic;
using System.Web.Mvc;

using ftpg.Aor.Engine;
using ftpg.AoR.Entity;
using ftpg.AoR.Web.Models;
using Neo4jClient;

namespace ftpg.AoR.Web.Controllers
{
    public class GameController : Controller
    {
        //
        // GET: /Game/

        public ActionResult Lookup(string id)
        {
            var game = SessionHelper.Game;

            Storage.StoreGame(game, "Common");
            var gameModel = GetGameModel(SessionHelper.Game, SessionHelper.User);
            gameModel.ViewAction = id;
            return View("Play", gameModel);
        }
        public ActionResult Neo()
        {
            try
            {
                //var client = new GraphClient(new Uri("http://52.17.52.5:4747//db/data"));
                var client = new GraphClient(new Uri("http://neo4j:myneo@52.17.52.5:7474/db/data"));
                client.Connect();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return View();
        }

        [HttpPost]
        public ActionResult War(List<string> doms)
        {
            var game = SessionHelper.Game;
            var player = PlayerInSession();
            Storage.StoreGame(game, "Common");
            var gameModel = GetGameModel(SessionHelper.Game, SessionHelper.User);
            return View("Play", gameModel);
        }

        [HttpPost]
        public ActionResult ChangeOrder(int id)
        {
            var game = SessionHelper.Game;
            game.EnligthenmentChangeOrder(id);

            Storage.StoreGame(game, "Common");
            var gameModel = GetGameModel(SessionHelper.Game, SessionHelper.User);
            return View("Play", gameModel);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Save(FormCollection form)
        {
            var id = Storage.SaveFile(form["Game"], "Common");
            var gameFile = Storage.GetFile(id, "Common");
            ViewData["gameFile"] = System.Xml.Linq.XDocument.Parse(gameFile).ToString();
            ViewData["id"] = id;
            return View("Edit");
        }

        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                RedirectToAction("Index");
            }
            Storage.DeleteGame(id, "Common");
            return RedirectToAction("Play");
        }

        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                RedirectToAction("Index");
            }
            var gameFile = Storage.GetFile(id, "Common");
            ViewData["gameFile"] = System.Xml.Linq.XDocument.Parse(gameFile).ToString();
            ViewData["id"] = id;
            return View();
        }

        public ActionResult Overview()
        {
            var game = SessionHelper.Game;
            var player = PlayerInSession();
            if (game == null || player == null)
            {
                return RedirectToAction("Index", "AoR");
            }
            var gameModel = GetGameModel(SessionHelper.Game, SessionHelper.User);
            return View("Overview", gameModel);
        }

        public ActionResult DetermineOrderofPlay(int tokens)
        {
            var game = SessionHelper.Game;
            var player = PlayerInSession();
            game.DeterminePlayOrder(player, tokens);
            Storage.StoreGame(game, "Common");
            var gameModel = GetGameModel(SessionHelper.Game, SessionHelper.User);
            return View("Play", gameModel);
        }

        public ActionResult OrderOfPlay(FormCollection form)
        {
            var game = SessionHelper.Game;
            var player = PlayerInSession();
            var fistBid = form["selFistBid"];
            game.SetPlayerOrderOfPlay(player.Capital, Convert.ToInt32(fistBid));
            Storage.StoreGame(game, "Common");
            var gameModel = GetGameModel(SessionHelper.Game, SessionHelper.User);
            return View("Play", gameModel);
        }

        [HttpPost]
        public ActionResult Expand(Map map)
        {
            var game = SessionHelper.Game;
            var player = PlayerInSession();
            game.Expand(map);

            Storage.StoreGame(game, "Common");
            var gameModel = GetGameModel(SessionHelper.Game, SessionHelper.User);
            //return Json(gameModel);
            return View("Map", gameModel);
        }

        //[HttpPost]
        //public ActionResult Expand( string map)
        //{
        //    var game = SessionHelper.Game;
        //    var player = PlayerInSession();
        //    //game.Expand(map);

        //    //Storage.StoreGame(game, "Common");
        //    var gameModel = GetGameModel(SessionHelper.Game, SessionHelper.User);
        //    return View("Map");
        //}

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Play(string id)
        {
            if (id == null)
            {
                RedirectToAction("Index");
            }
            var game = GetGame(id);
            SessionHelper.Game = game;
            var gameModel = GetGameModel(game, SessionHelper.User);
            var action = game.Phase == Enums.GamePhase.Expansion ? "Map" : "Play";
            return View("Play", gameModel);
        }

        public ActionResult Map(string id)
        {
            var game = SessionHelper.Game;
            if (game.Name != id)
            {
                game = GetGame(id);
            }
            var player = PlayerInSession();

            var gameModel = GetGameModel(game, SessionHelper.User);
            return View("Map", GetGameModel(game, SessionHelper.User));
        }

        //public ActionResult Map()
        //{
        //    var game = SessionHelper.Game;
        //    return View("Map", GetGameModel(game, SessionHelper.User));
        //}

        [HttpPost]
        public ActionResult CardInterruptResolve(string card, string resolve)
        {
            var game = SessionHelper.Game;
            var player = PlayerInSession();
            game.ResolveInterrupt(card, resolve);
            return View("Play", GetGameModel(game, SessionHelper.User));
        }

        [HttpPost]
        public ActionResult Interupt(string id)
        {
            var game = SessionHelper.Game;
            var player = PlayerInSession();
            game.ResolveInterrupt(id);
            return View("Play", GetGameModel(game, SessionHelper.User));
        }

        [HttpPost]
        public ActionResult Purchase(PurchaseModel purchaseModel)
        {
            var game = SessionHelper.Game;
            var player = PlayerInSession();
            var purchase = Mapping.PurchaseModelToPurchase(purchaseModel);
            game.Purchase(player, purchase);
            Storage.StoreGame(game, "Common");
            return Json(new { result = "Redirect", url = Url.Action("Play", "Game") });
        }


        [HttpPost]
        public ActionResult PlayCard(FormCollection form)
        {
            var game = SessionHelper.Game;
            var player = PlayerInSession();
            if (game.Phase == Enums.GamePhase.PlayCard)
            {
                var cardName = form["selCard"];
                var card = game.CardHandWithDualGoodCardsSplitted(player.Cards).Find(m => m.Name == cardName);
                var fee = Convert.ToInt32(form["tbFee"]);
                var capitalString = form["selCard"];
                var effect = string.Empty;
                switch (cardName)
                {
                    case "Alchemist's Gold":
                        effect = form["selAlchemistGold"];
                        break;
                    case "Black Death":
                        effect = form["selBlackDeath"];
                        break;
                    case "Civil War":
                        effect = form["selCivilWar"];
                        break;
                    case "War!":
                        effect = form["selWar"];
                        break;
                    case "Pirates/Vikings":
                        effect = form["selPiratesVikings"];
                        break;
                    case "Papal Decree":
                        effect = form["selAdvanceGroup"];
                        break;
                    case "The Crusades":
                        effect = form["selTheCrusades"];
                        break;
                }

                game.PlayCard(card, fee, effect);
              
            }
            Storage.StoreGame(game, "Common");
            SessionHelper.Game = game;

            return View("Play", GetGameModel(game, SessionHelper.User));
        }

        [HttpPost]
        public ActionResult EndCardPlay(FormCollection form)
        {
            var game = SessionHelper.Game;
            var player = PlayerInSession();
            if (game.Phase == Enums.GamePhase.PlayCard)
            {
                game.PlayCard(null, 0, string.Empty);
            }
            Storage.StoreGame(game,"Common");
            SessionHelper.Game = game;

            return View("Play", GetGameModel(game, SessionHelper.User));
        }

        [HttpPost]
        public ActionResult BuyCard(bool drawCard)
        {
            var game = SessionHelper.Game;
            var player = PlayerInSession();
            if (game.Phase == Enums.GamePhase.BuyCard && player.PlayOrder == game.PlayOrder)
            {
                if (drawCard)
                {
                    game.BuyCard();
                }
            }
            Storage.StoreGame(game,"Common");
            SessionHelper.Game = game;

            return View("Play", GetGameModel(game, SessionHelper.User));
        }

        public ActionResult BuyCardExpand()
        {
            var game = SessionHelper.Game;
            var player = PlayerInSession();
            if (game.Phase == Enums.GamePhase.Expansion && player.PlayOrder == game.PlayOrder)
            {
                game.BuyCardExpand();
            }
            Storage.StoreGame(game, "Common");
            SessionHelper.Game = game;

            return View("Play", GetGameModel(game, SessionHelper.User));
        }

        [HttpPost]
        public ActionResult RemoveShortageSurplus(int id)
        {
            var game = SessionHelper.Game;
            var player = PlayerInSession();
            if (player.PlayOrder == game.PlayOrder)
            {
                game.BuyRemovalSurplusShortage(id);
            }
            Storage.StoreGame(game,"Common");
            SessionHelper.Game = game;

            return View("Play", GetGameModel(game, SessionHelper.User));
        }

        [HttpPost]
        public ActionResult AddShortageSurplus(FormCollection form)
        {
            var game = SessionHelper.Game;
            var player = PlayerInSession();
            if (player.PlayOrder == game.PlayOrder)
            {
                var shortage = form["selShortageAdd"];
                var surplus = form["selSurplusAdd"];
                var good = shortage == string.Empty ?  surplus : shortage;
                var isShortage = (surplus == string.Empty);
                game.AddSurplusShortage(good, isShortage);
            }
            Storage.StoreGame(game, "Common");
            SessionHelper.Game = game;

            return View("Play", GetGameModel(game, SessionHelper.User));

        }

        [HttpPost]
        public ActionResult DiscardCard(FormCollection form)
        {
            var game = SessionHelper.Game;
            var player = PlayerInSession();

            var card1 = form["selCard1"];
            if (player.HasCardOnHand(card1))
            {
                game.DiscardCard(player, player.Cards.Find(m=>m.Name == card1), false);
            }
            var card2 = form["selCard2"];
            if (player.HasCardOnHand(card2))
            {
                game.DiscardCard(player, player.Cards.Find(m => m.Name == card2), false);
            }
            game.ResolveDiscard();
            
            Storage.StoreGame(game, "Common");
            SessionHelper.Game = game;

            return View("Play", GetGameModel(game, SessionHelper.User));

        }

        [HttpPost]
        public ActionResult DiscardInitialCard(string card)
        {
            var game = SessionHelper.Game;
            var player = PlayerInSession();
            game.DiscardInitialCard(player, player.Cards.Find(m => m.Name == card));
            Storage.StoreGame(game, "Common");
            SessionHelper.Game = game;
            return View("Play", GetGameModel(game, SessionHelper.User));

        }

        public ActionResult SelectCapital(Enums.Capital capital)
        {
            var game = SessionHelper.Game;
            if (game == null)
            {
                RedirectToAction("Index", "AoR", new {area = ""});
            }
            else
            {
                var player = PlayerInSession();
                if (game.Phase == Enums.GamePhase.SelectCapital && game.PlayerInTurn == player)
                {
                    game.PayAndSelectCapital(player, capital);
                }
            }
            Storage.StoreGame(game,"Common");
            SessionHelper.Game = game;
            return View("Play", GetGameModel(game, SessionHelper.User));
        }


        [HttpPost]
        public ActionResult CapitalBid(int bid)
        {
            var game = SessionHelper.Game;
            var player = PlayerInSession();
            game.MakeCapitalBid(player, bid);

            Storage.StoreGame(game,"Common");
            SessionHelper.Game = game;
            return View("Play", GetGameModel(game, SessionHelper.User));
        }

        [HttpPost]
        public ActionResult PurchaseTokens(int tokens)
        {
            var game = SessionHelper.Game;
            var player = PlayerInSession();
            game.TokenPurchase(player, tokens);

            Storage.StoreGame(game, "Common");
            SessionHelper.Game = game;
            return View("Play", GetGameModel(game, SessionHelper.User));
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection form)
        {
            var game = new Game(form["tbName"], Convert.ToInt32(form["selNoPlayers"]));
            switch (form["selGameType"])
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
            Storage.StoreGame(game,"Common");
            SessionHelper.Game = game;
            return RedirectToAction("Play", "Game");
        }
        private static Game GetGame(string name)
        {
            var game = Storage.GetGame(name,"Common");
            if (game.Name == null)
            {
                return SessionHelper.Game;
            }
            SessionHelper.Game = game;
            return game;
        }
        private static GameModel GetGameModel(Game game, User user)
        {
            var gameModel = Mapping.GameToGameModel(game, user);
            gameModel.Menu = new MenuModel { Games = Storage.GetGames("Common") };
            
            gameModel.Menu.LoggedInPlayer = SessionHelper.User.Nick;
            return gameModel;
        }

        private static Player PlayerInSession()
        {
            if (SessionHelper.User == null || SessionHelper.Game == null)
            {
                return null;
            }
            return SessionHelper.Game.Players.Find(m => m.Nick == SessionHelper.User.Nick);
            
        }
    }
}
