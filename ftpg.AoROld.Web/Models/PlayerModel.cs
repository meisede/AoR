using System.Collections.Generic;
using ftpg.AoR.Entity;

namespace ftpg.AoR.Web.Models
{
    public class PlayerModel
    {
        public string Nick { get; set; }
        public Enums.Capital Capital { get; set; }
        public int Cash { get; set; }
        public int WrittenCash { get; set; }
        public int Tokens { get; set; }
        public int Bid { get; set; }
        public int CapitalOrder { get; set; }
        public int PlayOrder { get; set; }
        public int PlayPosition { get; set; }
        public int ModifiedPlayOrder { get; set; }
        public int Stock { get; set; }
        public int Expansion { get; set; }
        public int Misery { get; set; }
        public bool DidAction { get; set; }
        public bool BoughtCardExpansion { get; set; }
        public Enums.ShipType ShipType { get; set; }
        public List<Card> Cards { get; set; }
        public string AdvanceString { get; set; }
        public string CaptitalLetter()
        {
            return Capital.ToString().Substring(0, 1);
        }
        public string CapitalName { get; set;}


        public string ShipUgradeText()
        {
            switch (ShipType)
            {
                case Enums.ShipType.NoShip:
                    return "Upgrade to Galley 2";
                case Enums.ShipType.Galley2:
                    return "Upgrade to Galley 4";
                case Enums.ShipType.Galley4:
                    return "Upgrade to Galley 6";
                case Enums.ShipType.Galley6:
                    return "Upgrade to Galley 8";
                case Enums.ShipType.SeaWorthy10:
                    return "Upgrade to Sea worthy 12";
                case Enums.ShipType.SeaWorthy12:
                    return "Upgrade to Sea worthy 14";
                case Enums.ShipType.SeaWorthy14:
                    return "Upgrade to Sea worthy 16";
                case Enums.ShipType.OceanGoing1:
                    return "Upgrade to Ocean going 2";
                case Enums.ShipType.OceanGoing2:
                    return "Upgrade to Ocean going 3";
                case Enums.ShipType.OceanGoing3:
                    return "Upgrade to Ocean going 4";
                case Enums.ShipType.Galley8:
                case Enums.ShipType.OceanGoing4:
                case Enums.ShipType.SeaWorthy16:
                default:
                    return "No possible upgrade";
            }
        }

        public string ShipUgradeDisabled()
        {
            return ShipUgradeText() == "No possible upgrade" ? "disabled" : "";
        }
    }
}