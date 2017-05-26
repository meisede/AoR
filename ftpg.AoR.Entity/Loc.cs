using System.Collections.Generic;

namespace ftpg.AoR.Entity
{
    public class Loc
    {
        public List<int> Polygon { get; set; }

        public Loc(List<int> polygon)
        {
            Polygon = polygon;
        }
    }
}
