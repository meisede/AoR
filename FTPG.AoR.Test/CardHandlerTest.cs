using System.Collections.Generic;
using ftpg.AoR.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FTPG.AoR.Test
{

    [TestClass]
    public class CardHandlerTest
    {
        [TestMethod]
        public void IsUnplayableTest()
        {
            var game = Util.GetGame(3);
            game.Players[0].Capital = Enums.Capital.Venice;
            game.Players[1].Capital = Enums.Capital.Genoa;
            game.Players[2].Capital = Enums.Capital.Barcelona;
            game.PlayOrder = 1;
            game.Players[0].PlayOrder = 1;
            var cardArmor = new Card("Armor");
            game.Players[1].Cards.Add(cardArmor);
            game.Players[2].Cards.Add(new Card("Stirrups"));

            var cardLongBow = new Card("Long Bow");
            game.Players[0].Cards.Add(cardLongBow);
            Assert.IsFalse(CardHandler.IsUnplayable(game.Unplayable, "Armor"));
            Assert.IsFalse(CardHandler.IsUnplayable(game.Unplayable, "Stirrups"));
            game.PlayCard(cardLongBow,0, string.Empty);
            Assert.IsTrue(CardHandler.IsUnplayable(game.Unplayable, "Armor"));
            Assert.IsTrue(CardHandler.IsUnplayable(game.Unplayable, "Stirrups"));

            game.Unplayable = new List<string>();
            var cardGunPowder = new Card("Gunpowder");
            game.Players[0].Cards.Add(cardGunPowder);
            Assert.IsFalse(CardHandler.IsUnplayable(game.Unplayable, "Armor"));
            game.PlayCard(cardGunPowder, 0,string.Empty);
            Assert.IsTrue(CardHandler.IsUnplayable(game.Unplayable, "Armor"));



        }
    }
}
