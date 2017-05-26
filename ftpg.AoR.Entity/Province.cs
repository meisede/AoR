using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace ftpg.AoR.Entity
{
    public class Province
    {
        public Enums.Pname Name {get; set; }
        public List<Presence> Presences { get; set; }

        public Province(){}

        public Province(Enums.Pname name)
        {
            Name = name;
            Presences = new List<Presence>();
        }

        public string NameText
        {
           get
        {
            switch (Name)
            {

                    default:
                        return "";
                }
            }
        }
        public bool IsCoastal
        {
            get
            {
                switch (Name)
                {
                    case Enums.Pname.Toledo:
                    case Enums.Pname.Lyons:
                    case Enums.Pname.Milan:
                    case Enums.Pname.Dijon:
                    case Enums.Pname.Basel:
                    case Enums.Pname.Strasbourg:
                    case Enums.Pname.Cologne:
                    case Enums.Pname.Nuremberg:
                    case Enums.Pname.Prague:
                    case Enums.Pname.Vienna:
                    case Enums.Pname.Budapest:
                        return false;
                    default:
                        return true;
                }
            }
        }

        public static Enums.Pname NameToEnum(string name)
        {
            switch (name)
            {
                case "WestAfrica":
                case "West Africa":
                    return Enums.Pname.WestAfrica;
                case "Hamburg":
                    return Enums.Pname.Hamburg;
                case "Lubeck":
                    return Enums.Pname.Lubeck;
                case "Stettin":
                    return Enums.Pname.Stettin;
                case "Danzig":
                    return Enums.Pname.Danzig;
                case "Mitau":
                    return Enums.Pname.Mitau;
                case "Riga":
                    return Enums.Pname.Riga;
                case "Novgorod":
                    return Enums.Pname.Novgorod;
                case "Stockholm":
                    return Enums.Pname.Stockholm;
                case "Whisby":
                    return Enums.Pname.Whisby;
                case "Malmo":
                    return Enums.Pname.Malmo;
                case "Copenhagen":
                    return Enums.Pname.Copenhagen;
                case "London":
                    return Enums.Pname.London;
                case "Portsmouth":
                    return Enums.Pname.Portsmouth;
                case "Cornwall":
                    return Enums.Pname.Cornwall;
                case "York":
                    return Enums.Pname.York;
                case "Chester":
                    return Enums.Pname.Chester;
                case "Wales":
                    return Enums.Pname.Wales;
                case "Edinburg":
                    return Enums.Pname.Edinburg;
                case "Armagh":
                    return Enums.Pname.Armagh;
                case "Waterford":
                    return Enums.Pname.Waterford;
                case "Iceland":
                    return Enums.Pname.Iceland;
                case "Shetlandslands":
                case "Shetland Islands":
                    return Enums.Pname.ShetlandIslands;
                case "Bergen":
                    return Enums.Pname.Bergen;
                case "Kongsberg":
                    return Enums.Pname.Kongsberg;
                case "Paris":
                    return Enums.Pname.Paris;
                case "Loire":
                    return Enums.Pname.Loire;
                case "StMalo":
                case "St. Malo":
                    return Enums.Pname.StMalo;
                case "Bruges":
                    return Enums.Pname.Bruges;
                case "Amsterdam":
                    return Enums.Pname.Amsterdam;
                case "Cologne":
                    return Enums.Pname.Cologne;
                case "Strasbourg":
                    return Enums.Pname.Strasbourg;
                case "Dijon":
                    return Enums.Pname.Dijon;
                case "Basel":
                    return Enums.Pname.Basel;
                case "StGali":
                case "St. Gali":
                    return Enums.Pname.StGali;
                case "Nuremberg":
                    return Enums.Pname.Nuremberg;
                case "Prague":
                    return Enums.Pname.Prague;
                case "Breslau":
                    return Enums.Pname.Breslau;
                case "Salzburg":
                    return Enums.Pname.Salzburg;
                case "Vienna":
                    return Enums.Pname.Vienna;
                case "Budapest":
                    return Enums.Pname.Budapest;
                case "Esseg":
                    return Enums.Pname.Esseg;
                case "Barcelona":
                    return Enums.Pname.Barcelona;
                case "Basque":
                    return Enums.Pname.Basque;
                case "Leon":
                    return Enums.Pname.Leon;
                case "Valencia":
                    return Enums.Pname.Valencia;
                case "Toledo":
                    return Enums.Pname.Toledo;
                case "Lisbon":
                    return Enums.Pname.Lisbon;
                case "Palma":
                    return Enums.Pname.Palma;
                case "Abasgia":
                    return Enums.Pname.Abasgia;
                case "Angora":
                    return Enums.Pname.Angora;
                case "Erzerum":
                    return Enums.Pname.Erzerum;
                case "Kaffa":
                    return Enums.Pname.Kaffa;
                case "Kamishin":
                    return Enums.Pname.Kamishin;
                case "Kiev":
                    return Enums.Pname.Kiev;
                case "Poti":
                    return Enums.Pname.Poti;
                case "Sarai":
                    return Enums.Pname.Sarai;
                case "Tana":
                    return Enums.Pname.Tana;
                case "Trezibond":
                    return Enums.Pname.Trezibond;
                case "Varna":
                    return Enums.Pname.Varna;
                case "Genoa":
                    return Enums.Pname.Genoa;
                case "Venice":
                    return Enums.Pname.Venice;
                case "Bordeaux":
                    return Enums.Pname.Bordeaux;
                case "Toulouse":
                    return Enums.Pname.Toulouse;
                case "Montpelier":
                    return Enums.Pname.Montpelier;
                case "Lyons":
                    return Enums.Pname.Lyons;
                case "Marseilles":
                    return Enums.Pname.Marseilles;
                case "Milan":
                    return Enums.Pname.Milan;
                case "Florence": 
                    return Enums.Pname.Florence;
                case "Rome":
                    return Enums.Pname.Rome;
                case "Naples":
                    return Enums.Pname.Naples;
                case "Bari":
                    return Enums.Pname.Bari;
                case "Sicily":
                    return Enums.Pname.Sicily;
                case "Cagliari":
                    return Enums.Pname.Cagliari;
                case "Dubrovnik":
                    return Enums.Pname.Dubrovnik;
                case "Belgrade":
                    return Enums.Pname.Belgrade;
                case "Acre": 
                    return Enums.Pname.Acre;
                case "Adalia":
                    return Enums.Pname.Adalia;
                case "Aleppo":
                    return Enums.Pname.Aleppo;
                case "Alexandria":
                    return Enums.Pname.Alexandria;
                case "Cairo":
                    return Enums.Pname.Cairo;
                case "Cyprus":
                    return Enums.Pname.Cyprus;
                case "Jerusalem":
                    return Enums.Pname.Jerusalem;
                case "Levant":
                    return Enums.Pname.Levant;
                case "Libya":
                    return Enums.Pname.Libya;
                case "Suez":
                    return Enums.Pname.Suez;
                case "Tarsus":
                    return Enums.Pname.Tarsus;
                case "Algiers":
                    return Enums.Pname.Algiers;
                case "Athens":
                    return Enums.Pname.Athens;
                case "Barca":
                    return Enums.Pname.Barca;
                case "Constantinople":
                    return Enums.Pname.Constantinople;
                case "Corfu":
                    return Enums.Pname.Corfu;
                case "Crete":
                    return Enums.Pname.Crete;
                case "Durazzo":
                    return Enums.Pname.Durazzo;
                case "Fez":
                    return Enums.Pname.Fez;
                case "Gallipoli":
                    return Enums.Pname.Gallipoli;
                case "Granada":
                    return Enums.Pname.Granada;
                case "Oran":
                    return Enums.Pname.Oran;
                case "Salonika":
                    return Enums.Pname.Salonika;
                case "Seville":
                    return Enums.Pname.Seville;
                case "Smyrna":
                    return Enums.Pname.Smyrna;
                case "Tripoli":
                    return Enums.Pname.Tripoli;
                case "Tunis":
                    return Enums.Pname.Tunis;
                case "China":
                    return Enums.Pname.China;
                case "India":
                    return Enums.Pname.India;
                case "EastIndies":
                case "East Indies":
                    return Enums.Pname.EastIndies;
                case "NorthAmerica":
                case "North America":
                    return Enums.Pname.NorthAmerica;
                case "SouthAmerica":
                case "South America":
                    return Enums.Pname.SouthAmerica;
                default:
                    return Enums.Pname.Unknown;
            }
        }

        public string NameAsText
        {
            get
            {
                switch (Name)
                {
                    case Enums.Pname.ShetlandIslands:
                        return "Shetland Islands";
                    case Enums.Pname.StMalo:
                        return "St. Malo";
                    case Enums.Pname.StGali:
                        return "St. Gali";
                    case Enums.Pname.EastIndies:
                        return "East Indies";
                    case Enums.Pname.NorthAmerica:
                        return "North America";
                    case Enums.Pname.SouthAmerica:
                        return "South America";
                    case Enums.Pname.WestAfrica:
                        return "West Africa";
                    default:
                        return Name.ToString();
                }
            }
        }

        public bool IsNotNewWorldOrCapital
        {
            get
            {
                switch (Name)
                {
                    case Enums.Pname.SouthAmerica:
                    case Enums.Pname.NorthAmerica:
                    case Enums.Pname.Genoa:
                    case Enums.Pname.Venice:
                    case Enums.Pname.Barcelona:
                    case Enums.Pname.Paris:
                    case Enums.Pname.London:
                    case Enums.Pname.Hamburg:
                        return false;
                    default:
                        return true;
                }
            }
        }

        public Enums.GoodType Good
        {
            get
            {
                switch (Name)
                {
                    case Enums.Pname.Milan:
                    case Enums.Pname.Montpelier:
                    case Enums.Pname.Lubeck:
                    case Enums.Pname.Naples:
                    case Enums.Pname.Paris:
                    case Enums.Pname.Rome:
                    case Enums.Pname.Vienna:
                        return Enums.GoodType.Stone;
                    case Enums.Pname.Edinburg:
                    case Enums.Pname.York:
                    case Enums.Pname.London:
                    case Enums.Pname.Waterford:
                    case Enums.Pname.Basque:
                    case Enums.Pname.Toledo:
                    case Enums.Pname.Algiers:
                    case Enums.Pname.Smyrna:
                    case Enums.Pname.Angora:
                        return Enums.GoodType.Wool;
                    case Enums.Pname.Riga:
                    case Enums.Pname.Bergen:
                    case Enums.Pname.Hamburg:
                    case Enums.Pname.Bordeaux:
                    case Enums.Pname.Dubrovnik:
                    case Enums.Pname.Fez:
                    case Enums.Pname.Poti:
                        return Enums.GoodType.Timber;
                    case Enums.Pname.Danzig:
                    case Enums.Pname.Portsmouth:
                    case Enums.Pname.Dijon:
                    case Enums.Pname.Belgrade:
                    case Enums.Pname.Seville:
                    case Enums.Pname.Sicily:
                    case Enums.Pname.Kiev:
                        return Enums.GoodType.Grain;
                    case Enums.Pname.Cologne:
                    case Enums.Pname.Bruges:
                    case Enums.Pname.Budapest:
                    case Enums.Pname.Venice:
                    case Enums.Pname.Florence:
                    case Enums.Pname.Genoa:
                    case Enums.Pname.Tunis:
                        return Enums.GoodType.Cloth;
                    case Enums.Pname.Marseilles:
                    case Enums.Pname.Barcelona:
                    case Enums.Pname.Lisbon:
                    case Enums.Pname.Crete:
                    case Enums.Pname.Cyprus:
                        return Enums.GoodType.Wine;
                    case Enums.Pname.Stockholm:
                    case Enums.Pname.Chester:
                    case Enums.Pname.Nuremberg:
                    case Enums.Pname.Lyons:
                    case Enums.Pname.Granada:
                    case Enums.Pname.Constantinople:
                    case Enums.Pname.Strasbourg:
                        return Enums.GoodType.Metal;
                    case Enums.Pname.Basel:
                    case Enums.Pname.Novgorod:
                    case Enums.Pname.Varna:
                    case Enums.Pname.Tana:
                        return Enums.GoodType.Fur;
                    case Enums.Pname.Valencia:
                    case Enums.Pname.Salonika:
                    case Enums.Pname.Aleppo:
                    case Enums.Pname.Erzerum:
                        return Enums.GoodType.Silk;
                    case Enums.Pname.Trezibond:
                    case Enums.Pname.Acre:
                    case Enums.Pname.Alexandria:
                        return Enums.GoodType.Spice;
                    case Enums.Pname.Prague:
                    case Enums.Pname.Sarai:
                    case Enums.Pname.Suez:
                        return Enums.GoodType.Gold;
                    case Enums.Pname.Cairo:
                    case Enums.Pname.Tripoli:
                    case Enums.Pname.WestAfrica:
                        return Enums.GoodType.Ivory;
                    default:
                        return Enums.GoodType.NoGood;
                }
            }
        }

        public int MarketValue
        {
            get
            {
                switch (Name)
                {
                    case Enums.Pname.NorthAmerica:
                    case Enums.Pname.SouthAmerica:
                        return 6;
                    case Enums.Pname.Danzig:
                    case Enums.Pname.London:
                    case Enums.Pname.Portsmouth:
                    case Enums.Pname.Marseilles:
                    case Enums.Pname.Genoa:
                    case Enums.Pname.Naples:
                    case Enums.Pname.Venice:
                    case Enums.Pname.Alexandria:
                    case Enums.Pname.Acre:
                    case Enums.Pname.EastIndies:
                    case Enums.Pname.China:
                    case Enums.Pname.India:
                        return 5;
                    case Enums.Pname.Hamburg:
                    case Enums.Pname.Paris:
                    case Enums.Pname.Bruges:
                    case Enums.Pname.Cologne:
                    case Enums.Pname.Vienna:
                    case Enums.Pname.Bordeaux:
                    case Enums.Pname.Lyons:
                    case Enums.Pname.Florence:
                    case Enums.Pname.Rome:
                    case Enums.Pname.Tunis:
                    case Enums.Pname.Constantinople:
                    case Enums.Pname.Cairo:
                    case Enums.Pname.Aleppo:
                    case Enums.Pname.Trezibond:
                        return 4;
                    case Enums.Pname.Lubeck:
                    case Enums.Pname.Stockholm:
                    case Enums.Pname.Bergen:
                    case Enums.Pname.York:
                    case Enums.Pname.Chester:
                    case Enums.Pname.Dijon:
                    case Enums.Pname.Strasbourg:
                    case Enums.Pname.Basel:
                    case Enums.Pname.Nuremberg:
                    case Enums.Pname.Budapest:
                    case Enums.Pname.Milan:
                    case Enums.Pname.Dubrovnik:
                    case Enums.Pname.Barcelona:
                    case Enums.Pname.Toledo:
                    case Enums.Pname.Seville:
                    case Enums.Pname.Lisbon:
                    case Enums.Pname.Suez:
                    case Enums.Pname.Cyprus:
                    case Enums.Pname.Erzerum:
                        return 3;
                    case Enums.Pname.Riga:
                    case Enums.Pname.Novgorod:
                    case Enums.Pname.Edinburg:
                    case Enums.Pname.Waterford:
                    case Enums.Pname.Prague:
                    case Enums.Pname.StMalo:
                    case Enums.Pname.Montpelier:
                    case Enums.Pname.Belgrade:
                    case Enums.Pname.Basque:
                    
                    case Enums.Pname.Valencia:
                    case Enums.Pname.Granada:
                    case Enums.Pname.Fez:
                    case Enums.Pname.Algiers:
                    case Enums.Pname.Tripoli:
                    case Enums.Pname.Sicily:
                    case Enums.Pname.Salonika:
                    case Enums.Pname.Smyrna:
                    case Enums.Pname.Angora:
                    case Enums.Pname.Poti:
                    case Enums.Pname.Tana:
                    case Enums.Pname.Sarai:
                    case Enums.Pname.Varna:
                    case Enums.Pname.Kiev:
                        return 2;
                    default:
                        return 1;
                }
            }
        }

        // To:do Update string to province names
        public List<string> Support
        {
            get
            {
                switch (Name)
                {
                    // Area 1
                    case Enums.Pname.Hamburg:
                        return new List<string>{"Amsterdam", "Copenhagen"};
                    case Enums.Pname.Danzig:
                        return new List<string> { "Mitau", "Breslau", "Stettin" };
                    case Enums.Pname.Lubeck:
                        return new List<string> { "Copenhagen", "Stettin"};
                    case Enums.Pname.Riga:
                        return new List<string> { "Mitau" };
                    case Enums.Pname.Stockholm:
                        return new List<string> {"Whisby", "Malmo" };

                    // Area II
                    case Enums.Pname.Portsmouth:
                        return new List<string> {"Cornwall" };
                    case Enums.Pname.Chester:
                        return new List<string> {"Cornwall", "Wales" };
                    case Enums.Pname.Waterford:
                        return new List<string> {"Wales", "Armagh" };
                    case Enums.Pname.Edinburg:
                        return new List<string> {"Armagh", "Shetland Islands" };
                    case Enums.Pname.Bergen:
                        return new List<string> { "Shetland Islands", "Kongsberg"};

                    // Area III
                    case Enums.Pname.Bruges:
                        return new List<string> {"Amsterdam" };
                    case Enums.Pname.Cologne:
                        return new List<string> { "Amsterdam" };
                    case Enums.Pname.Prague:
                        return new List<string> {"Stettin", "Breslau", "Salzburg" };
                    case Enums.Pname.Nuremberg:
                        return new List<string> {"Salzburg" };
                    case Enums.Pname.Vienna:
                        return new List<string> { "Breslau", "Salzburg", "Esseg"};
                    case Enums.Pname.Budapest:
                        return new List<string> { "Esseg"};
                    case Enums.Pname.Strasbourg:
                        return new List<string> {"St. Gali" };
                    case Enums.Pname.Basel:
                        return new List<string> { "St. Gali"};

                    // Area IV
                    case Enums.Pname.Basque:
                        return new List<string> { "Toulouse", "Leon"};
                    case Enums.Pname.Barcelona:
                        return new List<string> { "Toulouse", "Palma"};
                    case Enums.Pname.Toledo:
                        return new List<string> { "Leon" };
                    case Enums.Pname.Lisbon:
                        return new List<string> { "Leon" };
                    case Enums.Pname.Valencia:
                        return new List<string> {"Palma" };

                    // Area V
                    case Enums.Pname.Kiev:
                        return new List<string> { "Kaffa"};
                    case Enums.Pname.Tana:
                        return new List<string> { "Kaffa", "Kamishin", "Abasgia"};
                    case Enums.Pname.Varna:
                        return new List<string> { "Kaffa" , "Gallipoli"};
                    case Enums.Pname.Sarai:
                        return new List<string> {"Kamishin" };
                    case Enums.Pname.Poti:
                        return new List<string> { "Abasgia" };

                    // Area VI
                    case Enums.Pname.Aleppo:
                        return new List<string> {"Tarsus", "Levant" };
                    case Enums.Pname.Cyprus:
                        return new List<string> { "Tarsus", "Levant" , "Adalia"};
                    case Enums.Pname.Acre:
                        return new List<string> {"Levant", "Jerusalem" };
                    case Enums.Pname.Suez:
                        return new List<string> {"Jerusalem" };
                    case Enums.Pname.Alexandria:
                        return new List<string> { "Libya"};

                    // Acre VII
                    case Enums.Pname.Bordeaux:
                        return new List<string> { "Toulouse"};
                    case Enums.Pname.Montpelier:
                        return new List<string> { "Toulouse" };
                    case Enums.Pname.Marseilles:
                        return new List<string> { "Cagliari"};
                    case Enums.Pname.Genoa:
                        return new List<string> { "Cagliari" };
                    case Enums.Pname.Naples:
                        return new List<string> { "Bari"};
                    case Enums.Pname.Sicily:
                        return new List<string> { "Bari" };
                    case Enums.Pname.Milan:
                        return new List<string> { "St. Gali"};
                    case Enums.Pname.Venice:
                        return new List<string> { "St. Gali", "Salzburg" };
                    case Enums.Pname.Dubrovnik:
                        return new List<string> { "Esseg"};
                    case Enums.Pname.Belgrade:
                        return new List<string> { "Esseg", "Durazzo"};

                    // Acre VIII
                    case Enums.Pname.Fez:
                        return new List<string> {"Oran" };
                    case Enums.Pname.Algiers:
                        return new List<string> {"Oran" };
                    case Enums.Pname.Tripoli:
                        return new List<string> { "Barca"};
                    case Enums.Pname.Crete:
                        return new List<string> { "Athens"};
                    case Enums.Pname.Salonika:
                        return new List<string> { "Durazzo", "Corfu", "Athens", "Gallipoli"};
                    case Enums.Pname.Constantinople:
                        return new List<string> {"Gallipoli" };
                    case Enums.Pname.Smyrna:
                        return new List<string> { "Adalia"};

                    default:
                        return null;
                }
            }
        }

        public Loc Loc {
            get
            {
                switch (Name)
                {
                    case Enums.Pname.Hamburg:
                        return new Loc(new List<int> { 497, 340, 422, 337, 463, 401, 484, 400, 502, 404, 526, 384, 527, 372, 512, 332, 500, 316, 443, 308 });
                    case Enums.Pname.Copenhagen:
                        return new Loc(new List<int> { 472, 239, 446, 308, 430, 220, 482, 185, 498, 181, 522, 249, 540, 238, 565, 285, 499, 314 });
                    case Enums.Pname.Lubeck:
                        return new Loc(new List<int> { 539, 322,528, 385, 520, 341, 499, 317, 565, 288, 586, 317, 573, 328, 572, 352, 584, 369 });
                    case Enums.Pname.Stettin:
                        return new Loc(new List<int> { 606, 296, 584, 367, 571, 348, 586, 320, 560, 275, 603, 280, 643, 254, 650, 282, 668, 292 });
                    case Enums.Pname.Danzig:
                        return new Loc(new List<int> { 701, 251, 667, 289, 648, 283, 642, 256, 718, 200, 803, 239, 733, 293, 704, 312 });
                    case Enums.Pname.Mitau:
                        return new Loc(new List<int> { 763, 147, 801, 240, 718, 203, 757, 107, 780, 112, 823, 158 });
                    case Enums.Pname.Riga:
                        return new Loc(new List<int> { 790, 89, 822, 154, 780, 111, 758, 109, 747, 67, 837, 20, 844, 55, 874, 71 });
                    case Enums.Pname.Novgorod:
                        return new Loc(new List<int> { 851, 26, 835, 20, 844, 53, 874, 70, 913, 40, 954, 45, 1041, 102, 1051, 246, 1068, 244, 1067, 104, 1033, 30, 864, 10 });
                    case Enums.Pname.Whisby:
                        return new Loc(new List<int> { 667, 142, 665, 116, 647, 144, 650, 167, 678, 179, 696, 129 });
                    case Enums.Pname.Stockholm:
                        return new Loc(new List<int> { 571, 102, 542, 52, 540, 86, 593, 237, 618, 243, 650, 165, 650, 143, 667, 116, 678, 58, 611, 13 });
                    case Enums.Pname.Malmo:
                        return new Loc(new List<int> { 526, 175, 488, 90, 483, 182, 499, 181, 522, 247, 537, 237, 559, 275, 601, 278, 615, 266, 595, 235, 541, 89, 544, 53, 522, 47 });
                    
                    // Area II
                    case Enums.Pname.London:
                        return new Loc(new List<int> { 276, 437, 302, 493, 255, 441, 268, 414, 328, 375, 354, 431, 337, 481 });
                    case Enums.Pname.Portsmouth:
                        return new Loc(new List<int> { 242, 467, 255, 440, 302, 494, 250, 526, 243, 496, 206, 479 });
                    case Enums.Pname.Cornwall:
                        return new Loc(new List<int> { 188, 511, 248, 520, 244, 499, 210, 478, 180, 490, 164, 535, 196, 552 });
                    case Enums.Pname.York:
                        return new Loc(new List<int> { 235, 538, 263, 426, 212, 339, 230, 306, 336, 370 });
                    case Enums.Pname.Chester:
                        return new Loc(new List<int> { 208, 371, 15, 413, 197, 365, 214, 339, 263, 427, 255, 439, 236, 468, 214, 470, 216, 414, 167, 396, 161, 386, 195, 363 });
                    case Enums.Pname.Wales:
                        return new Loc(new List<int> { 188, 425, 178, 496, 208, 479, 223, 454, 216, 415, 167, 397, 157, 466 });
                    case Enums.Pname.Waterford:
                        return new Loc(new List<int> { 99, 437, 163, 419, 138, 412, 80, 411, 92, 425, 82, 458, 62, 472, 54, 524, 93, 528, 148, 506 });
                    case Enums.Pname.Armagh:
                        return new Loc(new List<int> { 98, 372, 96, 343, 78, 409, 161, 419, 167, 396, 140, 357 });
                    case Enums.Pname.Edinburg:
                        return new Loc(new List<int> { 148, 316, 158, 384, 192, 366, 202, 358, 228, 310, 191, 232, 178, 216, 78, 276, 96, 321 });
                    case Enums.Pname.Iceland:
                        return new Loc(new List<int> { 82, 192, 80, 185, 77, 215, 144, 217, 144, 185 });
                    case Enums.Pname.ShetlandIslands:
                        return new Loc(new List<int> { 196, 157, 192, 234, 179, 214, 167, 175, 184, 137, 227, 140, 243, 153, 270, 189 });
                    case Enums.Pname.Bergen:
                        return new Loc(new List<int> { 357, 80, 229, 138, 328, 10, 384, 10, 460, 60, 369, 148, 335, 168, 249, 183 });
                    case Enums.Pname.Kongsberg:
                        return new Loc(new List<int> { 403, 126, 334, 166, 381, 247, 433, 219, 479, 185, 490, 88, 462, 60 });

                    // Area III
                    case Enums.Pname.Paris:
                        return new Loc(new List<int> { 396, 524, 371, 602, 348, 579, 320, 582, 328, 528, 296, 504, 339, 480, 348, 458, 368, 468, 438, 503, 438, 540 });
                    case Enums.Pname.Loire:
                        return new Loc(new List<int> { 306, 593, 371, 604, 352, 594, 348, 582, 306, 583, 299, 606, 344, 609 });
                    case Enums.Pname.StMalo:
                        return new Loc(new List<int> { 251, 588, 319, 579, 305, 579, 302, 604, 272, 630, 207, 618, 208, 557, 294, 505, 329, 526 });
                    case Enums.Pname.Bruges:
                        return new Loc(new List<int> { 397, 448, 346, 458, 383, 412, 443, 426, 453, 418, 482, 457, 465, 501, 438, 503 });
                    case Enums.Pname.Amsterdam:
                        return new Loc(new List<int> { 423, 381, 384, 412, 368, 397, 400, 334, 422, 340, 466, 404, 452, 421, 442, 428 });
                    case Enums.Pname.Cologne:
                        return new Loc(new List<int> { 483, 442, 453, 418, 466, 403, 505, 409, 542, 391, 511, 483, 480, 467 });
                    case Enums.Pname.Strasbourg:
                        return new Loc(new List<int> { 466, 507, 472, 533, 452, 545, 436, 540, 436, 501, 462, 501, 480, 462, 534, 500, 544, 493, 567, 509 });
                    case Enums.Pname.Salzburg:
                        return new Loc(new List<int> { 623, 494, 587, 509, 606, 527, 636, 533, 664, 523, 672, 440, 658, 430, 612, 468, 610, 461, 582, 472 });
                    case Enums.Pname.Dijon:
                        return new Loc(new List<int> { 413, 578, 394, 584, 438, 542, 453, 546, 473, 535, 478, 586, 427, 617, 402, 617 });
                    case Enums.Pname.Basel:
                        return new Loc(new List<int> { 483, 540, 477, 534, 544, 518, 535, 555, 538, 569, 524, 576, 478, 588 });
                    case Enums.Pname.StGali:
                        return new Loc(new List<int> { 571, 535, 537, 558, 532, 551, 548, 522, 545, 514, 565, 511, 577, 514, 586, 508, 604, 521, 609, 536, 592, 550, 588, 563, 570, 570 });
                    case Enums.Pname.Nuremberg:
                        return new Loc(new List<int> { 528, 449, 543, 494, 535, 501, 508, 485, 541, 391, 555, 387, 551, 427, 584, 469, 588, 480, 589, 491, 588, 507, 570, 513 });
                    case Enums.Pname.Prague:
                        return new Loc(new List<int> { 578, 413, 611, 459, 625, 471, 638, 457, 636, 450, 658, 427, 601, 370, 555, 384, 548, 426, 568, 448, 583, 469 });
                    case Enums.Pname.Breslau:
                        return new Loc(new List<int> { 655, 341, 601, 364, 601, 357, 673, 294, 705, 312, 722, 292, 730, 295, 727, 309, 739, 335, 738, 385, 729, 409, 719, 404, 673, 435, 660, 426 });
                    case Enums.Pname.Vienna:
                        return new Loc(new List<int> { 684, 481, 729, 406, 709, 407, 674, 436, 667, 514, 688, 542, 705, 541, 706, 520, 732, 507, 742, 457 });
                    case Enums.Pname.Budapest:
                        return new Loc(new List<int> { 743, 490, 730, 503, 728, 487, 741, 457, 814, 480, 813, 505, 829, 518, 814, 529, 778, 522 });
                    case Enums.Pname.Esseg:
                        return new Loc(new List<int> { 713, 523, 703, 540, 696, 525, 709, 521, 733, 506, 775, 517, 779, 528, 757, 542, 739, 562, 723, 561 });


                    // Area IV
                    case Enums.Pname.Barcelona:
                        return new Loc(new List<int> { 391, 773, 412, 813, 396, 808, 367, 809, 387, 733, 422, 720, 429, 705, 479, 742, 453, 817, 437, 841 });
                    case Enums.Pname.Basque:
                        return new Loc(new List<int> { 334, 769, 338, 735, 389, 732, 376, 779, 365, 792, 354, 792, 308, 781, 292, 771, 258, 727, 293, 715 });
                    case Enums.Pname.Leon:
                        return new Loc(new List<int> { 218, 785, 256, 804, 250, 820, 231, 828, 214, 823, 209, 815, 134, 805, 190, 726, 258, 725, 289, 772, 307, 779, 276, 800 });
                    case Enums.Pname.Lisbon:
                        return new Loc(new List<int> { 208, 864, 155, 809, 239, 827, 259, 915, 194, 978, 165, 920, 154, 808 });
                    case Enums.Pname.Palma:
                        return new Loc(new List<int> { 432, 867, 450, 824, 485, 845, 445, 893, 427, 887, 421, 866 });
                    case Enums.Pname.Toledo:
                        return new Loc(new List<int> { 283, 854, 338, 831, 342, 851, 350, 855, 330, 907, 259, 919, 239, 826, 251, 820, 255, 806, 306, 782, 363, 793, 367, 809 });
                    case Enums.Pname.Valencia:
                        return new Loc(new List<int> { 387, 838, 398, 808, 415, 817, 434, 843, 422, 865, 426, 884, 428, 906, 408, 948, 391, 933, 391, 919, 344, 854, 340, 829, 366, 808 });
                    
                    // Area V
                    case Enums.Pname.Abasgia:
                        return new Loc(new List<int> { 1248, 469, 1231, 493, 1230, 477, 1234, 436, 1304, 457, 1294, 465, 1301, 475, 1278, 480, 1267, 497, 1264, 515 });
                    case Enums.Pname.Angora:
                        return new Loc(new List<int> { 1139, 619, 1101, 604, 1152, 563, 1232, 646, 1129, 668 });
                    case Enums.Pname.Erzerum:
                        return new Loc(new List<int> { 1297,612, 1267, 631, 1273, 613, 1312, 600, 1322, 571, 1303, 562, 1303, 538, 1334, 524, 1345, 638, 1293, 645 });
                    case Enums.Pname.Kaffa:
                        return new Loc(new List<int> { 1033, 425, 999, 411, 1058, 393, 1109, 409, 1136, 401, 1151, 406, 1169, 423, 1094, 440, 1055, 433, 1047, 466, 1008, 456 });
                    case Enums.Pname.Kamishin:
                        return new Loc(new List<int> { 1246, 349, 1220, 350, 1272, 338, 1275, 345, 1269, 358, 1281, 377, 1252, 390 });
                    case Enums.Pname.Kiev:
                        return new Loc(new List<int> { 1068, 258, 1101, 402, 1132, 382, 1162, 328, 1098, 293, 1080, 313, 1053, 289, 1063, 271, 1048, 263, 1047, 249, 1075, 254, 1094, 245, 1121, 258, 1122, 279, 1182, 324, 1144, 400, 1111, 409 });
                    case Enums.Pname.Poti:
                        return new Loc(new List<int> { 1303, 502, 1268, 497, 1283, 476, 1302, 475, 1298, 456, 1320, 466, 1337, 487, 1338, 522, 1306, 534, 1264, 516 });
                    case Enums.Pname.Sarai:
                        return new Loc(new List<int> { 1277, 352, 1273, 339, 1313, 324, 1343, 373, 1323, 383, 1280, 375, 1266, 358 });
                    case Enums.Pname.Tana:
                        return new Loc(new List<int> { 1210, 421, 1191, 480, 1196, 450, 1174, 442, 1168, 420, 1150, 406, 1178, 365, 1220, 350, 1256, 391, 1237, 442, 1233, 493 });
                    case Enums.Pname.Trezibond:
                        return new Loc(new List<int> { 1220, 611, 1149, 559, 1304, 562, 1321, 570, 1323, 582, 1312, 598, 1273, 612, 1265, 631, 1269, 637, 1232, 650 });
                    case Enums.Pname.Varna:
                        return new Loc(new List<int> { 991, 506, 1010, 458, 1061, 476, 1042, 603, 986, 605, 972, 542 });

                    // Area VI
                    case Enums.Pname.Acre:
                        return new Loc(new List<int> { 1250, 853,1230, 850, 1298, 830, 1299, 878, 1231, 911 });
                    case Enums.Pname.Adalia:
                        return new Loc(new List<int> { 1124, 752, 1088, 750, 1110, 732, 1173, 731, 1157, 787, 1127, 776 });
                    case Enums.Pname.Aleppo:
                        return new Loc(new List<int> { 1253, 748, 1278, 722, 1279, 706, 1315, 710, 1315, 732, 1298, 741, 1298, 796, 1284, 784, 1252, 803, 1231, 807, 1219, 780, 1216, 757, 1234, 736, 1268, 728 });
                    case Enums.Pname.Alexandria:
                        return new Loc(new List<int> { 1145, 962, 1112, 1006, 1121, 943, 1183, 936, 1186, 956, 1171, 1029 });
                    case Enums.Pname.Cairo:
                        return new Loc(new List<int> { 1183, 976, 1184, 936, 1198, 931, 1202, 961, 1210, 975, 1204, 993, 1211, 1000, 1222, 1046, 1170, 1041 });
                    case Enums.Pname.Cyprus:
                        return new Loc(new List<int> { 1176, 828, 1151, 811, 1206, 801, 1220, 781, 1232, 806, 1227, 837, 1183, 870, 1153, 836 });
                    case Enums.Pname.Jerusalem:
                        return new Loc(new List<int> { 1258, 927, 1235, 909, 1287, 883, 1299, 901, 1285, 921, 1296, 947, 1283, 976, 1243, 970, 1229, 938 });
                    case Enums.Pname.Levant:
                        return new Loc(new List<int> { 1265, 819, 1231, 807, 1252, 808, 1287, 786, 1294, 794, 1302, 831, 1232, 852 });
                    case Enums.Pname.Suez:
                        return new Loc(new List<int> { 1209, 948, 1229, 937, 1242, 966, 1240, 996, 1221, 978, 1222, 1000, 1210, 1010, 1203, 993, 1211, 976, 1202, 960, 1200, 93 });
                    case Enums.Pname.Tarsus:
                        return new Loc(new List<int> { 1177, 754, 1172, 736, 1281, 709, 1280, 721, 1217, 753, 1217, 781, 1203, 801, 1170, 809, 1156, 785 });

                    // Area VII
                    case Enums.Pname.Bari:
                        return new Loc(new List<int> { 771, 755, 757, 720, 756, 756, 742, 772, 758, 820, 774, 838, 810, 822, 830, 745, 814, 707, 744, 680, 742, 702 });
                    case Enums.Pname.Belgrade:
                        return new Loc(new List<int> { 796, 564, 802, 591, 756, 552, 784, 526, 833, 522, 869, 534, 858, 618, 839, 619, 831, 663, 819, 661 });
                    case Enums.Pname.Bordeaux:
                        return new Loc(new List<int> { 366, 662, 299, 610, 398, 605, 413, 675, 404, 688, 336, 690, 305, 684, 275, 635 });
                    case Enums.Pname.Cagliari:
                        return new Loc(new List<int> { 593, 752, 580, 668, 548, 702, 582, 842, 648, 814, 609, 688 });
                    case Enums.Pname.Dubrovnik:
                        return new Loc(new List<int> { 756, 605, 734, 567, 754, 551, 804, 604, 808, 626, 818, 660, 802, 682, 688, 605, 688, 552, 711, 548 });
                    case Enums.Pname.Florence:
                        return new Loc(new List<int> { 609, 634, 631, 671, 639, 661, 699, 643, 680, 628, 624, 626, 609, 605, 584, 617, 601, 647 });
                    case Enums.Pname.Genoa:
                        return new Loc(new List<int> { 573, 642, 530, 630, 550, 612, 581, 620, 644, 684, 648, 695, 632, 709, 567, 657, 550, 674, 534, 664 });
                    case Enums.Pname.Lyons:
                        return new Loc(new List<int> { 505, 601, 403, 622, 524, 582, 548, 611, 528, 630, 486, 631, 483, 638, 405, 636 });
                    case Enums.Pname.Marseilles:
                        return new Loc(new List<int> { 513, 660, 533, 668, 530, 632, 487, 633, 490, 709, 509, 726, 547, 700, 564, 682 });
                    case Enums.Pname.Milan:
                        return new Loc(new List<int> { 571, 586, 528, 585, 544, 565, 592, 572, 605, 605, 581, 616, 548, 610 });
                    case Enums.Pname.Montpelier:
                        return new Loc(new List<int> { 454, 674, 407, 637, 485, 641, 490, 706, 458, 723, 425, 700 });
                    case Enums.Pname.Naples:
                        return new Loc(new List<int> { 719, 726, 671, 736, 697, 712, 711, 694, 720, 668, 743, 682, 740, 704, 754, 727, 750, 742, 754, 741, 754, 757, 742, 774, 720, 782 });
                    case Enums.Pname.Rome:
                        return new Loc(new List<int> { 659, 689, 710, 697, 670, 737, 631, 710, 651, 693, 647, 682, 634, 671, 639, 660, 699, 645, 719, 667 });
                    case Enums.Pname.Sicily:
                        return new Loc(new List<int> { 717, 859, 752, 815, 714, 807, 678, 822, 690, 851, 741, 919, 768, 923, 779, 846 });
                    case Enums.Pname.Toulouse:
                        return new Loc(new List<int> { 395, 698, 421, 691, 284, 698, 298, 718, 334, 734, 420, 720, 427, 698 });
                    case Enums.Pname.Venice:
                        return new Loc(new List<int> { 648, 571, 594, 572, 616, 535, 664, 528, 691, 560, 687, 604, 653, 628, 625, 626, 613, 602, 606, 604 });

                    // Area VIII
                    case Enums.Pname.Algiers:
                        return new Loc(new List<int> { 499, 944, 493, 919, 600, 880, 616, 922, 461, 1018 });
                    case Enums.Pname.Athens:
                        return new Loc(new List<int> { 901, 793, 880, 799, 904, 762, 898, 742, 919, 723, 969, 726, 989, 762, 986, 785, 1003, 838, 960, 857, 902, 857 });
                    case Enums.Pname.Barca:
                        return new Loc(new List<int> { 903, 1042, 878, 1021, 930, 957, 965, 1026, 940, 1059, 870, 1059 });
                    case Enums.Pname.Constantinople:
                        return new Loc(new List<int> { 1038, 680, 984, 713, 1016, 666, 1041, 651, 1049, 614, 1100, 603, 1117, 621, 1112, 636, 1129, 666, 1094, 672, 1074, 712, 1075, 737, 1005, 747 });
                    case Enums.Pname.Corfu:
                        return new Loc(new List<int> { 858, 734, 831, 743, 843, 740, 846, 720, 853, 706, 878, 707, 906, 759, 881, 799 });
                    case Enums.Pname.Crete:
                        return new Loc(new List<int> { 977, 852, 958, 859, 1020, 829, 1041, 856, 998, 887 });
                    case Enums.Pname.Durazzo:
                        return new Loc(new List<int> { 845, 660, 865, 625, 898, 655, 874, 666, 883, 671, 888, 683, 879, 707, 853, 706, 833, 746, 823, 718, 845, 626 });
                    case Enums.Pname.Fez:
                        return new Loc(new List<int> { 349, 1018, 278, 1029, 358, 1001, 395, 995, 394, 1033, 388, 1046, 357, 1060, 283, 1060 });
                    case Enums.Pname.Gallipoli:
                        return new Loc(new List<int> { 985, 651, 950, 658, 967, 651, 982, 606, 1047, 606, 1041, 651, 1018, 664, 984, 711, 960, 705 });
                    case Enums.Pname.Granada:
                        return new Loc(new List<int> { 342, 951, 334, 911, 352, 862, 387, 936, 402, 949, 360, 996, 329, 1007, 311, 961 });
                    case Enums.Pname.Libya:
                        return new Loc(new List<int> { 1007, 980, 932, 976, 934, 951, 1116, 946, 1109, 1008, 966, 1028 });
                    case Enums.Pname.Oran:
                        return new Loc(new List<int> { 418, 992, 397, 992, 496, 917, 462, 1014, 390, 1051 });
                    case Enums.Pname.Salonika:
                        return new Loc(new List<int> { 898, 680, 895, 656, 955, 649, 954, 661, 961, 701, 972, 723, 920, 721, 900, 743, 874, 709, 891, 681, 875, 669, 881, 654 });
                    case Enums.Pname.Seville:
                        return new Loc(new List<int> { 291, 933, 333, 910, 312, 962, 329, 1007, 274, 1028, 210, 1001, 197, 970, 259, 918, 330, 912 });
                    case Enums.Pname.Smyrna:
                        return new Loc(new List<int> { 1043, 762, 1004, 747, 1066, 738, 1123, 768, 1124, 813, 1055, 834 });
                    case Enums.Pname.Tripoli:
                        return new Loc(new List<int> { 737, 1002, 705, 971, 878, 1020, 873, 1059, 688, 1049, 663, 1017 });
                    case Enums.Pname.Tunis:
                        return new Loc(new List<int> { 658, 901, 600, 881, 691, 856, 709, 899, 706, 972, 665, 1016, 630, 1007 });
                    case Enums.Pname.WestAfrica:
                        return new Loc(new List<int> { 155, 1028, 156, 1011, 224, 1011, 224, 1045, 155, 1046 });


                    case Enums.Pname.NorthAmerica:
                        return new Loc(new List<int> { 109, 831, 103, 927, 104, 812, 134, 812, 133, 929 });
                    case Enums.Pname.SouthAmerica:
                        return new Loc(new List<int> { 109, 951, 102, 1054, 103, 936, 130, 934, 132, 1054 });
                    case Enums.Pname.EastIndies:
                        return new Loc(new List<int> { 1327, 738, 1315, 702, 1358, 703, 1358, 813, 1315, 812 });
                    case Enums.Pname.China:
                        return new Loc(new List<int> { 1327, 862, 1315, 826, 1358, 826, 1359, 939, 1315, 938 });
                    case Enums.Pname.India:
                        return new Loc(new List<int> { 1327, 987, 1315, 952, 1359, 954, 1360, 1064, 1316, 1061 });
                    
                    default:
                        return null;

                }
            }
        }

        public string Area
        {
            get
            {
                switch (Name)
                {
                    case Enums.Pname.Novgorod:
                    case Enums.Pname.Riga:
                    case Enums.Pname.Mitau:
                    case Enums.Pname.Danzig:
                    case Enums.Pname.Stettin:
                    case Enums.Pname.Lubeck:
                    case Enums.Pname.Hamburg:
                    case Enums.Pname.Copenhagen:
                    case Enums.Pname.Stockholm:
                    case Enums.Pname.Malmo:
                    case Enums.Pname.Whisby:
                        return "I";

                    case Enums.Pname.Kongsberg:
                    case Enums.Pname.Bergen:
                    case Enums.Pname.ShetlandIslands:
                    case Enums.Pname.Edinburg:
                    case Enums.Pname.York:
                    case Enums.Pname.London:
                    case Enums.Pname.Chester:
                    case Enums.Pname.Portsmouth:
                    case Enums.Pname.Cornwall:
                    case Enums.Pname.Wales:
                    case Enums.Pname.Armagh:
                    case Enums.Pname.Waterford:
                    case Enums.Pname.Iceland:
                        return "II";

                    case Enums.Pname.StMalo:
                    case Enums.Pname.Loire:
                    case Enums.Pname.Paris:
                    case Enums.Pname.Dijon:
                    case Enums.Pname.Basel:
                    case Enums.Pname.Bruges:
                    case Enums.Pname.Cologne:
                    case Enums.Pname.Nuremberg:
                    case Enums.Pname.Prague:
                    case Enums.Pname.Breslau:
                    case Enums.Pname.Vienna:
                    case Enums.Pname.Budapest:
                    case Enums.Pname.Esseg:
                    case Enums.Pname.Salzburg:
                    case Enums.Pname.StGali:
                    case Enums.Pname.Strasbourg:
                        return "III";
                    
                    case Enums.Pname.Barcelona:
                    case Enums.Pname.Basque:
                    case Enums.Pname.Leon:
                    case Enums.Pname.Valencia:
                    case Enums.Pname.Toledo:
                    case Enums.Pname.Lisbon:
                    case Enums.Pname.Palma:
                        return "IV";

                    case Enums.Pname.Varna:
                    case Enums.Pname.Kaffa:
                    case Enums.Pname.Kiev:
                    case Enums.Pname.Tana:
                    case Enums.Pname.Sarai:
                    case Enums.Pname.Abasgia:
                    case Enums.Pname.Poti:
                    case Enums.Pname.Erzerum:
                    case Enums.Pname.Trezibond:
                    case Enums.Pname.Angora:
                        return "V";

                    case Enums.Pname.Acre:
                    case Enums.Pname.Adalia:
                    case Enums.Pname.Aleppo:
                    case Enums.Pname.Alexandria:
                    case Enums.Pname.Cairo:
                    case Enums.Pname.Cyprus:
                    case Enums.Pname.Jerusalem:
                    case Enums.Pname.Levant:
                    case Enums.Pname.Libya:
                    case Enums.Pname.Suez:
                    case Enums.Pname.Tarsus:
                        return "VI";

                    case Enums.Pname.Bari:
                    case Enums.Pname.Belgrade:
                    case Enums.Pname.Bordeaux:
                    case Enums.Pname.Cagliari:
                    case Enums.Pname.Dubrovnik:
                    case Enums.Pname.Florence:
                    case Enums.Pname.Genoa:
                    case Enums.Pname.Lyons:
                    case Enums.Pname.Marseilles:
                    case Enums.Pname.Milan:
                    case Enums.Pname.Montpelier:
                    case Enums.Pname.Naples:
                    case Enums.Pname.Rome:
                    case Enums.Pname.Sicily:
                    case Enums.Pname.Toulouse:
                    case Enums.Pname.Venice:
                        return "VII";

                    case Enums.Pname.China:
                    case Enums.Pname.EastIndies:
                    case Enums.Pname.India:
                        return "Far East";
                    case Enums.Pname.NorthAmerica:
                    case Enums.Pname.SouthAmerica:
                        return "New World";
                    default:
                        return "Error";
                }

            }
        }

        public Enums.Capital GetDefenderInHomeArea(List<Player> players, Enums.Capital attackerCapital)
        {
            foreach (var presence in Presences)
            {
                if (presence.Capital != attackerCapital)
                {
                    var player = players.Find(m => m.HomeArea == Area);
                    if (player != null)
                    {
                        return presence.Capital;
                    }
                }
            }
            return Enums.Capital.NotSelected;
        }

        public int GetDefenseTokenNumberFromSatelites(Player attacker, Map map)
        {
            if (Support == null)
            {
                return 0;
            }

            var sateliteModifier = 0;
            foreach (var provinceName in Support)
            {
                var satelite = map.Provinces.Find(m => m.NameAsText == provinceName);
                if (satelite != null && satelite.Presences.Count == 1 &&
                    Presences.Exists(m => m.Capital == satelite.Presences[0].Capital))
                {
                    sateliteModifier += 1;
                }
            }
            return sateliteModifier;
        }

        public int GetAttackTokenNumberFromSatelites(Player attacker, Map map)
        {
            if (Support == null || !attacker.HasAdvance(Enums.AdvanceType.Cosmopolitan))
            {
                return 0;
            }

            var sateliteModifier = 0;
            foreach (var provinceName in Support)
            {
                var satelite = map.Provinces.Find(m => m.NameAsText == provinceName);
                if (satelite.Presences.Count == 1 && satelite.Presences[0].Capital == attacker.Capital)
                {
                    sateliteModifier += 1;
                }
            }

            return sateliteModifier;
        }
    }
}
