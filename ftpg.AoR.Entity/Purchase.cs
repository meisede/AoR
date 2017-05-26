using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ftpg.AoR.Entity
{
    public class Purchase
    {
        public bool ShipUpgrade { get; set; }
        public List<string> LeadersUsed { get; set; }
        public string AdvancesPurchased { get; set; }
        public bool Stabilization { get; set; }
        public int TierLevel { get; set; }
    }
}
