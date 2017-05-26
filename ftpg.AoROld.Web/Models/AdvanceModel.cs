using System.Collections.Generic;

namespace ftpg.AoR.Web.Models
{
    public class AdvanceModel
    {
        public List<bool> OtherNations { get; set; } 
        public string Letter { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool PreOwned { get; set; }
        public bool Purchased { get; set; }
        public int FullCost { get; set; }
        public int Cost { get; set; }
        public int OriginalCost { get; set; }
        public int Credits { get; set; }
        public string Requisite { get; set; }
        public bool Restricted { get; set; }
        public string Group { get; set; }
        public int MiseryRelief { get; set; }
        public int MiseryChange { get; set; }
    }
}