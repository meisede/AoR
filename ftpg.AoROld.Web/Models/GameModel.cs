using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime;
using System.Web.Mvc;
using ftpg.AoR.Entity;

namespace ftpg.AoR.Web.Models
{
    public class GameModel
    {
        public string ViewAction { get; set; }
        public Game Game { get; set; }
        public MenuModel Menu { get; set; }
        public string Name { get; set; }
        public int Epoch { get; set; }
        public DateTime Start { get; set; }
        public DateTime LatestPlay { get; set; }
        public Enums.GamePhase Phase { get; set; }
        public int Turn { get; set; }
        public int PlayOrder { get; set; }
        public List<PlayerModel> Players { get; set; }
        public List<Card> DrawDeck { get; set; }
        public List<Card> DiscardDeck { get; set; }
        public List<string> Unplayable { get; set; }
        public List<string> CardsLastingEffect { get; set; }
        public bool GameEnd { get; set; }
        public PlayerModel PlayerDisplay { get; set; }
        public int NumberOfPlayers => Players.Count;
        public int NoCards { get; set; }
        public int MiseryReliefAvailable { get; set; }
        public int MiseryReliefRebate { get; set; }
        public int MiseryReliefPurchase { get; set; }
        public Dictionary<string,bool> AdvancesPurchased { get; set; }
        public List<ShortageSurplus> ShortageSurplus { get; set; }
        public int CashSaved => PlayerDisplay.Cash - MiseryReliefPurchase;
        public Enums.GameType GameType { get; set; }
        public MapModel MapModel { get; set; }
        public DiceResult DiceResult { get; set; }
        public string BuyCardExpansion { get; set; }
        public PurchaseModel Purchase { get; set; }
        public WarResult WarResult{ get; set; }

        public void InitAdvancesPurchased()
        {
            AdvancesPurchased = new Dictionary<string, bool>();
            foreach (var advances in Game.Advances)
            {
                AdvancesPurchased.Add(advances.Letter, false);
            }
        }
       
        public List<SelectItem> AvailableCapitalsNames()
        {
            var list = new List<SelectItem>();

            var index = 0;
            foreach (var capital in Game.CapitalsNames())
            {
                if (!Players.Exists(m => m.Capital == capital))
                {
                    list.Add(new SelectItem {Id = index, Name = capital.ToString()});
                    index++;
                }
                
            }
            return list;
        }

        public bool IsPlayerInOrder => PlayerDisplay.PlayOrder == PlayOrder;

        public bool IsDone(Player player)
        {
            return player.DidAction;
        }

        public List<Player> GetTargetsOfAlchemistGold()
        {
            var playersNotProtectedByEnlightenedRuler = Game.GetPlayersNotProtectedByEnlightenedRuler();
            var players = AdvanceHandler.AllPlayersNotHaveAdvance(playersNotProtectedByEnlightenedRuler.FindAll(m => m.Capital != PlayerDisplay.Capital), Enums.AdvanceType.LawsOfMatter);
            return players;
        }

        public string AdvanceBackgroundColor(AdvanceModel advance)
        {
            return advance.Group == Enums.AdvanceGroup.Religion.ToString() || advance.Group == Enums.AdvanceGroup.Communic.ToString() || advance.Group == Enums.AdvanceGroup.Civics.ToString()
                ? "gainsboro"
                : "white";
        }

        public string GetShipTypeByEnum(Enums.ShipType shipType)
        {
            switch (shipType)
            {
                case Enums.ShipType.Galley2:
                    return "Galley 2";
                case Enums.ShipType.Galley4:
                    return "Galley 4";
                case Enums.ShipType.Galley6:
                    return "Galley 6";
                case Enums.ShipType.Galley8:
                    return "Galley 8";
                case Enums.ShipType.SeaWorthy10:
                    return "Sea worthy 10";
                case Enums.ShipType.SeaWorthy12:
                    return "Sea worthy 12";
                case Enums.ShipType.SeaWorthy14:
                    return "Sea worthy 14";
                case Enums.ShipType.SeaWorthy16:
                    return "Sea worthy 16";
                case Enums.ShipType.OceanGoing1:
                    return "Ocean going 1";
                case Enums.ShipType.OceanGoing2:
                    return "Ocean going 2";
                case Enums.ShipType.OceanGoing3:
                    return "Ocean going 3";
                case Enums.ShipType.OceanGoing4:
                    return "Ocean going 4";
                default:
                    return "None";

            }
        }
        public string GetShipShortNameByEnum(Enums.ShipType shipType)
        {
            switch (shipType)
            {
                case Enums.ShipType.Galley2:
                    return "Ga2";
                case Enums.ShipType.Galley4:
                    return "Ga4";
                case Enums.ShipType.Galley6:
                    return "Ga6";
                case Enums.ShipType.Galley8:
                    return "Ga8";
                case Enums.ShipType.SeaWorthy10:
                    return "Sea10";
                case Enums.ShipType.SeaWorthy12:
                    return "Sea12";
                case Enums.ShipType.SeaWorthy14:
                    return "Sea14";
                case Enums.ShipType.SeaWorthy16:
                    return "Sea16";
                case Enums.ShipType.OceanGoing1:
                    return "Oce1";
                case Enums.ShipType.OceanGoing2:
                    return "Oce2";
                case Enums.ShipType.OceanGoing3:
                    return "Oce3";
                case Enums.ShipType.OceanGoing4:
                    return "Oce4";
                default:
                    return "None";
            }
        }

        public string GetShortageSurplusItems(bool shortage)
        {
            var text = string.Empty;
            foreach (var item in ShortageSurplus)
            {
                if (item.IsShortage == shortage)
                {
                    text = (text == string.Empty) ? item.Good.ToString() : text + ", " + item.Good;
                }
            }
            return text;
        }


        private List<SelectItem> _positions;

        [Display(Name = "Positions")]
        public int SelectedPosition { get; set; }

        public IEnumerable<SelectListItem> Positions
        {
            get
            {
                PopulatePositions();
                return new SelectList(_positions, "Id", "Name"); }
        }

        public void PopulatePositions()
        {
            _positions = new List<SelectItem>();
            for (var index = 1; index < Players.Count + 1; index++)
            {
                var position = Game.GetModifiedPlayOrder(index);
                if (!Players.Exists(m => m.PlayPosition == index))
                {
                    _positions.Add(new SelectItem { Id = index, Name = GetPositionText(position) });
                }
            }
        }

        private List<SelectItem> _domList;
        public void PopulateDomList(Map map, Player player)
        {
            _domList = new List<SelectItem>();
            foreach (var province in player.GetDoms(map))
            {
                _domList.Add(new SelectItem { Id = 0, Name = province.NameAsText });
            }
        }

        private static string GetPositionText(int position)
        {
            if (position == 1)
            {
                return "1st, 0 tokens, 0 MI";
            }
            if (position == 2)
            {
                return "2nd, 5 tokens, 1 MI";
            }
            if (position == 3)
            {
                return "3rd, 10 tokens, 2 MI";
            }
            if (position == 4)
            {
                return "4th, 15 tokens, 3 MI";
            }
            if (position == 5)
            {
                return "5th, 20 tokens, 4 MI";
            }
            return "6th, 25 tokens, 5 MI";
        }

        
    }

    public class SelectItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}