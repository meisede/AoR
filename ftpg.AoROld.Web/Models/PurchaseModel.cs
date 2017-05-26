using System.Collections.Generic;
using System.Linq;
using Microsoft.Ajax.Utilities;

namespace ftpg.AoR.Web.Models
{
    public class PurchaseModel
    {
        public int OriginalCash { get; set; }
        public int Cash { get; set; }
        public int Misery { get; set; }
        public int MiseryRelief { get; set; }
        public int MiseryReliefSpent { get; set; }
        public int OriginalMisery { get; set; }
        public List<AdvanceModel> Advances { get; set; }
        public bool ShipUpgrade { get; set; }
        public bool Stabilization { get; set; }
        public int StabilzationCost { get; set; }
        public int StabilzationMiseryPenalty { get; set; }
        public int TierLevel { get; set; }
        public int OriginalTierLevel { get; set; }
        public List<CardPlayModel> CardPlays { get; set; }

        public bool HasAdvance(string letter)
        {
            return Advances.Any(advance => advance.Letter == letter && (advance.PreOwned || advance.Purchased));
        }
    }
}