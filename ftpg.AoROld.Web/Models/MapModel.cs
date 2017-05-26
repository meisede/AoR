using System.Collections.Generic;

namespace ftpg.AoR.Web.Models
{
    public class MapModel
    {
        public List<ProvinceModel> Provinces { get; set; }
        public bool EndTurn { get; set; }
    }

    public class ProvinceModel
    {
        public string Name { get; set; }
        public string Good { get; set; }
        public bool IsCoastal { get; set; }
        public int MarketValue { get; set; }
        public int AttackTokensNeeded { get; set; }
        public List<ProvinceModel> Support { get; set; }
        public LocModel Loc { get; set; }
        public List<PresenceModel> Presences { get; set; }
    }

    public class PresenceModel
    {
        public string Capital { get; set; }
        public int Strength { get; set; }
        public int Original { get; set; }
    }

    public class LocModel
    {
        public List<int> Polygon { get; set; }
    }


}