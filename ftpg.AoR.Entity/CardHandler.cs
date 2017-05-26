using System.Collections.Generic;
using System.Linq;

namespace ftpg.AoR.Entity
{
    public class CardHandler
    {

        // Todo. Add code for new epoch
        public static Card DrawCard(List<Card> deck)
        {
            var card = deck[Common.GetRandomNumber(deck.Count)];
            deck.Remove(card);
            return card;
        }

        public static string GetEarnedRebateFromLeaderText(Card card, string ownedAdvances,
            List<string> cardsLastingEffect)
        {
            var earnedRebate = GetEarnedRebateFromLeader(card, ownedAdvances, cardsLastingEffect);
            if (earnedRebate > 0)
            {
                return "(Earns rebate: $" + earnedRebate + ")";
            }
            return string.Empty;
        }

        public static int GetEarnedRebateFromLeader(Card card,string ownedAdvances, List<string> cardsLastingEffect)
        {
            if (!ownedAdvances.Contains("H"))
            {
                return 0;
            }
            var earnedRebate = 0;
            foreach (var letter in card.AdvanceDiscountLetters.ToCharArray())
            {
                if (ownedAdvances.Contains(letter))
                {
                    earnedRebate += card.Discount(card.Name, cardsLastingEffect);
                }
            }
            return earnedRebate;
        }

        public static int GetDiscountFromLeader(List<CardPlay> cardPlays, string letter, Enums.Capital capital, List<string> cardsLastingEffect, bool hasWrittenRecord)
        {
            var discount = 0;
            foreach (var cardPlay in cardPlays)
            {
                if (cardPlay.Card.AdvanceDiscountLetters.Contains(letter) && (cardPlay.FeePaid || cardPlay.Capital == capital))
                {
                    discount += (hasWrittenRecord)
                        ? cardPlay.Card.Discount(cardPlay.Card.Name, cardsLastingEffect) + 10
                        : cardPlay.Card.Discount(cardPlay.Card.Name, cardsLastingEffect);
                }
            }
            return discount;
        }


        public static bool IsUnplayed(Game game , string cardName) // tested
        {
            return game.Players.Any(player => player.Cards.Exists(m => m.Name == cardName) || game.DrawPile.Exists(m => m.Name == cardName));
        }

        public static bool IsUnplayable(List<string> unplayable, string cardName) // tested
        {
            return unplayable.Any(m => m == cardName);
        }

        public static List<Card> Shuffle(List<Card> discardDeck, List<Card> epochDeck)
        {
            var newDeck = new List<Card>();
            newDeck.AddRange(discardDeck);
            newDeck.AddRange(epochDeck);
            return newDeck;
        }

        public static void AddEuroRuleWithHeldCards(List<Card> deck)
        {
            deck.Add(new Card("Silk"));
            deck.Add(new Card("Spice"));
            deck.Add(new Card("The Crusades"));
            deck.Add(new Card("Rashid ad Din"));
            deck.Add(new Card("Walter the Penniless"));
        }

        public static List<Card> CreateEpoch1CardsStartingCards()
        {
            return new List<Card>{
                new Card("Cloth/Wine"), 
                new Card("Fur"),
                new Card("Ivory/Gold"),
                new Card("Metal"),
                new Card("Stone"),
                new Card("Stone"),
                new Card("Timber"),
                new Card("Timber"), // Moved from Epoch II
                new Card("Wool"),
                new Card("Wool"),
                new Card("Alchemist's Gold"),
                new Card("Armor"),
                new Card("Civil War"),
                new Card("Enlightened Ruler"),
                new Card("Famine"),
                new Card("Mysticism Abounds"),
                new Card("Papal Decree"),
                new Card("Pirates/Vikings"),
                new Card("Rebellion"),
                new Card("Revolutionary Uprisings"),
                new Card("Stirrups"),
                new Card("War!"),
                new Card("Charlemagne"),
                new Card("Dionysus Exiguus"),
                new Card("St. Benedict"),
            };
        }

        public static List<Card> CreateEpoch2Cards()
        {
            return new List<Card>{
                new Card("Cloth"),
                new Card("Grain"),
                new Card("Grain"),
                new Card("Metal"),
                new Card("Silk"),
                new Card("Spice"),
                new Card("Timber"),
                new Card("Wine"),
                new Card("Black Death"),
                new Card("Gunpowder"),
                new Card("Long Bow"),
                new Card("Mongol Armies"),
                new Card("Religious Strife"),
                new Card("Christopher Columbus"),
                new Card("Desiderius Erasmus"),
                new Card("Ibn Majid"),
                new Card("Johann Gutenberg"),
                new Card("Marco Polo"),
                new Card("Nicolaus Copernicus"),
                new Card("Prince Henry"),
                new Card("William Caxton"),
            };
        }

        public static List<Card> CreateEpoch3Cards()
        {
            return new List<Card>{
                    new Card("Cloth"),
                    new Card("Fur"),
                    new Card("Gold"),
                    new Card("Metal"),
                    new Card("Silk"),
                    new Card("Spice"),
                    new Card("Wine"),
                    new Card("Andreas Vesalius"),
                    new Card("Bartolome de Las Casas"),
                    new Card("Galileo Galilei"),
                    new Card("Henry Oldenburg"),
                    new Card("Leonardo Da Vinci"),
                    new Card("Sir Isaac Newton"),
            };
        }

        public static List<Pair> RemoveFromInEffect(List<Pair> cardsInEffect, string cardName)
        {
            var pair = cardsInEffect.Find(m => m.Key == cardName);
            cardsInEffect.Remove(pair);
            return cardsInEffect;
        }
    }
}
