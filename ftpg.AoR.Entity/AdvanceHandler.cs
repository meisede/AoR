using System;
using System.Collections.Generic;
using System.Linq;

namespace ftpg.AoR.Entity
{
    public class AdvanceHandler
    {
        public static List<Advance> GetAdvances()
        {
            var list = new List<Advance>
            {
                new Advance { Letter = "A", Cost = 30,  Name = "The Heavens",           AdvanceType  = Enums.AdvanceType.TheHeavens,            MiseryRelief = 5,   Credits = 20,   Requisite = string.Empty,   Description = "Transit one sea as a coastal province",                          Group = Enums.AdvanceGroup.Science} ,
                new Advance { Letter = "B", Cost = 60,  Name = "Human Body",            AdvanceType  = Enums.AdvanceType.HumanBody,             MiseryRelief = 10,  Credits = 20,   Requisite = string.Empty,   Description = "Reduces Misery one space",                                       Group = Enums.AdvanceGroup.Science},
                new Advance { Letter = "C", Cost = 90,  Name = "Laws of Matter",        AdvanceType  = Enums.AdvanceType.LawsOfMatter,          MiseryRelief = 5,   Credits = 20,   Requisite = string.Empty,   Description = "Avoids Alchemst's Gold",                                         Group = Enums.AdvanceGroup.Science},
                new Advance { Letter = "D", Cost = 120, Name = "Enlightenment",         AdvanceType  = Enums.AdvanceType.Enlightenment,         MiseryRelief = 50,  Credits = 20,   Requisite = string.Empty,   Description = "Misery Relief ½ price if have at least 1 advance tier",          Group = Enums.AdvanceGroup.Science},

                new Advance { Letter = "E", Cost = 30,  Name = "Patronage",             AdvanceType  = Enums.AdvanceType.Patronage,             MiseryRelief = 10,  Credits = 20,   Requisite = string.Empty,   Description = "May invest in other players' leaders",                           Group = Enums.AdvanceGroup.Religion},
                new Advance { Letter = "F", Cost = 60,  Name = "Holy Indulgence",       AdvanceType  = Enums.AdvanceType.HolyIndulgence,        MiseryRelief = 0,   Credits = 20,   Requisite = string.Empty,   Description = "Collect 2 tokens each turn from each non-holder",                Group = Enums.AdvanceGroup.Religion},
                new Advance { Letter = "G", Cost = 90,  Name = "Proselytism",           AdvanceType  = Enums.AdvanceType.Proselytism,           MiseryRelief = 0,   Credits = 20,   Requisite = string.Empty,   Description = "Win attacks if green die roll ≥ your Order Of Play",             Group = Enums.AdvanceGroup.Religion},
                new Advance { Letter = "H", Cost = 120, Name = "Cathedral",             AdvanceType  = Enums.AdvanceType.Cathedral,             MiseryRelief = 25,  Credits = 20,   Requisite = "F",            Description = "Win 1 non-War attack/defense per turn vs each non-holder",       Group = Enums.AdvanceGroup.Religion},

                new Advance { Letter = "I", Cost = 20,  Name = "Caravan",               AdvanceType  = Enums.AdvanceType.Caravan,               MiseryRelief = 5,   Credits = 10,   Requisite = string.Empty,   Description = "May put expansion tokens through adjacent uncontrolled land provinces", Group = Enums.AdvanceGroup.Commerce},
                new Advance { Letter = "J", Cost = 40,  Name = "Wind/Watermill",        AdvanceType  = Enums.AdvanceType.WindWatermill,         MiseryRelief = 5,   Credits = 10,   Requisite = "I",            Description = "1 trade try vs defeated mkt owner; d6 ≤ Mkt #; cost: Mkt #",     Group = Enums.AdvanceGroup.Commerce},
                new Advance { Letter = "K", Cost = 50,  Name = "Improved Agriculture",  AdvanceType  = Enums.AdvanceType.ImprovedAgriculture,   MiseryRelief = 25,  Credits = 10,   Requisite = "J",            Description = "–1 Misery; Reduce Famine Misery by –1",                          Group = Enums.AdvanceGroup.Commerce},
                new Advance { Letter = "L", Cost = 80,  Name = "Interest & Profit",     AdvanceType  = Enums.AdvanceType.InterestAndProfit,     MiseryRelief = 0,   Credits = 20,   Requisite = "K",            Description = "2x cash on hand after Expansion, up to value of Income",         Group = Enums.AdvanceGroup.Commerce},
                new Advance { Letter = "M", Cost = 110, Name = "Industry",              AdvanceType  = Enums.AdvanceType.Industry,              MiseryRelief = 5,   Credits = 0,    Requisite = "M",            Description = "Increase your commodity values by 1 payment box",                Group = Enums.AdvanceGroup.Commerce},

                new Advance { Letter = "N", Cost = 30,  Name = "Written Record",        AdvanceType  = Enums.AdvanceType.WrittenRecord,         MiseryRelief = 5,   Credits = 5,    Requisite = string.Empty,   Description = "+$10 for Leader credits, including Patronage claims",            Group = Enums.AdvanceGroup.Communic},
                new Advance { Letter = "O", Cost = 60,  Name = "Printed Word",          AdvanceType  = Enums.AdvanceType.PrintedWord,           MiseryRelief = 10,  Credits = 10,   Requisite = "N",            Description = "Get Leader Credit rebate for any prior held advances",           Group = Enums.AdvanceGroup.Communic},
                new Advance { Letter = "P", Cost = 90,  Name = "Master Art",            AdvanceType  = Enums.AdvanceType.MasterArt,             MiseryRelief = 5,   Credits = 5,    Requisite = "O",            Description = "May discard 1 each Buy Card phase; Discard 1 on buying",         Group = Enums.AdvanceGroup.Communic},
                new Advance { Letter = "Q", Cost = 120, Name = "Renaissance",           AdvanceType  = Enums.AdvanceType.Renaissance,           MiseryRelief = 50,  Credits = 50,   Requisite = "P",            Description = "Once per turn trade Order of Play with adjacent non-holder",     Group = Enums.AdvanceGroup.Communic},
                
                new Advance { Letter = "R", Cost = 40,  Name = "Overland East",         AdvanceType  = Enums.AdvanceType.OverlandEast,          MiseryRelief = 5,   Credits = 5,    Requisite = string.Empty,   Description = "May enter Area V",                                               Group = Enums.AdvanceGroup.Exploration},
                new Advance { Letter = "S", Cost = 80,  Name = "Seaworthy Vessels",     AdvanceType  = Enums.AdvanceType.SeaworthyVessels,      MiseryRelief = 5,   Credits = 5,    Requisite = string.Empty,   Description = "Seaworthy 10, enter all coasts but Far East/New World",          Group = Enums.AdvanceGroup.Exploration},
                new Advance { Letter = "T", Cost = 120, Name = "Ocean Navigation",      AdvanceType  = Enums.AdvanceType.OceanNavigation,       MiseryRelief = 5,   Credits = 5,    Requisite = "A,S",          Description = "Ship on Ocean Nav 1, no token limit; May enter Far East",        Group = Enums.AdvanceGroup.Exploration},
                new Advance { Letter = "U", Cost = 160, Name = "New World",             AdvanceType  = Enums.AdvanceType.NewWorld,              MiseryRelief = 25,  Credits = 25,   Requisite = "V,T",          Description = "–1 Misery each Income phase; May enter New World",               Group = Enums.AdvanceGroup.Exploration},

                new Advance { Letter = "V", Cost = 20,  Name = "Urban Ascendancy",      AdvanceType  = Enums.AdvanceType.UrbanAscendancy,       MiseryRelief = 5,   Credits = 5,    Requisite = string.Empty,   Description = "May buy 1 extra card draw for $10 each Buy Card phase",          Group = Enums.AdvanceGroup.Civics},
                new Advance { Letter = "W", Cost = 60,  Name = "Nationalism",           AdvanceType  = Enums.AdvanceType.Nationalism,           MiseryRelief = 5,   Credits = 5,    Requisite = string.Empty,   Description = "+1 to attack/defense totals in start Area; +1 to any War roll",  Group = Enums.AdvanceGroup.Civics},
                new Advance { Letter = "X", Cost = 100, Name = "Institutional Research", AdvanceType  = Enums.AdvanceType.InstitutionalResearch, MiseryRelief = 10,  Credits = 10,   Requisite = string.Empty,   Description = "Discount any advance $10 except Civics and Religion",            Group = Enums.AdvanceGroup.Civics},
                new Advance { Letter = "Y", Cost = 150, Name = "Cosmopolitan",          AdvanceType  = Enums.AdvanceType.Cosmopolitan,          MiseryRelief = 25,  Credits = 25,   Requisite = "R",            Description = "+1 strength for Satellite on attacks into supported province",   Group = Enums.AdvanceGroup.Civics},
                new Advance { Letter = "Z", Cost = 170, Name = "Middle Class",          AdvanceType  = Enums.AdvanceType.MiddleClass,           MiseryRelief = 50,  Credits = 50,   Requisite = "K",            Description = "+$10 Income each turn; Stabilization cost is ½",                 Group = Enums.AdvanceGroup.Civics},

            };
            return list;
        }

        public static bool HasAdvance(Player player, Enums.AdvanceType advanceType)
        {
            return player.AdvanceString.Contains(GetLetterFromAdvance(advanceType));
        }

        public static string GetLetterFromAdvance(Enums.AdvanceType advanceType)
        {
            return GetAdvances().Find(m => m.AdvanceType == advanceType).Letter;
        }

        public static Advance GetAdvanceFromLetter(string letter)
        {
            return GetAdvances().Find(m => m.Letter == letter);
        }

        public static bool AnyPlayerHasAdvance(List<Player> players, Enums.AdvanceType advanceType)
        {
            return players.Any(m => m.AdvanceString.Contains(GetLetterFromAdvance(advanceType)));
        }

        public static bool AnyOtherNationHasAdvance(Game game, Enums.Capital capital, string letter)
        {
            return game.Players.Where(m => m.Capital != capital).Any(nation => nation.AdvanceString.IndexOf(letter, StringComparison.Ordinal) > -1);
        }

        public static List<Player> AllPlayersNotHaveAdvance(List<Player> players, Enums.AdvanceType advanceType)
        {
            return players.FindAll(m => m.AdvanceString.IndexOf(GetLetterFromAdvance(advanceType), StringComparison.Ordinal) < 0);
        }
        
        public static List<Player> AllPlayersHaveAdvance(List<Player> players, Enums.AdvanceType advanceType)
        {
            return players.FindAll(m => m.AdvanceString.IndexOf(GetLetterFromAdvance(advanceType), StringComparison.Ordinal) > -1);
        }

        public static int GetNumberofAdvancesInGroup(Player player, Enums.AdvanceGroup group)
        {
            var advancesInScience = GetAdvances().FindAll(m => m.Group == group);
            var count = 0;
            foreach (var advance in advancesInScience)
            {
                if (player.AdvanceString.Contains(advance.Letter))
                {
                    count ++;
                }
            }
            return count;
        }

        public static int TierLevel(Player player)
        {
            var tierNumber = GetNumberofAdvancesInGroup(player, Enums.AdvanceGroup.Civics);
            var nextTierNumber = GetNumberofAdvancesInGroup(player, Enums.AdvanceGroup.Commerce);
            tierNumber = (tierNumber > nextTierNumber) ? nextTierNumber : tierNumber;
            nextTierNumber = GetNumberofAdvancesInGroup(player, Enums.AdvanceGroup.Exploration);
            tierNumber = (tierNumber > nextTierNumber) ? nextTierNumber : tierNumber;  
            nextTierNumber = GetNumberofAdvancesInGroup(player, Enums.AdvanceGroup.Religion);
            tierNumber = (tierNumber > nextTierNumber) ? nextTierNumber : tierNumber; 
            nextTierNumber = GetNumberofAdvancesInGroup(player, Enums.AdvanceGroup.Science);
            tierNumber = (tierNumber > nextTierNumber) ? nextTierNumber : tierNumber;
            nextTierNumber = GetNumberofAdvancesInGroup(player, Enums.AdvanceGroup.Communic);
            tierNumber = (tierNumber > nextTierNumber) ? nextTierNumber : tierNumber;
            return tierNumber;
        }

        public static int GetPriceForAdvance(Player player, string letter, List<string> cardsLastingEffect, List<CardPlay> cardPlays, bool hasInstitutionalResearch,  bool hasWrittenRecord)
        {
            var advance = GetAdvanceFromLetter(letter);
            var price = advance.Cost;
            price -= GetCreditForPastBuys(advance, player.AdvanceString);
            price -= CardHandler.GetDiscountFromLeader(cardPlays,advance.Letter, player.Capital, cardsLastingEffect, hasWrittenRecord);
            if (hasInstitutionalResearch && (advance.Group != Enums.AdvanceGroup.Religion && advance.Group != Enums.AdvanceGroup.Civics))
            {
                price -= 10;
            }
            return (price < 0) ? 0 : price;
        }

        private static int GetCreditForPastBuys(Advance advance, string ownedAdvances)
        {
            var credit = 0;
            foreach (var advanceInGroup in GetAdvances().Where(m => m.Group == advance.Group && m != advance))
            {
                if (ownedAdvances.Contains(advanceInGroup.Letter))
                {
                    credit += advanceInGroup.Credits;
                }
            }
            return credit;
        }

        public static bool IsAdvanceRestrcited(Advance advance, string advanceString)
        {
            if (advance.Requisite == string.Empty)
            {
                return false;
            }
            var letter = advance.Requisite.Substring(0, 1);
            if (!advanceString.Contains(letter))
            {
                return true;
            }
            if (advance.Requisite.Length > 1)
            {
                letter = advance.Requisite.Substring(1, 1);
                if (!advanceString.Contains(letter))
                {
                    return true;
                }
            }
            return false;
        }

        public static int PurchaseMiseryRelief(string letters)
        {
            return letters.Select(letter => GetAdvanceFromLetter(letter.ToString())).Select(advance => advance.MiseryRelief).Sum();
        }
    }
}
