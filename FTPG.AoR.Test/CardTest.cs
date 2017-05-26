using System;
using ftpg.AoR.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FTPG.AoR.Test
{
    [TestClass]
    public class CardTest
    {
        [TestMethod]
        public void CreateDeck()
        {
            var game = Util.GetGame(6);

            // 25 Epoch1 cards. 3 cards are handed to 6 players. 5 cards are removed from initial deck.
            Assert.AreEqual(7, game.DrawPile.Count);
            foreach (var player in game.Players)
            {
                game.DiscardCard(player, player.Cards[0], true);
            }
            Assert.AreEqual(13, game.DrawPile.Count);

            game.Players[0].Cards.AddRange(game.DrawCards(1));
            Assert.AreEqual(3, game.Players[0].Cards.Count);
            Assert.AreEqual(12, game.DrawPile.Count);

            //Next card draw will lead to new epoch and shuffle
            //game.Players[0].Cards.AddRange(game.DrawCards(2));
            //Assert.AreEqual(16, game.Players[0].Cards.Count);
        }

        [TestMethod]
        public void AlchemistGoldTest()
        {
            var game = Util.GetGame(3);
            game.Players[0].Capital = Enums.Capital.Venice;
            game.Players[1].Capital = Enums.Capital.Genoa;
            game.Players[2].Capital = Enums.Capital.Barcelona;
            game.PlayOrder = 1;
            var venice = game.Players[0];
            venice.PlayOrder = 1;
            var alchemistsGoldcard = new Card("Alchemist's Gold");
            game.Players[0].Cards.Add(alchemistsGoldcard);
            game.Players[1].Cash = 20;
            game.Players[1].WrittenCash = 25;
            game.PlayCard(alchemistsGoldcard, 0, "Genoa");
            Assert.AreEqual(7, game.Players[1].Cash);

            game.Players[0].Cards.Add(alchemistsGoldcard);
            game.Players[1].Cash = 10;
            game.Players[1].WrittenCash = 24;
            game.PlayCard(alchemistsGoldcard, 0, "Genoa");
            Assert.AreEqual(0, game.Players[1].Cash);
        }

        [TestMethod]
        public void ArmorTest()
        {
            var game = Util.GetGame(3);
            game.PlayOrder = 1;
            var venice = game.Players[0];
            venice.PlayOrder = 1;
            var armorCard = new Card("Armor");
            venice.Cards.Add(armorCard);
            game.PlayCard(armorCard, 0, string.Empty);
            Assert.AreEqual(1,game.CardsInEffect.Count);

            var longBowCard = new Card("Long Bow");
            venice.Cards.Add(longBowCard);
            game.PlayCard(longBowCard, 0, string.Empty);
            Assert.AreEqual(1, game.CardsInEffect.Count);
            Assert.AreEqual(2, game.Unplayable.Count); // also count stirrups
            Assert.IsTrue(game.CardsInEffect[0].Key == "Long Bow");
        }

        [TestMethod]
        public void BlackDeathTest()
        {
            var game = Util.GetGame(3);
            Util.AddDomsAndTokens(game); // add control to Barcelona
            var blackDeathCard = new Card("Black Death");
            game.Players[0].Cards.Add(blackDeathCard);
            game.PlayOrder = 1;
            game.Players[0].PlayOrder = 1;
            game.Phase = Enums.GamePhase.PlayCard;
            game.PlayCard(blackDeathCard, 0, "IV");
            var barcelonaProvince = game.Map.GetProvinceByName("Barcelona");
            var leonProvince = game.Map.GetProvinceByName("Leon");
            Assert.AreEqual(1, barcelonaProvince.Presences[0].Strength);
            Assert.AreEqual(1, leonProvince.Presences.Count);
        }

        [TestMethod]
        public void CivilWarTest()
        {
            var game = Util.GetGame(3);
            Util.AddDomsAndTokens(game);
            var civilWarCard = new Card("Civil War");
            var venice = game.Players[0];
            venice.Cards.Add(civilWarCard);

            game.PlayOrder = 1;
            venice.PlayOrder = 1;
            game.Phase = Enums.GamePhase.PlayCard;
            var barcelona = game.Players.Find(m => m.Capital == Enums.Capital.Barcelona);
            barcelona.PlayOrder = 2;

            game.PlayCard(civilWarCard, 0, "Barcelona");
            var barcelonaProvince = game.Map.GetProvinceByName("Barcelona");
            Assert.AreEqual(1, barcelonaProvince.Presences[0].Strength);
            Assert.AreEqual(10, barcelona.Misery);
            Assert.AreEqual(1, game.InterruptPreservedOrder);
            Assert.AreEqual("Civil War", game.Interrupt);
            Assert.AreEqual(3, barcelona.PlayOrder);
        }

        [TestMethod]
        public void EnlightenedRulerTest()
        {
            var game = Util.GetGame(3);
            game.PlayOrder = 1;
            //game.Phase = Enums.GamePhase.PlayCard;
            var venice = game.Players[0];
            venice.PlayOrder = 1;
            var enligthenedRulerCard = new Card("Enlightened Ruler");
            venice.Cards.Add(enligthenedRulerCard);
            game.PlayCard(enligthenedRulerCard, 0, string.Empty);

            var players = game.GetPlayersNotProtectedByEnlightenedRuler();
            Assert.AreEqual(2, players.Count);

            var genoa = game.Players[1];
            genoa.Misery = 10;
            var mysticismAboundsCard = new Card("Mysticism Abounds");
            venice.Cards.Add(mysticismAboundsCard);
            game.PlayCard(mysticismAboundsCard, 0, string.Empty);
            Assert.AreEqual(0, venice.Misery);
            Assert.AreEqual(50, genoa.Misery);

            var religiousStrifeCard = new Card("Religious Strife");
            venice.Cards.Add(mysticismAboundsCard);
            venice.AdvanceString = "EF";
            genoa.AdvanceString = "EF";
            game.PlayCard(religiousStrifeCard, 0, string.Empty);
            Assert.AreEqual(0, venice.Misery);
            Assert.AreEqual(70, genoa.Misery);


            var revolutionaryUprisingsCard = new Card("Revolutionary Uprisings");
            venice.Cards.Add(revolutionaryUprisingsCard);
            venice.AdvanceString = "IJK";
            genoa.AdvanceString = "IJK";
            game.PlayCard(revolutionaryUprisingsCard, 0, string.Empty);
            Assert.AreEqual(0, venice.Misery);
            Assert.AreEqual(100, genoa.Misery);
        }

        [TestMethod]
        public void FamineTest()
        {
            var game = Util.GetGame(3);
            game.Players[0].Capital = Enums.Capital.Venice;
            game.Players[1].Capital = Enums.Capital.Genoa;
            game.Players[2].Capital = Enums.Capital.Barcelona;
            game.PlayOrder = 1;
            game.Players[0].PlayOrder = 1;
            var card1 = new Card("Enlightened Ruler");
            var card2 = new Card("Mysticism Abounds");
            game.Players[0].Cards.Add(card1);
            game.Players[0].Cards.Add(card2);
            game.PlayCard(card1, 0, string.Empty);
            game.PlayCard(card2, 0, string.Empty);

            Assert.AreEqual(0, game.Players[0].Misery);
            Assert.AreEqual(40, game.Players[1].Misery);
            Assert.AreEqual(40, game.Players[2].Misery);
        }

        [TestMethod]
        public void MongolArmiesTest()
        {
            var game = Util.GetGame(3);
            game.DrawPile.Add(new Card("The Crusades"));
            var mongolArmiesCard = new Card("Mongol Armies");
            game.Players[0].Cards.Add(mongolArmiesCard);
            game.PlayOrder = 1;
            game.Players[0].PlayOrder = 1;
            game.Phase = Enums.GamePhase.PlayCard;
            game.PlayCard(mongolArmiesCard, 0, string.Empty);
            Assert.AreEqual(50, game.Players[0].Cash);
            Assert.AreEqual(1, game.Unplayable.Count);
        }

        [TestMethod]
        public void MysticismAboundsTest()
        {
            var game = Util.GetGame(3);
            game.PlayOrder = 1;
            game.Players[0].PlayOrder = 1;
            game.Players[1].AdvanceString = "ABCDE";
            var card1 = new Card("Enlightened Ruler");
            var mysticismAbounds = new Card("Mysticism Abounds");
            game.Players[0].Cards.Add(card1);
            game.Players[0].Cards.Add(mysticismAbounds);
            game.PlayCard(card1, 0, string.Empty);
            game.PlayCard(mysticismAbounds, 0, string.Empty);

            Assert.AreEqual(0, game.Players[0].Misery);
            Assert.AreEqual(0, game.Players[1].Misery);
            Assert.AreEqual(40, game.Players[2].Misery);
        }

        [TestMethod]
        public void PapalDecreeTest()
        {
            var game = Util.GetGame(3);
            game.PlayOrder = 1;
            //game.Phase = Enums.GamePhase.PlayCard;
            var venice = game.Players[0];
            venice.PlayOrder = 1;
            var papalDecreeCard = new Card("Papal Decree");
            venice.Cards.Add(papalDecreeCard);
            game.PlayCard(papalDecreeCard, 0, "Science");
            Assert.AreEqual(1, game.CardsInEffect.Count);
            Assert.AreEqual("Science", game.CardsInEffect[0].Value);
            Assert.AreEqual("Papal Decree", game.CardsInEffect[0].Key);
       }

        [TestMethod]
        public void PiratesVikingsTest()
        {
            var game = Util.GetGame(3);
            Util.AddDomsAndTokens(game);
            game.Phase = Enums.GamePhase.PlayCard;
            game.PlayOrder = 1;
            var piratesVikingsCard = new Card("Pirates/Vikings");
            var venice = game.Players[0];
            venice.PlayOrder = 1;
            venice.Cards.Add(piratesVikingsCard);
            game.PlayCard(piratesVikingsCard, 0, "Leon");
            Assert.AreEqual(4, venice.Cards.Count);

            Assert.IsFalse(game.PlayCard(piratesVikingsCard, 0, "Barcelona,Venice"));
            game.PlayCard(piratesVikingsCard, 0, "Barcelona");
            Assert.AreEqual(3, venice.Cards.Count);
            Assert.AreEqual(1, game.Map.Provinces.Find(m=>m.NameAsText == "Barcelona").Presences[0].Strength);

            venice.Cards.Add(piratesVikingsCard);
            game.Epoch = 2;
            game.PlayCard(piratesVikingsCard, 0, "Marseilles,Genoa");
            Assert.AreEqual(3, venice.Cards.Count);
            Assert.AreEqual(1, game.Map.Provinces.Find(m => m.NameAsText == "Marseilles").Presences[0].Strength);
            Assert.AreEqual(1, game.Map.Provinces.Find(m => m.NameAsText == "Genoa").Presences[0].Strength);


            venice.Cards.Add(piratesVikingsCard);
            game.Epoch = 3;
            game.PlayCard(piratesVikingsCard, 0, "Venice,Valencia,Toledo");
            Assert.AreEqual(3, venice.Cards.Count);
            Assert.AreEqual(1, game.Map.Provinces.Find(m => m.NameAsText == "Venice").Presences[0].Strength);
            Assert.AreEqual(1, game.Map.Provinces.Find(m => m.NameAsText == "Valencia").Presences[0].Strength);
            Assert.AreEqual(1, game.Map.Provinces.Find(m => m.NameAsText == "Toledo").Presences[0].Strength);
        }

        [TestMethod]
        public void ReligiousStrifeTest()
        {
            var game = Util.GetGame(3);
            var papalDecreeCard = new Card("Papal Decree");
            var religiousStrifeCard = new Card("Religious Strife");
            game.Players[0].Cards.Add(papalDecreeCard);
            game.Players[0].Cards.Add(religiousStrifeCard);
            game.Players[0].AdvanceString = "EF";
            game.Players[1].AdvanceString = "E";
            game.PlayOrder = 1;
            game.Players[0].PlayOrder = 1;
            game.Phase = Enums.GamePhase.PlayCard;

            // Test that Religious Strife voids Papal Decree
            game.PlayCard(papalDecreeCard, 0, string.Empty);
            Assert.AreEqual(1, game.CardsInEffect.Count);
            game.PlayCard(religiousStrifeCard, 0, string.Empty);
            Assert.AreEqual(20, game.Players[0].Misery);
            Assert.AreEqual(10, game.Players[1].Misery);
            Assert.AreEqual(0, game.Players[2].Misery);
            Assert.AreEqual(0, game.CardsInEffect.Count);

            // Test that if religious strife is played in Epoch 3, Papal Decree becomes unplayable
            game.Players[0].Cards.Add(papalDecreeCard);
            game.Players[0].Cards.Add(religiousStrifeCard);
            game.Epoch = 3;
            game.PlayCard(religiousStrifeCard, 0, string.Empty);
            Assert.AreEqual(1, game.Unplayable.Count);
        }

        [TestMethod]
        public void TheCrusadesTest()
        {
            var game = Util.GetGame(3);
            var theCrusades = new Card("The Crusades");
            var venice = game.Players[0];
            venice.Cards.Add(theCrusades);

            game.PlayOrder = 1;
            venice.PlayOrder = 1;
            game.Phase = Enums.GamePhase.PlayCard;

            // Test that province must be in area VI
            Assert.IsFalse(game.PlayCard(theCrusades, 0, "Barcelona"));
            Assert.AreEqual(4, venice.Cards.Count); // Note. Venice is dealt 3 cards in game initialization

            game.PlayCard(theCrusades, 0, "Aleppo");
            var aleppoProvince = game.Map.GetProvinceByName("Aleppo");
            Assert.AreEqual(4, aleppoProvince.Presences[0].Strength);
            Assert.AreEqual(0, aleppoProvince.Presences[0].Original);
            Assert.AreEqual(Enums.Capital.Venice, aleppoProvince.Presences[0].Capital);
            Assert.AreEqual(3, venice.Cards.Count); // Note. Venice is dealt 3 cards in game initialization
            Assert.AreEqual(10, venice.Misery);
        }


        [TestMethod]
        public void WarTest()
        {
            var game = Util.GetGame(3);
            var warCard = new Card("War!");
            var venice = game.Players[0];
            var genoa = game.Players[1];
            venice.Cards.Add(warCard);
            game.PlayOrder = 1;
            venice.PlayOrder = 1;
            game.Phase = Enums.GamePhase.PlayCard;
            
            game.PlayCard(warCard, 0, "genoa");
            var genoaProvince = game.Map.GetProvinceByName("Genoa");
            var veniceProvince = game.Map.GetProvinceByName("Venice");

            Assert.IsTrue(venice.Misery > 0);
            Assert.IsTrue(genoa.Misery > 0);
            if (genoa.Misery > venice.Misery)
            {
                Assert.AreEqual(venice.Capital, genoaProvince.Presences[0].Capital);
                Assert.AreEqual(venice.Capital, game.WarResult.Winner);
                Assert.AreEqual(genoa.Capital, game.WarResult.Looser);
                Assert.IsTrue(game.WarResult.Loss > 0);
            }
            else
            {
                Assert.AreEqual(genoa.Capital, veniceProvince.Presences[0].Capital);
            }

                
        }







    }
}
