using System.Collections.Generic;

namespace ftpg.AoR.Entity
{
    public class Card
    {
        public string Name { get; set; }

        public Card()
        {
        }

        public Card(string name)
        {
            Name = name;
        }

        public bool MoveToRedrawPile
        {
            get
            {
                switch (Name)
                {
                    case "Alchemist's Gold":
                    case "Andreas Vesalius":
                    case "Armor":
                    case "Bartolome de Las Casas":
                    case "Charlemagne":
                    case "Christopher Columbus":
                    case "The Crusades":
                    case "Desiderius Erasmus":
                    case "Dionysus Exiguus":
                    case "Galileo Galilei":
                    case "Gunpowder":
                    case "Henry Oldenburg":
                    case "Ibn Majid":
                    case "Johann Gutenberg":
                    case "Leonardo Da Vinci":
                    case "Long Bow":
                    case "Marco Polo":
                    case "Mongol Armies":
                    case "Nicolaus Copernicus":
                    case "Rashid ad Din":
                    case "Prince Henry":
                    case "Revolutionary Uprisings":
                    case "Sir Isaac Newton":
                    case "St. Benedict":
                    case "Stirrups":
                    case "Walter the Penniless":
                    case "William Caxton":
                        return false;
                    default:
                        return true;
                }
            }
        }

        public Enums.CardType Type
        {
            get
            {
                switch (Name)
                {
                    case "Stone":
                    case "Wool":
                    case "Timber":
                    case "Grain":
                    case "Cloth":
                    case "Cloth III":
                    case "Wine":
                    case "Wine III":
                    case "Metal":
                    case "Metal III":
                    case "Fur":
                    case "Fur III":
                    case "Silk":
                    case "Silk III":
                    case "Spice":
                    case "Spice III":
                    case "Gold":
                    case "Ivory":
                    case "Ivory III":
                    case "Cloth/Wine":
                    case "Ivory/Gold":
                    case "Ivory/Gold as Ivory":
                    case "Ivory/Gold as Gold":
                    case "Cloth/Wine as Cloth":
                    case "Cloth/Wine as Wine":
                        return Enums.CardType.Good;
                    case "Andreas Vesalius":
                    case "Charlemagne":
                    case "Christopher Columbus":
                    case "Bartolome de Las Casas":
                    case "Desiderius Erasmus":
                    case "Dionysus Exiguus":
                    case "Galileo Galilei":
                    case "Henry Oldenburg":
                    case "Ibn Majid":
                    case "Johann Gutenberg":
                    case "Leonardo Da Vinci":
                    case "Marco Polo":
                    case "Nicolaus Copernicus":
                    case "Prince Henry":
                    case "Rashid ad Din":
                    case "Sir Isaac Newton":
                    case "St. Benedict":
                    case "Walter the Penniless":
                    case "William Caxton":
                        return Enums.CardType.Leader;
                    default:
                        return Enums.CardType.Event;
                }
            }
        }

        public Enums.GoodType Good
        {
            get
            {
                switch (Name)
                {
                    case "Stone":
                        return Enums.GoodType.Stone;
                    case "Wool":
                        return Enums.GoodType.Wool;
                    case "Timber":
                        return Enums.GoodType.Timber;
                    case "Grain":
                        return Enums.GoodType.Grain;
                    case "Cloth":
                    case "Cloth/Wine as Cloth":
                        return Enums.GoodType.Cloth;
                    case "Wine":
                    case "Cloth/Wine as Wine":
                        return Enums.GoodType.Wine;
                    case "Metal":
                        return Enums.GoodType.Metal;
                    case "Fur":
                        return Enums.GoodType.Fur;
                    case "Silk":
                        return Enums.GoodType.Silk;
                    case "Spice":
                        return Enums.GoodType.Spice;
                    case "Gold":
                    case "Ivory/Gold as Gold":
                        return Enums.GoodType.Gold;
                    case "Ivory":
                    case "Ivory/Gold as Ivory":
                        return Enums.GoodType.Ivory;
                    default:
                        return Enums.GoodType.NoGood;
                }
            }
        }

        public int GoodValue
        {
            get
            {
                switch (Name)
                {
                    case "Stone":
                        return 1;
                    case "Wool":
                        return 2;
                    case "Timber":
                        return 3;
                    case "Grain":
                        return 4;
                    case "Cloth":
                    case "Cloth/Wine as Cloth":
                        return 5;
                    case "Wine":
                    case "Cloth/Wine as Wine":
                        return 5;
                    case "Metal":
                        return 6;
                    case "Fur":
                        return 7;
                    case "Silk":
                        return 8;
                    case "Spice":
                        return 9;
                    case "Gold":
                    case "Ivory":
                    case "Ivory/Gold as Ivory":
                    case "Ivory/Gold as Gold":
                        return 10;
                    default:
                        return 0;
                }
            }
        }

        public string Description
        {
            get
            {
                switch (Name)
                {
                    case "Stone":
                        return "Payout: 1, 4, 9, 16, 25, 36, 49, 64, 81, 100";
                    case "Wool":
                        return "Payout: 2, 8, 18, 32, 50, 72, 98, 128, 162, 200";
                    case "Timber":
                        return "Payout: 3, 12, 27, 48, 75, 108, 147, 192, 243";
                    case "Grain":
                        return "Payout: 4, 16, 36, 64, 100, 144, 196, 256, 324, 400";
                    case "Cloth":
                    case "Cloth/Wine":
                        return "Payout: 5,20, 45, 80, 125, 180, 245, 320, 405, 500";
                    case "Wine":
                        return "Payout: 5,20, 45, 80, 125, 180, 245, 320";
                    case "Metal":
                        return "Payout: 6, 24, 54, 96, 150, 216, 294, 384, 486";
                    case "Fur":
                        return "Payout: 7, 28, 63, 112, 175, 252, 343";
                    case "Silk":
                        return "Payout: 8, 32, 72, 128, 200, 288, 392, 512";
                    case "Spice":
                        return "Payout: 9, 36, 81, 144, 225, 324, 441, 576, 723";
                    case "Ivory":
                    case "Gold":
                        return "Payout: 10, 40, 90, 160, 250, 360, 490";

                    case "Alchemist's Gold":
                        return
                            "A player of your choice may pay half of written cash to Banker. Penalty cannot exceed current cash. Cash already spent on Patronage defense is not vulnerable. Voided by LAWS OF MATTER. If all players have LAWS OF MATTER, this card becomes unplayable Misery burden.";
                    case "Armor":
                        return 
                            "A temporary Arms advantage enhances your trading ventures. You win all Attack ties this turn (including WAR). Add 1 to your competition totals this turn on both offense and defense. Voided by LONG BOW or GUNPOWDER. If voided, ARMOR becomes unplayable Misery burden.";
                    case "Black Death":
                        return
                            "Select one Area to be hit by the plague. All Colored Tokens (squares) in that Area are returned to their respective Stocks. Then reduce all Colored Dominance Markers (circles) of all players in that Area to a single Colored Token (square) per Province.";
                    case "Chinese Treasure Fleet":
                        return
                            "Payouts for Fur, Silk, Spice, Ivory and Gold are reduced one level this turn. You may trigger a payout for either Timber, Grain or Wine.";
                    case "Civil War":
                        return
                            "A player of your choice is struck by CIVIL WAR. He gains one Misery. Any Dominance Marker (circle) in his Capital is reduced to a Colored Token (square). He must lose his choice of half of his last recorded cash or half of his bid tokens (squares). At start of the Expansion Phase, his Order of play position becomes last";
                    case "The Crusades":
                        return
                            "Place one of your Colored Dominance Markers (circle) anywhere within Area VI and remove any other markers in that Province. Gain one Misery. This card increases the credits for WALTER THE PENNILESS if he is also played this turn. Voided by MONGOL ARMIES in Epoch 2 or Epoch 3 and becomes unplayable Misery burden.";
                    case "Enlightened Ruler":
                        return
                            "Play on yourself to void the effects of MYSTICISM, RELIGIOUS STRIFE, CIVIL WAR, REVOLUTIONARY UPRISINGS, REBELLION, and ALCHEMIST'S GOLD for the rest of the turn.";
                    case "Famine":
                        return
                            "All players gain four spaces on the Misery Index minus one space for each Grain Province they dominate. Having [J] Improved Agriculture also reduces the penalty by one space.";
                    case "Gunpowder":
                        return
                            "A temporary Arms advantage enhances your trading ventures. You win all Attack ties this turn (including WAR). Add 1 to all your competition totals this turn on both offense and defense. Voids ARMOR and STIRRUPS and turns them into Misery burdens.";
                    case "Long Bow":
                        return
                            "A temporary Arms advantage enhances your trading ventures. You win all Attack ties this turn (including WAR). Add 1 to all your competition totals this turn on both offense and defense except against players currently using GUNPOWDER. Voids ARMOR and STIRRUPS and turns them into Misery burdens.";
                    case "Mongol Armies":
                        return
                            "Collect $10 from the Bank. MARCO POLO credits are doubled if played hereafter. The Crusades event becomes unplayable Misery burden.";
                    case "Mysticism Abounds":
                        return
                            "All players gain four spaces on the Misery Index minus one space for each Science Advance [A,B,C,D] held. This card becomes a worthless Misery burden if all players own all four Sciences [A,B,C,D].";
                    case "Papal Decree":
                        return
                            "You may ban the acquisition by all players of any Advance in one of the following three categories: Science [A,B,C,D], Religion [E,F,G,H] and Exploration [R,S,T,U]. Voided by RELIGIOUS STRIFE played in the same turn. When RELIGIOUS STRIFE occurs in Epoch 3, this card becomes an unplayable Misery burden.";
                    case "Pirates/Vikings":
                        return
                            "Reduce any Dominance Marker (circle) to a Colored Token (square) in any coastal Province of your choice. If played during Epoch II, reduce two Colored Dominance Markers (circles). If played during Epoch III, reduce three Colored Dominance Markers (circles).";
                    case "Rebellion":
                        return
                            "Reduce any Dominance Marker (circle) to a Colored Token (square) in any coastal Province of your choice. If played during Epoch II, reduce two Colored Dominance Markers (circles). If played during Epoch III, reduce three Colored Dominance Markers (circles).";
                    case "Religious Strife":
                        return
                            "All players increase Misery one space for each Religion Advance [E,F,G,H] they hold. Voids PAPAL DECREE, if played in the same turn. If played in Epoch 3, the PAPAL DECREE card becomes an unplayable Misery burden.";
                    case "Revolutionary Uprisings":
                        return
                            "Each player gains one space on the Misery Index for each Commerce Advance [I,J,K,L,M] he holds.";
                    case "Stirrups":
                        return 
                            "A temporary Arms advantage enhances your trading ventures. You win all Attack ties this turn (including WAR). Add 1 to all your competition totals this turn on both offense and defense, except against player currently using ARMOR. Voided by LONG BOW or GUNPOWDER. If voided, STIRRUPS becomes unplayable Misery burden.";
                    case "War!":
                        return
                            "Declare WAR on any player. Each player rolls 1 die. [W] Nationalism & Military Advantages modify their owner's die roll by +1 (each). Highest total gains one Misery, lowest total gains two Misery. The difference between the modified die rolls is the amount of supportable Colored Dominance Markers (circles) the loser must cede to the winner (loser's choice). If tied, both sides gain one Misery and continue resolution in each succeeding round of the Play Cards Phase until one side wins.";

                    case "Andreas Vesalius":
                        return "20 OFF [B] Human Body, [D] Enlightenment";
                    case "Bartolome de Las Casas":
                        return "30 OFF [Y] Cosmopolitan";
                    case "Charlemagne":
                        return "20 OFF [W] Nationalism";
                    case "Christopher Columbus":
                        return "30 OFF [T] Ocean Navigation, [U] New World";
                    case "Desiderius Erasmus":
                        return "20 OFF [O] Printed Word, [Q] Renaissance";
                    case "Dionysus Exiguus":
                        return "20 OFF [N] Written Record";
                    case "Galileo Galilei":
                        return "20 OFF [A] The Heavens, [Q] Renaissance";
                    case "Ibn Majid":
                        return "20 OFF [T] Ocean Navigation, [Y] Cosmopolitan";
                    case "Johann Gutenberg":
                        return "30 OFF [O] Printed Word";
                    case "Leonardo Da Vinci":
                        return "20 OFF [P] Master Art, [B] Human Body, [Q] Renaissance";
                    case "Marco Polo":
                        return "20 OFF [R] Overland East, [Y] Cosmopolitan {40 OFF after Mongol Armies}";
                    case "Nicolaus Copernicus":
                        return "20 OFF [A] The Heavens, [X] Institutional Research";
                    case "Henry Oldenburg":
                        return "30 OFF [D] Enlightenment";
                    case "Prince Henry":
                        return "20 OFF [T] Ocean Navigation, [X] Institutional Research";
                    case "Rashid ad Din":
                        return "10 OFF [N] Written Record, [R] Overland East";
                    case "Sir Isaac Newton":
                        return "20 OFF [C] Laws of Matter, [D] Enlightenment";
                    case "St. Benedict":
                        return "10 OFF [N] Written Record, [E] Patronage";
                    case "Walter the Penniless":
                        return "20 OFF [R] Overland East  {30 OFF during the Crusades turn}";
                    case "William Caxton":
                        return "20 OFF [O] Printed Word";
                    default:
                        return "Error";
                }
            }
        }

        public string AdvanceDiscountLetters
        {
            get
            {
                switch (Name)
                {
                    
                    case "Andreas Vesalius":
                        return "BD";
                    case "Bartolome de Las Casas":
                        return "Y";
                    case "Charlemagne":
                        return "W";
                    case "Christopher Columbus":
                        return "TU";
                    case "Desiderius Erasmus":
                        return "OQ";
                    case "Dionysus Exiguus":
                        return "N";
                    case "Galileo Galilei":
                        return "AQ";
                    case "Henry Oldenburg":
                        return "D";
                    case "Ibn Majid":
                        return "TY";
                    case "Johann Gutenberg":
                        return "O";
                    case "Leonardo Da Vinci":
                        return "BPQ";
                    case "Marco Polo":
                        return "RY";
                    case "Nicolaus Copernicus":
                        return "AX";
                    case "Prince Henry":
                        return "TX";
                    case "Rashid ad Din":
                        return "NR";
                    case "Sir Isaac Newton":
                        return "CD";
                    case "St. Benedict":
                        return "EN";
                    case "Walter the Penniless":
                        return "R";
                    case "William Caxton":
                        return "O";
                        
                    default:
                        return "ERROR";
                }
            }
        }

        public int Discount(string name, List<string> cardsLastingEffect)
        {
            switch (name)
            {
                case "Rashid ad Din":
                case "St. Benedict":
                    return 10;
                case "Charlemagne":
                case "Dionysus Exiguus":
                case "Nicolaus Copernicus":
                case "Leonardo Da Vinci":
                case "Desiderius Erasmus":
                case "Ibn Majid":
                case "Prince Henry":
                case "Andreas Vesalius":
                case "William Caxton":
                case "Galileo Galilei":
                case "Sir Isaac Newton":
                    return 20;
                case "Christopher Columbus":
                case "Bartolome de Las Casas":
                case "Johann Gutenberg":
                case "Henry Oldenburg":
                    return 30;
                case "Marco Polo":
                    return cardsLastingEffect.Contains("Mongol Armies") ? 40 : 20;
                case "Walter the Penniless":
                    return cardsLastingEffect.Contains("The Crusades") ? 30 : 20;

                default:
                    return 0;
            }
        }

        public bool UnPlayable(List<string> unplayable)
        {
            return CardHandler.IsUnplayable(unplayable, Name);
        }
    }
}