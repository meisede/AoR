namespace ftpg.AoR.Entity
{
    public static class ShipHandler
    {
        public static Enums.ShipType GetShipUgrade(Enums.ShipType current)
        {
            switch (current)
            {
                case Enums.ShipType.NoShip:
                    return Enums.ShipType.Galley2;
                case Enums.ShipType.Galley2:
                    return Enums.ShipType.Galley4;
                case Enums.ShipType.Galley4:
                    return Enums.ShipType.Galley6;
                case Enums.ShipType.Galley6:
                    return Enums.ShipType.Galley8;
                case Enums.ShipType.OceanGoing1:
                    return Enums.ShipType.OceanGoing2;
                case Enums.ShipType.OceanGoing2:
                    return Enums.ShipType.OceanGoing3;
                case Enums.ShipType.OceanGoing3:
                    return Enums.ShipType.OceanGoing4;
                case Enums.ShipType.SeaWorthy10:
                    return Enums.ShipType.SeaWorthy10;
                case Enums.ShipType.SeaWorthy12:
                    return Enums.ShipType.SeaWorthy14;
                case Enums.ShipType.SeaWorthy14:
                    return Enums.ShipType.SeaWorthy16;
            }
            return Enums.ShipType.NoShip;
        }

        public static string GetShipTypeText(Enums.ShipType shipType)
        {
            switch (shipType)
            {
                default:
                case Enums.ShipType.NoShip:
                    return "No ship";
                case Enums.ShipType.Galley2:
                    return "Galley 2";
                case Enums.ShipType.Galley4:
                    return "Galley 4";
                case Enums.ShipType.Galley6:
                    return "Galley 6";
                case Enums.ShipType.Galley8:
                    return "Galley 8";
                case Enums.ShipType.SeaWorthy10:
                    return "SeaWorthy 10";
                case Enums.ShipType.SeaWorthy12:
                    return "SeaWorthy 12";
                case Enums.ShipType.SeaWorthy14:
                    return "SeaWorthy 14";
                case Enums.ShipType.SeaWorthy16:
                    return "SeaWorthy 16";
                case Enums.ShipType.OceanGoing1:
                    return "OceanGoing 1";
                case Enums.ShipType.OceanGoing2:
                    return "OceanGoing 2";
                case Enums.ShipType.OceanGoing3:
                    return "OceanGoing 3";
                case Enums.ShipType.OceanGoing4:
                    return "OceanGoing 4";
            }
        }
        public static Enums.ShipType GetShipTypeFromText(string shipType)
        {
            switch (shipType.ToLower().Replace(" ", string.Empty))
            {
                default:
                case "noship":
                    return Enums.ShipType.NoShip;
                case "galley2":
                    return Enums.ShipType.Galley2;
                case "galley4":
                    return Enums.ShipType.Galley4;
                case "galley6":
                    return Enums.ShipType.Galley6;
                case "galley8":
                    return Enums.ShipType.Galley8;
                case "seaworthy10":
                    return Enums.ShipType.SeaWorthy10;
                case "seaworthy12":
                    return Enums.ShipType.SeaWorthy12;
                case "seaworthy14":
                    return Enums.ShipType.SeaWorthy14;
                case "seaworthy16":
                    return Enums.ShipType.SeaWorthy16;
                case "oceangoing1":
                    return Enums.ShipType.OceanGoing1;
                case "oceangoing2":
                    return Enums.ShipType.OceanGoing2;
                case "oceangoing3":
                    return Enums.ShipType.OceanGoing3;
                case "oceangoing4":
                    return Enums.ShipType.OceanGoing4;
            }
        }
    }
}
