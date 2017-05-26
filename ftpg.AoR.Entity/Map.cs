using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;

namespace ftpg.AoR.Entity
{
    public class Map
    {
        public List<Province> Provinces { get; set; }

        [XmlIgnore]
        public bool EndTurn { get; set; }

        public Map(){}

        public Map(int numberOfPlayers)
        {
            // Area I
            Provinces = new List<Province>();
            if (numberOfPlayers == 6)
            {
                var hamburg = new Province(Enums.Pname.Hamburg);
                hamburg.Presences.Add(new Presence {Capital = Enums.Capital.Hamburg, Original = 4, Strength = 4});
                Provinces.Add(hamburg);
                Provinces.Add(new Province(Enums.Pname.Lubeck));
                Provinces.Add(new Province(Enums.Pname.Stettin));
                Provinces.Add(new Province(Enums.Pname.Danzig));
                Provinces.Add(new Province(Enums.Pname.Mitau));
                Provinces.Add(new Province(Enums.Pname.Riga));
                Provinces.Add(new Province(Enums.Pname.Novgorod));
                Provinces.Add(new Province(Enums.Pname.Stockholm));
                Provinces.Add(new Province(Enums.Pname.Whisby));
                Provinces.Add(new Province(Enums.Pname.Malmo));
                Provinces.Add(new Province(Enums.Pname.Copenhagen));
            }

            // Area II
            if (numberOfPlayers > 4)
            {
                var london = new Province(Enums.Pname.London);
                london.Presences.Add(new Presence {Capital = Enums.Capital.London, Original = 5, Strength = 5});
                Provinces.Add(london);
                Provinces.Add(new Province(Enums.Pname.Portsmouth));
                Provinces.Add(new Province(Enums.Pname.Cornwall));
                Provinces.Add(new Province(Enums.Pname.York));
                Provinces.Add(new Province(Enums.Pname.Chester));
                Provinces.Add(new Province(Enums.Pname.Wales));
                Provinces.Add(new Province(Enums.Pname.Edinburg));
                Provinces.Add(new Province(Enums.Pname.Armagh));
                Provinces.Add(new Province(Enums.Pname.Waterford));
                Provinces.Add(new Province(Enums.Pname.Iceland));
                Provinces.Add(new Province(Enums.Pname.ShetlandIslands));
                Provinces.Add(new Province(Enums.Pname.Bergen));
                Provinces.Add(new Province(Enums.Pname.Kongsberg));
            }

            // Area III
            if (numberOfPlayers > 3)
            {
                var paris = new Province(Enums.Pname.Paris);
                paris.Presences.Add(new Presence {Capital = Enums.Capital.Paris, Original = 4, Strength = 4});
                Provinces.Add(paris);
                Provinces.Add(new Province(Enums.Pname.Loire));
                Provinces.Add(new Province(Enums.Pname.StMalo));
                Provinces.Add(new Province(Enums.Pname.Bruges));
                Provinces.Add(new Province(Enums.Pname.Amsterdam));
                Provinces.Add(new Province(Enums.Pname.Cologne));
                Provinces.Add(new Province(Enums.Pname.Strasbourg));
                Provinces.Add(new Province(Enums.Pname.Dijon));
                Provinces.Add(new Province(Enums.Pname.Basel));
                Provinces.Add(new Province(Enums.Pname.StGali));
                Provinces.Add(new Province(Enums.Pname.Nuremberg));
                Provinces.Add(new Province(Enums.Pname.Prague));
                Provinces.Add(new Province(Enums.Pname.Breslau));
                Provinces.Add(new Province(Enums.Pname.Salzburg));
                Provinces.Add(new Province(Enums.Pname.Vienna));
                Provinces.Add(new Province(Enums.Pname.Budapest));
                Provinces.Add(new Province(Enums.Pname.Esseg));
            }


            // Area IV
            var barcelona = new Province(Enums.Pname.Barcelona);
            barcelona.Presences.Add(new Presence { Capital = Enums.Capital.Barcelona, Original = 3, Strength = 3 });
            Provinces.Add(barcelona);
            Provinces.Add(new Province(Enums.Pname.Basque));
            Provinces.Add(new Province(Enums.Pname.Leon));
            Provinces.Add(new Province(Enums.Pname.Valencia));
            Provinces.Add(new Province(Enums.Pname.Toledo));
            Provinces.Add(new Province(Enums.Pname.Lisbon));
            Provinces.Add(new Province(Enums.Pname.Palma));

            // Area V
            Provinces.Add(new Province(Enums.Pname.Abasgia));
            Provinces.Add(new Province(Enums.Pname.Angora));
            Provinces.Add(new Province(Enums.Pname.Erzerum));
            Provinces.Add(new Province(Enums.Pname.Kaffa));
            Provinces.Add(new Province(Enums.Pname.Kamishin));
            Provinces.Add(new Province(Enums.Pname.Kiev));
            Provinces.Add(new Province(Enums.Pname.Poti));
            Provinces.Add(new Province(Enums.Pname.Sarai));
            Provinces.Add(new Province(Enums.Pname.Tana));
            Provinces.Add(new Province(Enums.Pname.Trezibond));
            Provinces.Add(new Province(Enums.Pname.Varna));


            // Area VII
            var genoa = new Province(Enums.Pname.Genoa);
            genoa.Presences.Add(new Presence { Capital = Enums.Capital.Genoa, Original = 5, Strength = 5 });
            Provinces.Add(genoa);
            var venice = new Province(Enums.Pname.Venice);
            venice.Presences.Add(new Presence { Capital = Enums.Capital.Venice, Original = 5, Strength = 5 });
            Provinces.Add(venice);
            Provinces.Add(new Province(Enums.Pname.Bordeaux));
            Provinces.Add(new Province(Enums.Pname.Toulouse));
            Provinces.Add(new Province(Enums.Pname.Montpelier));
            Provinces.Add(new Province(Enums.Pname.Lyons));
            Provinces.Add(new Province(Enums.Pname.Marseilles));
            Provinces.Add(new Province(Enums.Pname.Milan));
            Provinces.Add(new Province(Enums.Pname.Florence));
            Provinces.Add(new Province(Enums.Pname.Rome));
            Provinces.Add(new Province(Enums.Pname.Naples));
            Provinces.Add(new Province(Enums.Pname.Bari));
            Provinces.Add(new Province(Enums.Pname.Sicily));
            Provinces.Add(new Province(Enums.Pname.Cagliari));
            Provinces.Add(new Province(Enums.Pname.Dubrovnik));
            Provinces.Add(new Province(Enums.Pname.Belgrade));

            // Aera VI
            Provinces.Add(new Province(Enums.Pname.Acre));
            Provinces.Add(new Province(Enums.Pname.Adalia));
            Provinces.Add(new Province(Enums.Pname.Aleppo));
            Provinces.Add(new Province(Enums.Pname.Alexandria));
            Provinces.Add(new Province(Enums.Pname.Cairo));
            Provinces.Add(new Province(Enums.Pname.Cyprus));
            Provinces.Add(new Province(Enums.Pname.Jerusalem));
            Provinces.Add(new Province(Enums.Pname.Levant));
            Provinces.Add(new Province(Enums.Pname.Libya));
            Provinces.Add(new Province(Enums.Pname.Suez));
            Provinces.Add(new Province(Enums.Pname.Tarsus));

            // Area VIII
            Provinces.Add(new Province(Enums.Pname.Algiers));
            Provinces.Add(new Province(Enums.Pname.Athens));
            Provinces.Add(new Province(Enums.Pname.Barca));
            Provinces.Add(new Province(Enums.Pname.Constantinople));
            Provinces.Add(new Province(Enums.Pname.Corfu));
            Provinces.Add(new Province(Enums.Pname.Crete));
            Provinces.Add(new Province(Enums.Pname.Durazzo));
            Provinces.Add(new Province(Enums.Pname.Fez));
            Provinces.Add(new Province(Enums.Pname.Gallipoli));
            Provinces.Add(new Province(Enums.Pname.Granada));
            Provinces.Add(new Province(Enums.Pname.Oran));
            Provinces.Add(new Province(Enums.Pname.Salonika));
            Provinces.Add(new Province(Enums.Pname.Seville));
            Provinces.Add(new Province(Enums.Pname.Smyrna));
            Provinces.Add(new Province(Enums.Pname.Tripoli));
            Provinces.Add(new Province(Enums.Pname.Tunis));

            // Area VIII
            Provinces.Add(new Province(Enums.Pname.China));
            Provinces.Add(new Province(Enums.Pname.India));
            Provinces.Add(new Province(Enums.Pname.EastIndies));
            Provinces.Add(new Province(Enums.Pname.NorthAmerica));
            Provinces.Add(new Province(Enums.Pname.SouthAmerica));
        }

        public Province FindProvinceWithCompetition()
        {
            foreach (var province in Provinces)
            {
                var strengthCount = 0;
                if (province.Presences != null)
                {
                    foreach (var presence in province.Presences)
                    {
                        strengthCount += presence.Strength;
                    }
                    if (strengthCount > province.MarketValue)
                    {
                        return province;
                    }
                }
            }
            return null;
        }

        public int GetPlayerProvincesWithGood(Player player, Enums.GoodType good)
        {
            var count = 0;
            foreach (var province in Provinces.Where(m=>m.Good == good))
            {
                {
                    foreach (var presence in province.Presences)
                    {
                        if (presence.Capital == player.Capital && presence.Strength == province.MarketValue)
                        {
                            count ++;
                        }
                    }
                }
            }
            return count;
        }

        public Province GetProvinceByName(string name)
        {
            return Provinces.Find(m => m.NameAsText == name);
        }

        //public bool Add(Enums.Capital capital, Province province, int strength)
        //{
        //    var presentStrength = province.Presences.Sum(presence => presence.Strength);
        //    if (province.MarketValue <= presentStrength + strength)
        //    {
        //        AddVerified(capital, province, strength);
        //        return true;
        //    }
        //    return false;
        //}

        public void AddDomOrSatelite(Enums.Capital capital, string provinceName)
        {
            var province = this.Provinces.Find(m => m.NameAsText == provinceName);
            AddTokensToProvince(capital, province, province.MarketValue);
        }

        private static void AddTokensToProvince(Enums.Capital capital, Province province, int strength)
        {
            if (province.Presences.Exists(m => m.Capital == capital))
            {
                province.Presences.Find(m => m.Capital == capital).Strength += strength;
            }
            else
            {
                province.Presences.Add(new Presence { Capital = capital, Original = strength, Strength = strength });
            }
        }

        public int GetPlayerNoDoms(Enums.Capital capital)
        {
            return (from province in Provinces from presence in province.Presences where presence.Capital == capital && presence.Strength == province.MarketValue select province).Count();
        }

        public int GetPlayerNoSatelites(Enums.Capital capital)
        {
            return (from province in Provinces from presence in province.Presences where presence.Capital == capital && presence.Strength == 1 select province).Count();
        }
    }
}
