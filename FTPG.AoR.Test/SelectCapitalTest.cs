using ftpg.AoR.Entity;
using FTPG.AoR;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FTPG.AoR.Test
{
    [TestClass]
    public class SelectCapitalTest
    {
        [TestMethod]
        public void PlaceBidTest()
        {
            var game = Util.GetGame(6);
            game.Phase = Enums.GamePhase.CapitalBid;
            var player1 = game.Players[0];
            game.MakeCapitalBid(player1, 0);
            Assert.AreEqual(0, player1.Bid);
            Assert.AreEqual(true, player1.DidAction);

            var player2 = game.Players[1];
            game.MakeCapitalBid(player2, 1);
            Assert.AreEqual(1, player2.Bid);

            var player3 = game.Players[2];
            game.MakeCapitalBid(player3, 5);

            var player4 = game.Players[3];
            game.MakeCapitalBid(player4, 5);

            var player5 = game.Players[4];
            game.MakeCapitalBid(player5, 5);

            var player6 = game.Players[5];
            game.MakeCapitalBid(player6, 6);

            Assert.AreEqual(1, player1.CapitalOrder);
            Assert.AreEqual(2, player2.CapitalOrder);
            Assert.AreEqual(6, player6.CapitalOrder);
            Assert.AreEqual(Enums.GamePhase.SelectCapital, game.Phase);

        }
    }
}
