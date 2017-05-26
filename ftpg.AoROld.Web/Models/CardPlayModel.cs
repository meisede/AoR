using System;
using System.Collections.Generic;
using ftpg.AoR.Entity;


namespace ftpg.AoR.Web.Models
{
    public class CardPlayModel
    {
        public string Name { get; set; }
        public Enums.Capital Capital { get; set; }
        public bool Protected { get; set; }
        public int Discount { get; set; }
        public int Fee { get; set; }
        public string Text { get; set; }
        public List <string> AdvanceLetters { get; set; }
        public bool Used { get; set; }

    }
}