using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ftpg.AoR.Entity;

namespace ftpg.AoR.Entity
{
    public class Advance
    {
        public string Name { get; set; }
        public string Letter { get; set; }
        public Enums.AdvanceType AdvanceType { get; set; }
        public string Description { get; set; }
        public string Requisite { get; set; }
        public int Credits { get; set; }
        public int Cost { get; set; }
        public int MiseryRelief { get; set; }
        
        public Enums.AdvanceGroup Group { get; set; }

        public int MiseryChange
        {
            get
            {
                switch (AdvanceType)
                {
                    case Enums.AdvanceType.HumanBody:
                    case Enums.AdvanceType.ImprovedAgriculture:
                        return -1;
                    case Enums.AdvanceType.Patronage:
                    case Enums.AdvanceType.Cathedral:
                    case Enums.AdvanceType.HolyIndulgence:
                    case Enums.AdvanceType.Proselytism:
                        return 1;
                    default:
                        return 0;
                }
            }
        }

        public bool HasAdvance(string advanceString)
        {
            return advanceString.IndexOf(Letter) > -1;
        }

        public bool IsPapalDecreeRestricted(string banned)
        {
            return Group.ToString() == banned;
        }

        public bool AnyPlayerHasAdvance(List<Player> players)
        {
            return players.Any(m => m.AdvanceString.Contains(Letter));
        }

        public string ShortName
        {
            get
            {
                switch (Name)
                {
                    default:
                    case "The Heavens":
                        return "Heav";
                    case "Human Body":
                        return "HuBo";
                    case "Laws of Matter":
                        return "LaMa";
                    case "Institutional Research":
                        return "InRe";
                    case "Urban Ascendancy":
                        return "UrAs";
                    case "Patronage":
                        return "Patr";
                    case "Nationalism":
                        return "Natio";
                    case "Printed Word":
                        return "PrWo";
                    case "Master Art":
                        return "MaAr";
                    case "Cosmopolitan":
                        return "Cosm";
                    case "Written Record":
                        return "WrRe";
                    case "Holy Indulgence":
                        return "HoIn";
                    case "Cathedral":
                        return "Cath";
                    case "Caravan":
                        return "Cara";
                    case "Improved Agriculture":
                        return "ImAg";
                    case "Wind/Watermill":
                        return "WiWa";
                    case "Interest & Profit":
                        return "InPr";
                    case "Overland East":
                        return "OvEa";
                    case "Seaworthy Vessels":
                        return "SeVe";
                    case "Ocean Navigation":
                        return "OcNa";
                    case "New World":
                        return "NeWo";
                }
            }

        }
    }
}
