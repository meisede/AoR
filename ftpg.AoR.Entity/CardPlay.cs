
using System.Xml.Serialization;

namespace ftpg.AoR.Entity
{
    public class CardPlay
    {
        public Enums.Capital Capital { get; set; }
        public Card Card { get; set; }
        public bool FirstLeaderPlay { get; set; }
        public int Fee { get; set; }
        [XmlIgnore]
        public bool FeePaid { get; set; }
    }
}
