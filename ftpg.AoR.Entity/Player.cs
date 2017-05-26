using System.Collections.Generic;
using System.Linq;

namespace ftpg.AoR.Entity
{
    public class Player
    {
        public string Nick { get; set; }
        public Enums.Capital Capital { get; set; }
        public int CapitalOrder { get; set; }
        public int PlayOrder { get; set; }
        public int PlayPosition { get; set; }
        public int Stock { get; set; } 
        public int Cash { get; set; }
        public int WrittenCash { get; set; }
        public int Tokens { get; set; }
        public int Bid { get; set; } 
        public int Misery { get; set; }
        public bool DidAction { get; set; }
        public bool MayUseRenaissance { get; set; }
        public bool MayUseCathedral { get; set; }
        public bool BoughtCardExpansion { get; set; }
        public int Vp { get; set; }
        public Enums.ShipType ShipType { get; set; }
        public string AdvanceString { get; set; }
        public List<Card> Cards { get; set; }

        public Player()
        {
        }

        public Player(string nick)
        {
            Nick = nick;
            Cash = 40;
            CapitalOrder = 0;
            Capital = Enums.Capital.NotSelected;
            Misery = 0;
            Stock = 36;
            Tokens = 0;
            Bid = 0;
            DidAction = false;
            AdvanceString = string.Empty;
            ShipType = Enums.ShipType.NoShip;
            Cards = new List<Card>();
        }

        public string CapitalShortName => Capital.ToString().Substring(0, 2);

        public string HomeArea
        {
            get
            {
                switch (Capital)
                {
                    case Enums.Capital.Venice:
                    case Enums.Capital.Genoa:
                        return "VII";
                    case Enums.Capital.Barcelona:
                        return "IV";
                    case Enums.Capital.Paris:
                        return "III";
                    case Enums.Capital.London:
                        return "II";
                    case Enums.Capital.Hamburg:
                        return "I";
                    default:
                        return "Error";
                }
            }
        }

        private int OceanGoingLevel
        {
            get
            {
                switch (ShipType)
                {
                    case Enums.ShipType.OceanGoing1:
                        return 1;
                    case Enums.ShipType.OceanGoing2:
                        return 2;
                    case Enums.ShipType.OceanGoing3:
                        return 3;
                    case Enums.ShipType.OceanGoing4:
                        return 4;
                    default:
                        return 0;
                }
            }
        }

        public int Stabilization
        {
            get
            {
                var cost = (Cards.Count * (Cards.Count + 1)) / 2;
                if (HasAdvance(Enums.AdvanceType.MiddleClass))
                {
                    cost = cost / 2;
                }
                return cost;
            }
        }

        public int PurchaseMiseryRelief(string letters)
        {
            return AdvanceHandler.PurchaseMiseryRelief(letters);
        }

        public int Income(Map map)
        {
            return NumberOfprovinces(map) + 3;
        }

        public int NumberOfprovinces(Map map)
        {
            return map.Provinces.Sum(province => province.Presences.FindAll(m => m.Capital == Capital).Count(presence => presence.Strength == province.MarketValue && province.MarketValue > 1));
        }

        public bool HasAdvance(Enums.AdvanceType advanceType)
        {
            var letter = AdvanceHandler.GetLetterFromAdvance(advanceType);
            return AdvanceString.Contains(letter);
        }

        public bool HasCardOnHand(string cardName)
        {
            return Cards.Any(m => m.Name == cardName);
        }

        public int GetNumberOfOverseasProvincesDoms(Map map)
        {
            return (from province in map.Provinces from presence in province.Presences where presence.Strength == province.MarketValue && (province.Area == "New World" || province.Area == "Far East") select province).Count();
        }

        public bool IsAtMaximumOverseasProvinces(Map map)
        {
            return (OceanGoingLevel == GetNumberOfOverseasProvincesDoms(map));
        }

        public int TierLevel => AdvanceHandler.TierLevel(this);

        public int WarModifier(List<CardPlay> cardPlays)
        {
            var modifier = HasAdvance(Enums.AdvanceType.Nationalism) ? 1 : 0;
            modifier += cardPlays.Any(m => m.Capital == Capital && m.Card.Name == "Armor") ? 1 : 0;
            modifier += cardPlays.Any(m => m.Capital == Capital && m.Card.Name == "Stirrups") ? 1 : 0;
            modifier += cardPlays.Any(m => m.Capital == Capital && m.Card.Name == "Gunpowder") ? 1 : 0;
            modifier += cardPlays.Any(m => m.Capital == Capital && m.Card.Name == "Long Bow") ? 1 : 0;
            return modifier;
        }

        public List<Province> GetDoms(Map map)
        {
            return map.Provinces.Where(province => province.Presences.Count == 1 && province.Presences[0].Capital == Capital && province.Presences[0].Strength == province.MarketValue).ToList();
        }
    }
}