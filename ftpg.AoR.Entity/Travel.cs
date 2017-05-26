using System.Collections.Generic;

namespace ftpg.AoR.Entity
{
    public class Travel
    {
        public class Distances
        {
            public string Province;
            public List<string> Neighbour;
            public List<string> Caravan;
            public List<string> Galley2;
            public List<string> Galley4;
            public List<string> Galley6;
            public List<string> Galley8;

            public Distances(string province, List<string> neighbour, List<string> caravan, List<string> galley2,
                List<string> galley4, List<string> galley6, List<string> galley8)
            {
                Province = province;
                Neighbour = neighbour;
                Caravan = caravan;
                Galley2 = galley2;
                Galley4 = galley4;
                Galley6 = galley6;
                Galley8 = galley8;
            }
        }

        public List<Distances> Routes { get; set; }


        
        public static bool LegalTravel(string from, string to, Player player, int noPlayers)
        {

            return true;
        }

        private void Populate()
        {
            Routes = new List<Distances>();
            Routes.Add(new Distances("Barcelona", new List<string>{"Valencia", "Toledo", "Basque", "Toulouse", "Montpelier"}, new List<string> {"Marseiles"}, new List<string> { }, new List<string> { }, new List<string> { }, new List<string> { }));
        }
        
    }
}
