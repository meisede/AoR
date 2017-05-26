using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace ftpg.AoR.Entity
{
    public class Game
    {
        [XmlElement(Order = 1)]
        public string Name { get; set; }
        [XmlElement(Order = 2)]
        public int Epoch { get; set; }
        [XmlElement(Order = 3)]
        public DateTime Start { get; set; }
        [XmlElement(Order = 4)]
        public DateTime LatestPlay { get; set; }
        [XmlElement(Order = 5)]
        public Enums.GamePhase Phase { get; set; }
        [XmlElement(Order = 6)]
        public int Turn { get; set; }
        [XmlElement(Order = 7)]
        public int PlayOrder { get; set; }
        [XmlElement(Order = 8)]
        public List<Pair> CardsInEffect { get; set; }
        [XmlElement(Order = 9)]
        public List<string> CardsLastingEffect { get; set; }
        [XmlElement(Order = 10)]
        public string Interrupt { get; set; }
        [XmlElement(Order = 11)]
        public int InterruptPreservedOrder { get; set; } // The nation in turn before the interrupt


        [XmlElement(Order = 14)]
        public List<string> CrusadeProvinces { get; set; }
        [XmlElement(Order = 15)]
        public Enums.GameType GameType { get; set; }
        [XmlElement(Order = 16)]
        public Enums.OrderOfPlayChoice OrderOfPlayDeterminative { get; set; }

        [XmlElement(Order = 17)]
        public bool GameOver { get; set; }
        [XmlElement(Order = 18)]
        public bool EndPlay { get; set; }
        [XmlElement(Order = 19)]
        public List<ShortageSurplus> ShortageSurplus { get; set; }

        [XmlElement(Order = 20)]
        public List<CardPlay> CardPlays { get; set; }
        [XmlElement(Order = 21)]
        public List<Card> DrawPile { get; set; }
        [XmlElement(Order = 22)]
        public List<Card> RedrawPile { get; set; }
        [XmlElement(Order = 23)]
        public List<string> Unplayable { get; set; }
        [XmlElement(Order = 24)]
        public List<Player> Players { get; set; }
        [XmlElement(Order = 25)]
        public int ExpansionCardPurchase { get; set; }
        [XmlElement(Order = 26)]
        public WarResult WarResult { get; set; }

        [XmlElement(Order = 80)]
        public Map Map;
        [XmlElement(Order = 90)]
        public string History { get; set; }

        [XmlIgnore]
        public DiceResult DiceResult { get; set; }
        [XmlIgnore]
        public Player PlayerInTurn { get { return Players.Find(m => m.PlayOrder == PlayOrder); } }
        [XmlIgnore]
        public int NumberOfPlayers => Players.Count;
        [XmlIgnore]
        public List<Advance> Advances => AdvanceHandler.GetAdvances();

        [XmlIgnore]
        public string BuyCardExpansion { get; set; }

        public string PapalDecree
        {
            get
            {
                return CardsInEffect.Exists(m => m.Key == "Papal Decree") ? CardsInEffect.Find(m => m.Key == "Papal Decree").Value : string.Empty;
            }
        }

        public Game()
        {
        }

        public Game(string name, int numberOfPlayers)
        {
            GameOver = false;
            Epoch = 1;
            Turn = 0;
            PlayOrder = 1;
            Name = name;
            Start = DateTime.Now;
            DrawPile = CardHandler.CreateEpoch1CardsStartingCards();
            RedrawPile = new List<Card>();
            Unplayable = new List<string>();
            CardsInEffect = new List<Pair>(); 
            CardsLastingEffect = new List<string>();
            
            Players = new List<Player>();
            Phase = Enums.GamePhase.InitialCardDraw;
            for (var index = 0; index < numberOfPlayers; index++)
            {
                var player = new Player("player" + (index + 1));
                player.Cards.AddRange(DrawCards(3));
                Players.Add(player);
            }
            CardPlays = new List<CardPlay>();
            Map = new Map(numberOfPlayers);
            ShortageSurplus = new List<ShortageSurplus>();
            History = string.Empty;
            AddToHistory(Name + " created", false);
            
        }

        #region GameProgress
        public void ProceedToNextPhase()
        {
            switch (Phase)
            {
                case Enums.GamePhase.InitialCardDraw:
                    Players.ForEach(m => m.DidAction = false);
                    Phase = Enums.GamePhase.CapitalBid;
                    break;
                case Enums.GamePhase.CapitalBid:
                    PlayOrder = 1;
                    Players.ForEach(m => m.PlayPosition = 0);
                    Phase = Enums.GamePhase.SelectCapital;
                    Turn = 1;
                    break;

                case Enums.GamePhase.SelectCapital:
                    Players.ForEach(m => m.DidAction = false);
                    Phase = Enums.GamePhase.DeterminePlayOrder;
                    break;

                case Enums.GamePhase.DeterminePlayOrder:
                    Turn++;
                    if ((Players.Count < 5 && Turn == 1) || Players.Count > 4 && Turn == 2)
                    {
                        CardHandler.AddEuroRuleWithHeldCards(DrawPile);
                        AddToHistory("Added withheld cards", false);
                    }
                    PlayOrder = 1;
                    AddToHistory("Turn: " + Turn, true);
                    Players.ForEach(m => m.DidAction = false);
                    RemoveShortageSurplusPhasePrepare();
                    break;
                case Enums.GamePhase.RemoveShortageSurplus:
                    AddShortageSurplusPhasePrepare();
                    break;
                case Enums.GamePhase.AddShortageSurplus:
                    AddToHistory("Draw Card phase", true);
                    DrawCards();
                    BuyCardPhasePrepare();
                    break;
                case Enums.GamePhase.BuyCard:
                    DiscardPhasePrepare();
                    break;
                case Enums.GamePhase.Discard:
                    PlayOrder = 1;
                    Phase = Enums.GamePhase.PlayCard;
                    AddToHistory("Play card phase", true);
                    break;
                case Enums.GamePhase.PlayCard:
                    Phase = Enums.GamePhase.Purchase;
                    AddToHistory("Purchase phase", true);
                    PlayOrder = 1;
                    
                    break;
                case Enums.GamePhase.Purchase:
                    AddToHistory("Expansion phase", true);
                    PlayOrder = 1;
                    Players.ForEach(m => m.DidAction = false);
                    Players.ForEach(m => m.BoughtCardExpansion = false);
                    ResolveTaxIncome();
                    ResolveHolyIndulgence();
                    ExpansionCardPurchase = 1;
                    Phase = Enums.GamePhase.Expansion;
                    break;
                case Enums.GamePhase.Expansion:
                    Phase = Enums.GamePhase.Domination;
                    ProceedDomination();
                    break;
                case Enums.GamePhase.Domination:
                    Phase = Enums.GamePhase.DeterminePlayOrder;
                    // To:do Implement end game conditions
                    PlayOrder = 0;
                    AddToHistory("Play order phase", true);
                    break;
            }
        }

        public void AddToHistory(string text, bool startWithLineBreak)
        {
            var extraLineBreak = (startWithLineBreak) ? Environment.NewLine : string.Empty;
            History = DateTime.Now.ToString("dd/MM HH:mm") + " " + text + extraLineBreak + Environment.NewLine + History + extraLineBreak;
        }
        #endregion GameProgress

        #region End Game
        #endregion End Game

        #region DetermineOrderOfPlayMethod

        public void TokenPurchase(Player player, int tokens)
        {
            if (player.Cash >= tokens)
            {
                player.Tokens = tokens;

                player.DidAction = true;
            }
            ResolveTokenPurchase();
        }

        private void ResolveTokenPurchase()
        {
            if (Players.TrueForAll(m => m.DidAction))
            {
                var playOrder = 1;
                foreach (var player in Players.OrderBy(m=>m.Tokens).ThenBy(m=>m.CapitalOrder))
                {
                    player.PlayOrder = playOrder;
                    playOrder ++;
                    player.Cash -= Math.Abs(player.Tokens);
                    player.WrittenCash = player.Cash;
                    if (player.Tokens < 0)
                    {
                        AddToHistory(player.Capital + " buys 0 tokens for $" + player.Tokens + ". Written cash $" + player.WrittenCash + " PO:" + playOrder, false);
                        player.Tokens = 0;
                    }
                    else
                    {
                        AddToHistory(player.Capital + " buys " + player.Tokens + ". Written cash $" + player.WrittenCash, false);

                    }
                }
                ProceedToNextPhase();
            }
        }

        public void SetPlayerOrderOfPlay(Enums.Capital capital, int bid)
        {
            Player player = Players.Find(m => m.Capital == capital);
            player.Bid = bid;
            player.DidAction = true;
            ProceedOrderOfPlay();
        }

        private void ProceedOrderOfPlay()
        {
            if (Players.TrueForAll(m => m.DidAction))
            {
                DetermineOrderOfPlay();
                SetTurnOrderNextTurn();
                ProceedToNextPhase();
            }
        }

        public void DeterminePlayOrder(Player player, int tokens)
        {
            player.Tokens = tokens;
            player.DidAction = true;
            ProceedOrderOfPlay();
        }

        private void DetermineOrderOfPlay()
        {
            var fistBid = Players.Max(m => m.Bid);
            switch (fistBid)
            {
                case 3:
                    OrderOfPlayDeterminative = Enums.OrderOfPlayChoice.Misery;
                    break;
                case 2:
                    OrderOfPlayDeterminative = Enums.OrderOfPlayChoice.Cards;
                    break;
                case 1:
                    OrderOfPlayDeterminative = Enums.OrderOfPlayChoice.Provinces;
                    break;
                default:
                    OrderOfPlayDeterminative = Enums.OrderOfPlayChoice.Default;
                    
                    break;
            }



            if (OrderOfPlayDeterminative == Enums.OrderOfPlayChoice.Default)
            {
                AddToHistory("No bids. Same turn order is kept in turn" + (Turn + 1), false);
            }
            else
            {
                AddToHistory(OrderOfPlayDeterminative + " sets play order in turn " + (Turn + 1), false);
            }
         }

        private void SetTurnOrderNextTurn()
        {
            var playOrder = 1;
            foreach (var player in Players.OrderByDescending(m => m.Tokens).ThenBy(x => x.CapitalOrder))
            {
                if (player.Bid == 0)
                {
                    AddToHistory(player.Capital + " buys tokens: " + player.Tokens + " #" + playOrder, false);
                }
                player.PlayOrder = playOrder;
                player.DidAction = false;
                playOrder++;
            }
            foreach (var player in Players.Where(player => player.HasAdvance(Enums.AdvanceType.Renaissance)))
            {
                player.MayUseRenaissance = true;
                AddToHistory(player.Capital + " may use Renaissance", false);
            }
            foreach (var player in Players.Where(player => player.HasAdvance(Enums.AdvanceType.Cathedral)))
            {
                player.MayUseCathedral = true;
                AddToHistory(player.Capital + " may use Cathedral", false);
            }
        }
        #endregion DetermineOrderOfPlayMethod

        #region Domination
        private void ProceedDomination()
        {
            ResolveExpansionBonus();
            MakeAllProvincesExpandable();
            ResolveIncome();
            ResolveReduceMisery();
            ResolveRollShortageSurplus();
            CardsInEffect = new List<Pair>();
            CardPlays = new List<CardPlay>();
            ProceedToNextPhase();
        }
        
        private void ResolveRollShortageSurplus()
        {
            RollShortageSurplus();
            if (ShortageSurplus.Count == 0)
            {
                AddToHistory("Shortage and Surplus cancel each other", false);
                return;
            }
            foreach (var shortageSurplus in ShortageSurplus)
            {
                var shortageSuplusText = (shortageSurplus.Good != Enums.GoodType.Gold)
                    ? shortageSurplus.Good.ToString()
                    : Enums.GoodType.Gold + "/" + Enums.GoodType.Ivory;
                AddToHistory((shortageSurplus.IsShortage ? "Shortage" : "Surplus") + " in " + shortageSuplusText, false);
                var playerWithSoleDomination = GetMajorityPlayerInCommodity(shortageSurplus.Good);
                if (shortageSurplus.IsShortage)
                {
                    if (playerWithSoleDomination != null)
                    {
                        DrawCard(playerWithSoleDomination, 0, "for DOM in " + shortageSurplus.Good);
                    }
                }
                else
                {
                    if (playerWithSoleDomination != null)
                    {
                        playerWithSoleDomination.Tokens -= 1;
                        AddToHistory(playerWithSoleDomination.Capital + " pays 1T for DOM in " + shortageSurplus.Good, false);
                    }
                }
            }
        }
        
        private void ResolveReduceMisery()
        {
            foreach (var player in Players.Where(player => AdvanceHandler.HasAdvance(player, Enums.AdvanceType.NewWorld)))
            {
                player.Misery -= 2;
                AddToHistory(player.Capital + " reduce misery:2 for New World", false);
            }
        }

        private void ResolveIncome()
        {
            foreach (var player in Players)
            {
                var income = player.Income(Map);
                player.Tokens += income;
                AddToHistory(player.Capital + " receives income $" + income, false);
            }
        }

        private void ResolveExpansionBonus()
        {
            var playerWithDomination = new Player();
            var highestDominationCount = -1;

            foreach (var player in Players.OrderBy(m=>m.PlayOrder))
            {
                var count = 0;
                foreach (var province in Map.Provinces)
                {
                    foreach (var presence in province.Presences.FindAll(m=>m.Capital == player.Capital))
                    {
                        if (presence.Strength > presence.Original && presence.Strength > 1 &&
                            presence.Strength == province.MarketValue)
                        {
                            count ++;
                        }
                    }
                }
                if (count > highestDominationCount)
                {
                    highestDominationCount = count;
                    playerWithDomination = player;
                }
                else if (count == highestDominationCount)
                {
                    if (player.PlayOrder < playerWithDomination.PlayOrder)
                    {
                        playerWithDomination = player;
                    }
                }
            }
                
            DrawCard(playerWithDomination, 0, "for Domination: " + highestDominationCount );
        }

        private Player GetMajorityPlayerInCommodity(Enums.GoodType good)
        {
            Player soleMajorityHolder = null;
            var mostDomination = 0;
            foreach (var player in Players)
            {
                var playerDomination = 0;
                foreach (var province in Map.Provinces.FindAll(m=>m.Good == good))
                {
                    foreach (var presence in province.Presences)
                    {
                        if (presence.Strength == province.MarketValue && presence.Capital == player.Capital)
                        {
                            playerDomination ++;
                        }
                    }
                }
                if (playerDomination > mostDomination)
                {
                    soleMajorityHolder = player;
                    mostDomination = playerDomination;
                }
                else if (playerDomination == mostDomination)
                {
                    soleMajorityHolder = null;
                }
            }
            return soleMajorityHolder;
        }

        #endregion Domination

        #region Purchase
        public void Purchase(Player player, Purchase purchase)
        {
            var previousTierLevel = player.TierLevel;
            PayShipUgrade(player, purchase.ShipUpgrade);
            PayLeaderUse(player, purchase);
            PayAdvances(player, purchase);
            PayStabilization(player, purchase.Stabilization);
            PayMiseryRelief(player, purchase, previousTierLevel);

            player.DidAction = true;
            ProceedPurchasePhase();
            //return true;
        }

        private void PayMiseryRelief(Player player, Purchase purchase, int previousTierLevel)
        {
            if (purchase.TierLevel == 0)
            {
                return;
            }
            if (player.TierLevel != purchase.TierLevel)
            {
                AddToHistory("Error Tierlevel tier=" + purchase.TierLevel + " player.TierLevel=" + player.TierLevel, false);
                return;
            }
            var mr = player.PurchaseMiseryRelief(purchase.AdvancesPurchased);
            var neededMr = player.Misery - MiseryHandler.MiseryChangeByStep(player.Misery, previousTierLevel - purchase.TierLevel);
            if (player.Cash < neededMr - mr)
            {
                AddToHistory("Error Player has not enough $" + player.Cash + " to pay $"+ neededMr + " for tier level:" + purchase.TierLevel, false);
                return;
            }
            var cashCover = (neededMr > mr) ? neededMr - mr : 0;
            if (cashCover > 0)
            {
                player.Cash -= cashCover;
            }
            player.Misery -= neededMr;
            AddToHistory(player.Capital + " Misery relief MR:" + mr + " $" + cashCover, false);
        }

        private void PayStabilization(Player player, bool stabilization)
        {
            
            if (stabilization && player.Cash >= player.Stabilization)
            {
                player.Cash -= player.Stabilization;
                AddToHistory(player.Capital + " pays stabilization $" + player.Stabilization, false);
                return;
            }
            var miseryIncrease = MiseryHandler.GetStabilizationMiseryPenalty(player.Misery, player.Stabilization);
            player.Misery += miseryIncrease;
            AddToHistory(player.Capital + " takes stabilization penalty MI:" + miseryIncrease, false);
        }

        private void PayAdvances(Player player, Purchase purchase)
        {
            var hasInstitutionalResearch = AdvanceHandler.HasAdvance(player, Enums.AdvanceType.InstitutionalResearch);
            var hasWrittenRecord = AdvanceHandler.HasAdvance(player, Enums.AdvanceType.WrittenRecord);
            // Note. Advances must be process in the order they are selected because Institutional research will give discount to later purchases
            foreach (var letter in purchase.AdvancesPurchased)
            {
                var advance = AdvanceHandler.GetAdvanceFromLetter(letter.ToString());
                if (AdvanceHandler.HasAdvance(player, advance.AdvanceType))
                {
                    AddToHistory("ERROR " + player.Capital + ": has " + advance.Name, false);
                }
                else
                {
                    var cost = AdvanceHandler.GetPriceForAdvance(player, advance.Letter, CardsLastingEffect, CardPlays, hasInstitutionalResearch, hasWrittenRecord);
                    if (cost > player.Cash)
                    {
                        AddToHistory("ERROR " + player.Capital + ": has not the cash $" + player.Cash + " for " + advance.Name + " $" + cost, false);
                    }
                    else
                    {
                        player.Cash -= cost;
                        player.AdvanceString += advance.Letter;
                        var historyText = player.Capital + " buys " + advance.Name + " for $" + cost;
                        hasInstitutionalResearch = (advance.AdvanceType == Enums.AdvanceType.InstitutionalResearch) || hasInstitutionalResearch;
                        hasWrittenRecord = (advance.AdvanceType == Enums.AdvanceType.WrittenRecord) || hasWrittenRecord;
                        if (advance.MiseryChange != 0 && !(player.Misery == 0 && advance.MiseryChange < 0))
                        {
                            var miseryChange = MiseryHandler.MiseryChangeByStep(player.Misery, advance.MiseryChange) - player.Misery;
                            player.Misery += miseryChange;
                            historyText += " MI:" + miseryChange;
                        }
                        AddToHistory(historyText, false);
                    }
                }
            }
        }

        private void PayLeaderUse(Player player, Purchase purchase)
        {
            var hasPatronage = player.HasAdvance(Enums.AdvanceType.Patronage) || purchase.AdvancesPurchased.Contains("E");
            if (hasPatronage)
            {
                foreach (var leader in purchase.LeadersUsed.Select(leaderName => CardPlays.Find(m => m.Card.Name == leaderName)).Where(leader => leader.Capital != player.Capital))
                {
                    if (!leader.FirstLeaderPlay)
                    {
                        if (leader.Fee > 0)
                        {
                            leader.FeePaid = true; //Note, this property is later use to give written record discount
                            player.Cash -= leader.Fee;
                            Players.Find(m => m.Capital == leader.Capital).Cash += leader.Fee;
                            AddToHistory(player.Capital + " uses " + leader.Card.Name + ". Fee $" + leader.Fee + " to " + leader.Capital, false);
                        }
                        else
                        {
                            AddToHistory(player.Capital + " uses " + leader.Card.Name + ". No fee", false);
                        }
                    }
                    else
                    {
                        // Error occured
                        AddToHistory("ERROR Protected leader is used: " + leader.Card.Name, false);
                    }
                }
            }
        }

        private void PayShipUgrade(Player player, bool shipUgrade)
        {
            if (shipUgrade)
            {
                player.Cash -= 10;
                player.ShipType = ShipHandler.GetShipUgrade(player.ShipType);
                AddToHistory(player.Capital + " upgrades to " + ShipHandler.GetShipTypeText(player.ShipType), false);
            }
        }

       private void ProceedPurchasePhase()
        {
            if (Players.Any(m=>m.DidAction == false))
            {
                var firstOrDefault = Players.OrderBy(p => p.PlayOrder).FirstOrDefault(m => m.DidAction == false);
                if (firstOrDefault != null)
                {
                    PlayOrder = firstOrDefault.PlayOrder;
                }
            }
            if (Players.TrueForAll(m => m.DidAction))
            {
                ProceedToNextPhase();
            }
        }

        #endregion Purchase

        #region PlayCard

        public bool PlayCard(Card card, int fee, string effect)
        {
            if (card == null)
            {
                AddToHistory(PlayerInTurn.Capital + " ends card play", false);
                ProceedCardPlayPhase();
                return true;
            }

            string cardName;
            switch (card.Name)
            {
                case "Ivory/Gold as Ivory":
                case "Ivory/Gold as Gold":
                    cardName = "Ivory/Gold";
                    break;
                case "Cloth/Wine as Cloth":
                case "Cloth/Wine as Wine":
                    cardName = "Cloth/Wine";
                    break;
                default:
                    cardName = card.Name;
                    break;
            }


            switch (card.Type)
            {
                //case Enums.CardType.Leader:
                default:
                    CardPlays.Add(new CardPlay
                    {
                        Card = card,
                        Fee = fee,
                        Capital = PlayerInTurn.Capital,
                        FirstLeaderPlay = (CardPlays.Count == 0 && card.Type == Enums.CardType.Leader)
                    });
                    var history = PlayerInTurn.Capital + " plays " + card.Name;
                    if (card.Type == Enums.CardType.Leader)
                    {
                        if (CardPlays.Any(m => m.Card.Type == Enums.CardType.Leader))
                        {
                            history += fee == 0 ? " no fee" : " fee $" + fee;
                            PlayerInTurn.Cash -= fee;
                        }
                        else
                        {
                            history += " (protected)";
                        }
                        if (PlayerInTurn.HasAdvance(Enums.AdvanceType.PrintedWord))
                        {
                            var earnedLeaderRebate = CardHandler.GetEarnedRebateFromLeader(card,
                                PlayerInTurn.AdvanceString, CardsLastingEffect);
                            if (earnedLeaderRebate > 0)
                            {
                                PlayerInTurn.Tokens += earnedLeaderRebate;
                                AddToHistory(
                                    PlayerInTurn.Capital + " earns $" + earnedLeaderRebate + " from " + card.Name, false);
                            }
                        }
                    }
                    AddToHistory(history, false);
                    break;
                case Enums.CardType.Good:
                    AddToHistory(PlayerInTurn.Capital + " plays " + card.Name + ":", false);
                    PayoutCommodityCard(card);
                    break;
                case Enums.CardType.Event:
                    switch (card.Name)
                    {
                        case "Armor":
                            ResolveArmor();
                            break;
                        case "Alchemist's Gold":
                            ResolveAlchemistsGold(effect);
                            break;
                        case "Black Death":
                            ResolveBlackDeath(effect);
                            break;
                        case "Civil War":
                            ResolveCivilWar(effect);
                            break;
                        case "The Crusades":
                            if (!ResolveTheCrusades(effect))
                            {
                                return false;
                            }
                            break;
                        case "Enlightened Ruler":
                            ResolveEnlightenedRuler();
                            break;
                        case "Famine":
                            ResolveFamine();
                            break;
                        case "Gunpowder":
                        case "Long Bow":
                            ResolveGunPowderAndLongBow(card.Name);
                            break;
                        case "Mysticism Abounds":
                            ResolveMysticismAbounds();
                            break;
                        case "Mongol Armies":
                            ResolveMongolArmies();
                            break;
                        case "Papal Decree":
                            ResolvePapalDecree(effect);
                            break;
                        case "Pirates/Vikings":
                            if (!ResolvePiratesVikings(effect))
                            {
                                return false;
                            };
                            break;
                        case "Rebellion":
                            ResolveRebellion();
                            break;
                        case "Religious Strife":
                            ResolveReligiousStrife();
                            break;
                        case "Revolutionary Uprisings":
                            ResolveRevolutionaryUprisings();
                            break;
                        case "Stirrups":
                            ResolveStirrups();
                            break;
                        case "War!":
                            ResolveWar(effect);
                            break;
                    }
                    break;
            }

            PlayerInTurn.Cards.Remove(PlayerInTurn.Cards.Find(m => m.Name == cardName));

            if (!card.MoveToRedrawPile)
            {
                RedrawPile.Add(card);
            }

            if (PlayerInTurn.Cards.Count == 0)
            {
                AddToHistory(PlayerInTurn.Capital + " has no cards om hand", false);
                ProceedCardPlayPhase();
            }
            return true;
        }

        private void PayoutCommodityCard(Card card)
        {
            var history = card.Name + " payout:";
            foreach (var player in Players)
            {
                var payout = GetCommodityPayout(card.Good, player);
                if (payout > 0)
                {
                    player.Cash += payout;
                }
                history += " " +player.CapitalShortName + "=" + payout;
            }
            AddToHistory(history, false);
            if (ShortageSurplus.Exists(m => m.Good == card.Good))
            {
                var item = ShortageSurplus.Find(m => m.Good == card.Good);
                ShortageSurplus.Remove(item);
                AddToHistory(item.IsShortage ? "Shortage" : "Surplus" + " is removed", false);
            }
        }

        public int GetCommodityPayout(Enums.GoodType good, Player player)
        {
            var provinceNumberModifier = ShortageSurplus.FindAll(m => m.Good == good).Sum(shortageSurplus => shortageSurplus.IsShortage ? 1 : -1);

            if (CardsInEffect.Any(m=>m.Key== "Stirrups"))
            {
                switch (good)
                {
                    case Enums.GoodType.Wool:
                    case Enums.GoodType.Grain:
                    case Enums.GoodType.Cloth:
                    case Enums.GoodType.Timber:
                        provinceNumberModifier ++;
                        break;
                    case Enums.GoodType.Silk:
                    case Enums.GoodType.Spice:
                        provinceNumberModifier --;
                        break;
                }
            }

            if (CardsInEffect.Any(m => m.Key == "Chinese Treasure Fleet"))
            {
                switch (good)
                {
                    case Enums.GoodType.Fur:
                    case Enums.GoodType.Gold:
                    case Enums.GoodType.Ivory:
                    case Enums.GoodType.Silk:
                    case Enums.GoodType.Spice:
                        provinceNumberModifier--;
                        break;
                }
            }

            var provinceNumber = Map.GetPlayerProvincesWithGood(player, good);
            if (provinceNumber > 0)
            {
                provinceNumber += provinceNumberModifier;
            }
            var payout = 0;
            if (provinceNumber > 0)
            {
                payout = GetCommodityValue(good, provinceNumber);
            }
            return payout;
        }

        public int GetSatelitePayout(Player player)
        {
            var sateliteNumber =
                Map.Provinces.Count(
                    m => m.MarketValue == 1 && m.Presences.Count == 1 && m.Presences[0].Capital == player.Capital);

            var payout = GetCommodityValue(Enums.GoodType.Grain, sateliteNumber);

            return payout;
        }

        private static int GetCommodityValue(Enums.GoodType good, int number)
        {
            switch (good)
            {
                case Enums.GoodType.Stone:
                    return number * number;
                case Enums.GoodType.Wool:
                    return number * number * 2;
                case Enums.GoodType.Timber:
                    return number * number * 3;
                case Enums.GoodType.Grain:
                    return number * number * 4;
                case Enums.GoodType.Cloth:
                case Enums.GoodType.Wine:
                    return number * number * 5;
                case Enums.GoodType.Metal:
                    return number * number * 6;
                case Enums.GoodType.Fur:
                    return number * number * 7;
                case Enums.GoodType.Silk:
                    return number * number * 8;
                case Enums.GoodType.Spice:
                    return number * number * 9;
                //case Enums.GoodType.Ivory:
                //case Enums.GoodType.Gold:
                default:
                    return number * number * 10;
            }
        }

        private void ProceedCardPlayPhase()
        {
            if (PlayOrder < Players.Count)
            {
                PlayOrder++;
            }
            else
            {
                ProceedToNextPhase();    
            }
        }

        public List<Card> CardHandWithDualGoodCardsSplitted(List<Card> cards)
        {
            if (!cards.Any(m => m.Name == "Cloth/Wine" || m.Name == "Ivory/Gold"))
            {
                return cards;
            }
            var splittedCards = new List<Card>();
            splittedCards.AddRange(cards);

            var ivoryGoldCard = cards.Find(m => m.Name == "Ivory/Gold");
            if (ivoryGoldCard != null)
            {
                splittedCards.Remove(ivoryGoldCard);
                splittedCards.Add(new Card { Name = "Ivory/Gold as Ivory" });
                splittedCards.Add(new Card { Name = "Ivory/Gold as Gold" });
            }
            var clothWineCard = cards.Find(m => m.Name == "Cloth/Wine");
            if (clothWineCard != null)
            {
                splittedCards.Remove(clothWineCard);
                splittedCards.Add(new Card { Name = "Cloth/Wine as Cloth" });
                splittedCards.Add(new Card { Name = "Cloth/Wine as Wine" });
            }
            return splittedCards;
        }

        private void ResolveAlchemistsGold(string capital)
        {
            var playerTarget = Players.Find(m => m.Capital.ToString() == capital);
            var loss = ((DivideByTwo(playerTarget.WrittenCash)) <= playerTarget.Cash) ? DivideByTwo(playerTarget.WrittenCash) : playerTarget.Cash;
            playerTarget.Cash -= (int)loss;
            AddToHistory(PlayerInTurn.Capital + " plays Alchemist's Gold on " + playerTarget.Capital + ". Loss $" + loss , false);
        }

        private void ResolveBlackDeath(string area)
        {
            AddToHistory(PlayerInTurn.Capital + " plays Black Death in Area " + area, false);
            foreach (var province in Map.Provinces.Where(province => province.Area == area).Where(province => province.Presences.Count > 0))
            {
                for (var index = province.Presences.Count - 1; index > -1; index--)
                {
                    if (province.Presences[index].Strength < province.MarketValue)
                    {
                        AddToHistory("Black death --> " + province.Name + " " + province.Presences[index].Capital + " loose $" + province.Presences[index].Strength, false);
                        province.Presences.RemoveAt(index);                                             
                    }
                    else
                    {
                        AddToHistory("Black death --> " + province.Name + " " + province.Presences[index].Capital + " DOM reduced to 1T", false);
                        province.Presences[0].Strength = 1;                                             
                    }
                }
            }
        }

        private void ResolveCivilWar(string capital)
        {
            var historyString = PlayerInTurn.Capital + " plays Civil War on " + capital;
            var targetedPlayer = Players.Find(m => m.Capital.ToString() == capital);
            var miseryIncrease = MiseryHandler.MiseryChangeByStep(targetedPlayer.Misery, 1);
            targetedPlayer.Misery += miseryIncrease;
            var province = Map.GetProvinceByName(capital);
            if (province.Presences.Count == 1 && province.Presences[0].Strength == province.MarketValue)
            {
                province.Presences[0].Strength = 1;
                historyString += capital + " reduced to a token.";
            }
            AddToHistory(historyString, false);
            ChangePlayOrder(targetedPlayer);
            Interrupt = "Civil War";
            InterruptPreservedOrder = PlayerInTurn.PlayOrder;
            PlayOrder = targetedPlayer.PlayOrder;
        }

        private bool ResolveTheCrusades(string provinceName)
        {
            var province = Map.GetProvinceByName(provinceName);
            if (province.Area != "VI")
            {
                AddToHistory("Error: The Crusades province was " +province.Name, true);
                return false;
            }

            AddToHistory(PlayerInTurn.Capital + " plays the Crusades and places dom in Aleppo", false);
            province.Presences = new List<Presence>
            {
                new Presence {Capital = PlayerInTurn.Capital, Locked = 0, Original = 0, Strength = province.MarketValue}
            };
            PlayerInTurn.Misery += MiseryHandler.GetMiseryIncrease(PlayerInTurn.Misery);
            return true;
        }

        

        // Tested
        private void ResolveEnlightenedRuler()
        {
            AddToHistory(PlayerInTurn.Capital + " plays Enlightened Ruler", false);
            CardsInEffect.Add(new Pair{Key="Enlightened Ruler", Value = PlayerInTurn.Capital.ToString()});
        }

        // Tested
        private void ResolveFamine()
        {
            var history = PlayerInTurn.Capital + " plays Famine: ";
            foreach (var player in Players)
            {
                var relief =
                    Map.Provinces.Count(
                        m =>
                            m.Good == Enums.GoodType.Grain &&
                            m.Presences.Any(n => n.Strength == m.MarketValue && n.Capital == player.Capital));

                var loss = (4 - relief > 0) ? 4 - relief : 0;
                player.Misery += loss;
                history += player.CapitalShortName + ":" + loss + ", ";
            }
            history = history.Remove(history.Length - 2, 1);
            AddToHistory(history,false);
        }
        
        private void ResolveArmor() // Test not needed
        {
            AddToHistory(PlayerInTurn.Capital + " plays Armor", false);
            CardsInEffect.Add(new Pair { Key = "Armor", Value = PlayerInTurn.Capital.ToString()});
        }
        private void ResolveGunPowderAndLongBow(string cardName)
        {
            var historyString = PlayerInTurn.Capital + " plays " + cardName;
            CardsInEffect.Add(new Pair { Key = cardName, Value = PlayerInTurn.Capital.ToString() });
            if (CardHandler.IsUnplayed(this, "Armor"))
            {
                Unplayable.Add("Armor");
                CardsInEffect = CardHandler.RemoveFromInEffect(CardsInEffect, "Armor");
                historyString += " Armor is unplayable.";
            }
            if (CardHandler.IsUnplayed(this, "Stirrups"))
            {
                Unplayable.Add("Stirrups");
                CardsInEffect = CardHandler.RemoveFromInEffect(CardsInEffect, "Stirrups");
                historyString += " Stirrups is unplayable.";
            }
            AddToHistory(historyString, false);
        }

        private void InterruptCleanUp()
        {
            PlayOrder = InterruptPreservedOrder;
            InterruptPreservedOrder = 0;
            Interrupt = null;
        }

        private void ResolveMongolArmies()
        {
            CardsInEffect.Add(new Pair { Key = "Mongol Armies", Value = string.Empty});
            CardsLastingEffect.Add("Mongol Armies");
            PlayerInTurn.Cash += 10;
            var historyString = PlayerInTurn.Capital + " plays Mongol Armies.";
            if (CardHandler.IsUnplayed(this, "The Crusades"))
            {
                Unplayable.Add("The Crusades");
                historyString += " the Crusades is unplayable";
            }
            AddToHistory(historyString, false);
        }

        // Tested
        private void ResolveMysticismAbounds()
        {
            foreach (var player in GetPlayersNotProtectedByEnlightenedRuler())
            {
                var scienceHeld = AdvanceHandler.GetNumberofAdvancesInGroup(player, Enums.AdvanceGroup.Science);
                var newMisery = MiseryHandler.MiseryChangeByStep(player.Misery, 4 - scienceHeld);
                AddToHistory("Mysticism Abounds --> " + player.Capital + " + MI:" + (newMisery - player.Misery), false);
                player.Misery = newMisery;
            }
        }

        // No test needed
        private void ResolvePapalDecree(string scienceGroup)
        {
            var religiousStrifeEffect = CardsInEffect.Find(m => m.Key == "Religious Strife");
            if (religiousStrifeEffect != null)
            {
                AddToHistory("Papal decree has no effect due to religious Strife", false);
                CardsInEffect.Remove(religiousStrifeEffect);
            }
            else
            {
                CardsInEffect.Add(new Pair {Key = "Papal Decree", Value = scienceGroup});
                AddToHistory("Papal decree --> bans buys in " + scienceGroup + " this turn", false);
            }
        }

        private bool ResolvePiratesVikings(string effect)
        {
            var provinceNames = effect.Split(',').ToList();
            if (provinceNames.Count != Epoch)
            {
                AddToHistory("Pirate/Vikings province list does not match Epoch: " + effect, true);
                return false;
            }
            if (provinceNames.Select(provinceName => Map.GetProvinceByName(provinceName)).Any(area => area.MarketValue == 1))
            {
                AddToHistory("Pirates/Vikings area is not a DOM, " + effect, true);
                return false;
            }
            var reducedProvincesString = string.Empty;
            foreach (var province in Map.Provinces)
            {
                foreach (var provinceHit in provinceNames)
                {
                    if (province.NameAsText == provinceHit)
                    {
                        if (province.Presences.Count != 1 || province.Presences[0].Strength != province.MarketValue)
                        {
                            AddToHistory("ERROR. Not a dom; " + province.Name, true);
                            return false;
                        }
                        province.Presences[0].Strength = 1;
                        reducedProvincesString += province.Name + ", ";
                    }
                }
            }
            reducedProvincesString = reducedProvincesString.Remove(reducedProvincesString.Length - 2);

            AddToHistory(PlayerInTurn.Capital + " plays Pirates/Vikings --> reduces " + reducedProvincesString + " to 1T", false);
            return true;
        }

        private void ResolveRebellion()
        {
            var history = PlayerInTurn.Capital + " plays Rebellion: ";
            foreach (var player in Players)
            {
                var fine = (player.Cards.Count > 3) ? player.Cards.Count - 3 : 0;
                player.Tokens -= fine;
                history += player.CapitalShortName + ":" + fine + ", ";
            }
            history = history.Remove(history.Length - 2, 2);
            AddToHistory(history, false);
        }

        private void ResolveReligiousStrife()
        {
            var history = PlayerInTurn.Capital + " plays Religious Strife: ";
            foreach (var player in GetPlayersNotProtectedByEnlightenedRuler())
            {
                var religiousAdvances = AdvanceHandler.GetNumberofAdvancesInGroup(player, Enums.AdvanceGroup.Religion);
                var newMisery = MiseryHandler.MiseryChangeByStep(player.Misery, religiousAdvances);
                history += player.CapitalShortName + ":" + (newMisery - player.Misery) + ", ";
                player.Misery = newMisery;
            }
            history = history.Remove(history.Length - 2, 2);
            AddToHistory(history, false);

            var papalDecreeInEffect = CardsInEffect.Find(m => m.Key == "Papal Decree");
            if (papalDecreeInEffect != null)
            {
                CardsInEffect.Remove(papalDecreeInEffect);
                AddToHistory("Religious Strife removes Papal Decree ban", false);
            }
            else
            {
                CardsInEffect.Add(new Pair {Key = "Religious Strife"});
            }
            if (Epoch == 3 && CardHandler.IsUnplayed(this, "Papal Decree"))
            {
                Unplayable.Add("Papal Decree");
                AddToHistory("Religious Strife make Papal Decree unplayable.", false);
            }
        }

        private void ResolveRevolutionaryUprisings()
        {
            foreach (var player in GetPlayersNotProtectedByEnlightenedRuler())
            {
                var commerceHeld = AdvanceHandler.GetNumberofAdvancesInGroup(player, Enums.AdvanceGroup.Commerce);
                var newMisery = MiseryHandler.MiseryChangeByStep(player.Misery, commerceHeld);
                AddToHistory("Revolutionary Uprisings --> " + player.Capital + " + MI:" + (newMisery - player.Misery), false);
                player.Misery = newMisery;
            }
        }

        private void ResolveStirrups()
        {
            CardsInEffect.Add(new Pair { Key = "Stirrups", Value = PlayerInTurn.Capital.ToString() });
            AddToHistory(PlayerInTurn.Capital + " plays Stirrups", false);
        }

        private void ResolveWar(string capital)
        {
            var attackerModifier = PlayerInTurn.WarModifier(CardPlays);
            var attacker = Players.Find(m => string.Equals(m.Capital.ToString(), PlayerInTurn.Capital.ToString(), StringComparison.CurrentCultureIgnoreCase));
            var defender = Players.Find(m => m.Capital.ToString() == capital);
            var defenderModifier = defender.WarModifier(CardPlays);
            AddToHistory(attacker.Capital + " (+" + attackerModifier + ") plays War! on " + defender.Capital + " (+" + defenderModifier + ")", false);

            var attackerDie = Common.GetRandomNumber(6) + 1 + attackerModifier;
            var defenderDie = Common.GetRandomNumber(6) + 1 + defenderModifier;
            int attackerMiseryIncreease;
            int defenderMiseryIncrease;
            while (attackerDie == defenderDie)
            {
                attackerMiseryIncreease = MiseryHandler.GetMiseryIncrease(attacker.Misery);
                defenderMiseryIncrease = MiseryHandler.GetMiseryIncrease(defender.Misery);
                AddToHistory(attacker.Capital + " MI:+" + attackerMiseryIncreease + " and " + defender.Capital + "MI:+" + defenderMiseryIncrease + " both rolled " + attackerDie + " and must reroll", false);
                attacker.Misery += attackerMiseryIncreease;
                defender.Misery += defenderMiseryIncrease;
                attackerDie = Common.GetRandomNumber(6) + 1 + attackerModifier;
                defenderDie = Common.GetRandomNumber(6) + 1 + defenderModifier;
            }
            attackerMiseryIncreease = MiseryHandler.GetMiseryIncrease(attacker.Misery);
            defenderMiseryIncrease = MiseryHandler.GetMiseryIncrease(defender.Misery);
            InterruptPreservedOrder = attacker.PlayOrder;
            WarResult = new WarResult
            {
                Loss = (attackerDie > defenderDie) ? attackerDie - defenderDie : defenderDie - attackerDie,
                Winner = (attackerDie > defenderDie) ? attacker.Capital : defender.Capital,
                Looser = (attackerDie > defenderDie) ? defender.Capital : attacker.Capital
            };
            if (attackerDie > defenderDie)
            {
                attacker.Misery += attackerMiseryIncreease;
                defenderMiseryIncrease = MiseryHandler.MiseryChangeByStep(defender.Misery, 2);
                defender.Misery += defenderMiseryIncrease;
                AddToHistory(attacker.Capital + " MI:+" + attackerMiseryIncreease + " wins " +attackerDie + ":" + defenderDie + " over " + defender.Capital + " MI:+" + defenderMiseryIncrease, false);
                Interrupt = "War," + PlayerInTurn.Capital + "," + defender.Capital +"," + (attackerDie - defenderDie);
                ResolveWarAuto(attacker, defender, attackerDie - defenderDie);
            }
            else
            {
                attackerMiseryIncreease = MiseryHandler.MiseryChangeByStep(PlayerInTurn.Misery, 2);
                attacker.Misery += attackerMiseryIncreease;
                defender.Misery += defenderMiseryIncrease;
                AddToHistory(defender.Capital + "MI:+" + defenderMiseryIncrease + " wins " + attackerDie + ":" + defenderDie + " over " + attacker.Capital + " MI:+" + attackerMiseryIncreease + " " + defenderDie + ":" + attackerDie, false);
                Interrupt = "War," + defender.Capital + "," + attacker.Capital + "," + (defenderDie - attackerDie);
                PlayOrder = defender.PlayOrder;
                ResolveWarAuto(defender, attacker, defenderDie - attackerDie);
            }
        }

        private void ResolveWarAuto(Player winner, Player looser, int result)
        {
            if (Map.GetPlayerNoDoms(looser.Capital) <= result)
            {
                foreach (var province in looser.GetDoms(Map))
                {
                    province.Presences[0].Capital = winner.Capital;
                }
                AddToHistory(looser.Capital +  " loose all it's DOMs to " + winner.Capital + " as result of the War", false);
                InterruptCleanUp();
            }
        }

        public List<Player> GetPlayersNotProtectedByEnlightenedRuler()
        {
            var players = new List<Player>();
            var playerCapital = (CardsInEffect.Find(m => m.Key == "Enlightened Ruler") != null) ? CardsInEffect.Find(m => m.Key == "Enlightened Ruler").Value : string.Empty;
            players.AddRange(Players.Where(player => player.Capital.ToString() != playerCapital));
            return players;
        }
        #endregion PlayCard

        #region DrawCards
        public void AddSurplusShortage(string good, bool shortage)
        {
            if (good == string.Empty)
            {
                AddToHistory(PlayerInTurn.Capital + " does not add shortage or surplus", false);
            }
            else
            {
                AddToHistory(PlayerInTurn.Capital + " adds" + ((shortage) ? " shortage" : " surplus") + " in " + good, false);
                ShortageSurplus.Add(new ShortageSurplus{Good = GetGoodByName(good), IsShortage = shortage});
            }
            CheckForShortageSurplusCancellation();
            ProceedToNextPhase();
        }

        private void CheckForShortageSurplusCancellation()
        {
            switch (ShortageSurplus.Count)
            {
                case 2:
                    if (ShortageSurplusGoodsCancelEachOther(0,1))
                    {
                        AddToHistory("Shortage and Surplus in " + ShortageSurplus[0].Good + " is cancelled", false);
                        ShortageSurplus.RemoveAt(1);
                        ShortageSurplus.RemoveAt(0);
                    }
                    break;
                case 3:
                    if (ShortageSurplusGoodsCancelEachOther(0, 2))
                    {
                        AddToHistory("Shortage and Surplus in " + ShortageSurplus[0].Good + " is cancelled", false);
                        ShortageSurplus.RemoveAt(2);
                        ShortageSurplus.RemoveAt(0);
                    }
                    else if (ShortageSurplusGoodsCancelEachOther(1, 2))
                    {
                        AddToHistory("Shortage and Surplus in " + ShortageSurplus[1].Good + " is cancelled", false);
                        ShortageSurplus.RemoveAt(2);
                        ShortageSurplus.RemoveAt(1);
                    }
                    break;
            }
        }

        private bool ShortageSurplusGoodsCancelEachOther(int index1, int index2)
        {
            return ShortageSurplus[index1].Good == ShortageSurplus[index2].Good &&
                   ShortageSurplus[index1].IsShortage != ShortageSurplus[index2].IsShortage;
        }

        public string DrawCard(Player player, int tokens, string text)
        {
            CheckDeck();
            var card = CardHandler.DrawCard(DrawPile);
            player.Cards.Add(card);
            if (tokens > 0)
            {
                player.Tokens -= tokens;
                AddToHistory(player.Capital + " buys a card for $" + tokens, false);
            }
            else
            {
                AddToHistory(player.Capital + " draws a card " + text, false);
            }
            return card.Name;
        }

        private void CheckDeck()
        {
            if (DrawPile.Count != 0) return;
            switch (Epoch)
            {
                case 1:
                    Epoch++;
                    DrawPile = CardHandler.Shuffle(RedrawPile, CardHandler.CreateEpoch2Cards());
                    AddToHistory("Entered Phase 2. Epoch 2 cards shuffled into deck", false);
                    break;
                case 2:
                    Epoch++;
                    DrawPile = CardHandler.Shuffle(RedrawPile, CardHandler.CreateEpoch3Cards());
                    AddToHistory("Entered Phase 3. Epoch3 cards shuffled into deck", false);
                    break;
                default:
                    EndPlay = true;
                    DrawPile = CardHandler.Shuffle(RedrawPile, new List<Card>());
                    AddToHistory("Card deck is emply. Game end triggered", false);
                    break;
            }
        }

        private void DrawCards()
        {
            foreach (var player in Players.OrderBy(m => m.PlayOrder))
            {
                DrawCard(player, 0, string.Empty);
            }
        }
        

        #endregion DrawCards

        #region Buy Cards


        public void BuyCard()
        {
            DrawCard(PlayerInTurn, 1, string.Empty);
            ResolveBuyCard();
        }

        private void BuyCardPhasePrepare()
        {
            Phase = Enums.GamePhase.BuyCard;
            if (AdvanceHandler.AnyPlayerHasAdvance(Players, Enums.AdvanceType.UrbanAscendancy))
            {
                PlayOrder = 0;
                ResolveBuyCard();
            }
            else
            {
                ProceedToNextPhase();
            }
        }

        private void ResolveBuyCard()
        {
            var firstOrDefault = Players.OrderBy(m => m.PlayOrder).FirstOrDefault(m => m.PlayOrder > PlayOrder && m.HasAdvance(Enums.AdvanceType.UrbanAscendancy));
            if (firstOrDefault != null)
            {
                PlayOrder = firstOrDefault.PlayOrder;
            }
            else
            {
                ProceedToNextPhase();
            }
        }
        #endregion 

        #region MoveToRedrawPile Card
        private void DiscardPhasePrepare()
        {
            Phase = Enums.GamePhase.Discard;
            PlayOrder = 0;
            ResolveDiscard();
        }

        public void DiscardCard(Player player, Card card, bool moveToDrawPile)
        {
            player.Cards.Remove(card);
            if (moveToDrawPile)
            {
                DrawPile.Add(card);
            }
            AddToHistory(player.Capital + " discards " + card.Name, false);
            if (!card.MoveToRedrawPile)
            {
                RedrawPile.Add(card);
            }
        }

        public void DiscardInitialCard(Player player, Card discardedCard)
        {
            DrawPile.Add(discardedCard);
            player.Cards.Remove(discardedCard);
            player.DidAction = true;
            ResolveInitialDiscard();
        }

        public void ResolveDiscard()
        {
            var firstOrDefault = Players.OrderBy(m => m.PlayOrder).FirstOrDefault(m => m.PlayOrder > PlayOrder && m.HasAdvance(Enums.AdvanceType.MasterArt));
            if (firstOrDefault != null)
            {
                PlayOrder = firstOrDefault.PlayOrder;
                return;
            }
            ProceedToNextPhase();
        }

        public void ResolveInitialDiscard()
        {
            if (Players.TrueForAll(m => m.Cards.Count == 2))
            {
                ProceedToNextPhase();
            }
        }
        #endregion

        #region Shortage/Surplus
        public void BuyRemovalSurplusShortage(int id)
        {
            if (id < 0)
            {
                AddToHistory(PlayerInTurn.Capital + " leaves shortage/surplus as is", false);
            }
            else
            {
                if (ShortageSurplus.Count == 0 || PlayerInTurn.Tokens == 0)
                {
                    return;
                }
                var good = ShortageSurplus[id].Good;
                var mode = ShortageSurplus[id].IsShortage;

                ShortageSurplus.RemoveAt(id);
                PlayerInTurn.Tokens -= 1;
                AddToHistory(PlayerInTurn.Capital + " removes " + (mode ? "surplus" : "shortage") + " in " + good, false);
            }
            ProceedToNextPhase();
        }

        private void RemoveShortageSurplusPhasePrepare()
        {
            Phase = Enums.GamePhase.RemoveShortageSurplus;
            PlayOrder = 1;

            if (ShortageSurplus.Count == 0)

            {
                ProceedToNextPhase();
            }
        }

        private void AddShortageSurplusPhasePrepare()
        {
            Phase = Enums.GamePhase.AddShortageSurplus;
            if (AdvanceHandler.AnyPlayerHasAdvance(Players, Enums.AdvanceType.WindWatermill))
            {
                PlayOrder = Players.OrderBy(o => o.PlayOrder).First(m => m.HasAdvance(Enums.AdvanceType.WindWatermill)).PlayOrder;
            }
            else
            {
                ProceedToNextPhase();
            }

        }

        public void RollShortageSurplus()
        {
            ShortageSurplus = new List<ShortageSurplus> {RollSurplusShortageDie(), RollSurplusShortageDie()};
            if (ShortageSurplus[0].Good == ShortageSurplus[1].Good && ShortageSurplus[0].IsShortage != ShortageSurplus[1].IsShortage)
            {
                ShortageSurplus.Clear();
            }
        }

        private static ShortageSurplus RollSurplusShortageDie()
        {
            var isShortage = Common.GetRandomNumber(2) == 1;
            var die1 = Common.GetRandomNumber(6) + 1;
            var die2 = Common.GetRandomNumber(6) + 1;

            var goodType = GetGoodByNumber(die1 + die2);

            return new ShortageSurplus{Good = goodType, IsShortage = isShortage};
        }

        private static Enums.GoodType GetGoodByNumber(int number)
        {
            switch (number)
            {
                case 2:
                    return Enums.GoodType.Stone;
                case 3:
                    return Enums.GoodType.Wool;
                case 4:
                    return Enums.GoodType.Timber;
                case 5:
                    return Enums.GoodType.Grain;
                case 6:
                    return Enums.GoodType.Cloth;
                case 7:
                    return Enums.GoodType.Wine;
                case 8:
                    return Enums.GoodType.Metal;
                case 9:
                    return Enums.GoodType.Fur;
                case 10:
                    return Enums.GoodType.Silk;
                case 11:
                    return Enums.GoodType.Spice;
                default:
                case 12:
                    return Enums.GoodType.Gold;
            }
        }
        private static Enums.GoodType GetGoodByName(string good)
        {
            switch (good.ToLower())
            {
                case "stone":
                    return Enums.GoodType.Stone;
                case "wool":
                    return Enums.GoodType.Wool;
                case "timber":
                    return Enums.GoodType.Timber;
                case "grain":
                    return Enums.GoodType.Grain;
                case "cloth":
                    return Enums.GoodType.Cloth;
                case "wine":
                    return Enums.GoodType.Wine;
                case "metal":
                    return Enums.GoodType.Metal;
                case "fur":
                    return Enums.GoodType.Fur;
                case "silk":
                    return Enums.GoodType.Silk;
                case "spice":
                    return Enums.GoodType.Spice;
                default:
                case "gold":
                    return Enums.GoodType.Gold;
            }
        }
        #endregion Shortage/Surplus

        #region Select capital
        public List<Enums.Capital> AvailableCapitals()
        {
            var list = new List<Enums.Capital>();
            if (!Players.Exists(m => m.Capital == Enums.Capital.Barcelona)) { list.Add(Enums.Capital.Barcelona); }
            if (!Players.Exists(m => m.Capital == Enums.Capital.Genoa)) { list.Add(Enums.Capital.Genoa); }
            if (Players.Count > 5 && !Players.Exists(m => m.Capital == Enums.Capital.Hamburg)) { list.Add(Enums.Capital.Hamburg); }
            if (Players.Count > 4 && !Players.Exists(m => m.Capital == Enums.Capital.London)) { list.Add(Enums.Capital.London); }
            if (Players.Count > 3 && !Players.Exists(m => m.Capital == Enums.Capital.Paris)) { list.Add(Enums.Capital.Paris); }
            if (!Players.Exists(m => m.Capital == Enums.Capital.Venice)) { list.Add(Enums.Capital.Venice); }
            return list;
        }

        public void MakeCapitalBid(Player player, int bid)
        {
            if (Phase == Enums.GamePhase.CapitalBid && bid > -1 && bid < 41) 
            {
                player.Bid = bid;
                player.DidAction = true;
                if (Players.TrueForAll(m=>m.DidAction))
                {
                    DetermineCapitalOrder();
                    PlayOrder = 0;
                    // Order the player in opposite order of Capital order.
                    foreach (var pl in Players.OrderByDescending(m=>m.CapitalOrder))
                    {
                        pl.PlayOrder = Players.Count + 1 - pl.CapitalOrder;
                    }
                    ProceedToNextPhase();
                }
            }
        }

        private void DetermineCapitalOrder()
        {
            var capitalOrder = Players.Count();
            while (!Players.TrueForAll(m=>m.CapitalOrder > 0))
            {
                var highestBid = Players.FindAll(m => m.CapitalOrder < 1).Max(m=>m.Bid);
                var playersWithCurrentHighestBid = Players.FindAll(m => m.Bid == highestBid && m.CapitalOrder < 1);
                var selectedPlayerIndex = Common.GetRandomNumber(playersWithCurrentHighestBid.Count);
                playersWithCurrentHighestBid[selectedPlayerIndex].CapitalOrder = capitalOrder;
                playersWithCurrentHighestBid[selectedPlayerIndex].PlayOrder = capitalOrder;
                capitalOrder--;
            }
            foreach (var player in Players.OrderByDescending(m=>m.CapitalOrder))
            {
                AddToHistory(player.Nick + " capital bid: " + player.Bid + ". Capital order: " + player.CapitalOrder, false); 
            }
        }

        public void PayAndSelectCapital(Player player, Enums.Capital capital)
        {
            if (PlayerInTurn.PlayOrder == PlayOrder)
            {
                player.Capital = capital;
                player.Cash -= player.Bid;
                //Map.AddDom(capital, Map.GetProvinceByName(capital.ToString()));
                AddToHistory(player.Nick + " pays $" + player.Bid + ", and selects " + capital, false);
                ProgressCapitalChoice(player);
            }
        }

        private void ProgressCapitalChoice(Player player)
        {
            if (PlayOrder == NumberOfPlayers)
            {
                ProceedToNextPhase();
            }
            else
            {
                PlayOrder++;
                // Auto choose last player 
                if (PlayOrder == NumberOfPlayers)
                {
                    PayAndSelectCapital(PlayerInTurn, GetRemainingCapital());
                }
            }
        }

        public List<Enums.Capital> CapitalsNames()
        {
            var list = new List<Enums.Capital>
            {
                Enums.Capital.NotSelected,
                Enums.Capital.Barcelona,
                Enums.Capital.Genoa,
                Enums.Capital.Venice
            };
            if (Players.Count > 5) { list.Add(Enums.Capital.Hamburg); }
            if (Players.Count > 4) { list.Add(Enums.Capital.London); }
            if (Players.Count > 3) { list.Add(Enums.Capital.Paris); }
            return list;
        }

        private Enums.Capital GetRemainingCapital()
        {
            var list = CapitalsNames();
            foreach (var player in Players)
            {
                list.Remove(player.Capital);
            }
            return list.First();
        }
        #endregion Select capital
        
        #region Cards
        public List<Card> DrawCards(int numberOfCards)
        {
            var cards = new List<Card>();
            for (var index = 0; index < numberOfCards; index++)
            {
                var card = DrawPile[Common.GetRandomNumber(DrawPile.Count)];
                DrawPile.Remove(card);
                cards.Add(card);

                if (DrawPile.Count == 0)
                {
                    if (Epoch < 3)
                    {
                        Epoch++;
                        var epochDeck = Epoch == 2 ? CardHandler.CreateEpoch2Cards() : CardHandler.CreateEpoch3Cards();
                        DrawPile = CardHandler.Shuffle(RedrawPile, epochDeck);
                    }
                    else
                    {
                        EndPlay = true;
                    }
                }
            }
            return cards;
        }

        public void ResolveInterrupt(string id)
        {
            if (Interrupt == "Civil War")
            {
                ResolveInterrupt("Civil War", (id == "true") ? "cash" : "tokens");
            }
        }

        public void ResolveInterrupt(string card, string resolve)
        {
            var historyString = string.Empty;
            if (card == "Civil War")
            {
                if (resolve == "cash")
                {
                    var halfWrittenCash = DivideByTwo(PlayerInTurn.WrittenCash);
                    historyString = "Civil War --> " + PlayerInTurn.Capital + " gives up $" + halfWrittenCash;
                    PlayerInTurn.Cash -= halfWrittenCash;
                }
                else if (resolve == "tokens")
                {
                    var halfTokens = DivideByTwo(PlayerInTurn.Tokens);
                    historyString = "Civil War --> " + PlayerInTurn.Capital + " gives up T" + halfTokens;
                    PlayerInTurn.Tokens -= halfTokens;
                }
                else
                {
                    return;
                }
                AddToHistory(historyString,false);
                InterruptCleanUp();
            }
        }

        #endregion Cards

        #region Expansion
        public void BuyCardExpand()
        {
            if (PlayerInTurn.Tokens > ExpansionCardPurchase)
            {
                BuyCardExpansion = DrawCard(PlayerInTurn, ExpansionCardPurchase, string.Empty);
                ExpansionCardPurchase++;
                PlayerInTurn.BoughtCardExpansion = true;
                ResolveExpansion();
            }
        }

        private void ResolveExpansion()
        {
            if (PlayerInTurn.Tokens == 0)
            {
                ProceedExpansionPhase();
            }
        }

        private void ProceedExpansionPhase()
        {
            if (PlayOrder < Players.Count)
            {
                PlayOrder++;
            }
            else
            {
                ProceedToNextPhase();
            }
        }

        public void EnligthenmentChangeOrder(int orderModifier)
        {
            var changeWithPlayer = Players.Find(m => m.PlayOrder == PlayOrder + orderModifier);
            var playerInTurn = Players.Find(m => m.PlayOrder == PlayOrder);
            AddToHistory(PlayerInTurn.Capital + " changes play position with " + changeWithPlayer.Capital, false);
            changeWithPlayer.PlayOrder -= orderModifier;
            playerInTurn.PlayOrder += orderModifier;
            Interrupt = null;
            PlayOrder = InterruptPreservedOrder;
        }

        public void Expand(Map map)
        {
            ResolveUpdatesInSubmittedMap(map);
            var spentTokens = GetUsedTokensInExpansion(map);
            PlayerInTurn.Tokens -= spentTokens;
            

            if (map.EndTurn)
            {
                AddToHistory(PlayerInTurn.Capital + " ends expansion", false);
                ProceedExpansionPhase(true);
            }
        }

        private void ProceedExpansionPhase(bool endTurn)
        {
            if (endTurn || PlayerInTurn.Tokens == 0)
            {
                if (PlayerInTurn.PlayOrder == NumberOfPlayers)
                {
                    ProceedToNextPhase();
                }
                else
                {
                    PlayOrder++;
                }
            }
        }
        
        private void MakeAllProvincesExpandable()
        {
            foreach (var province in Map.Provinces)
            {
                foreach (var presence in province.Presences)
                {
                    presence.Original = presence.Strength;
                }
            }
        }

        private void ResolveUpdatesInSubmittedMap(Map submittedMap)
        {
            foreach (var submittedProvince in GetAllProvincesByCapital(submittedMap, PlayerInTurn.Capital))
            {
                if (submittedProvince.Presences == null) continue;
                foreach (var submittedPresence in submittedProvince.Presences)
                {
                    if (submittedPresence.Capital == PlayerInTurn.Capital)
                    {
                        if (GetStrengthInProvince(submittedProvince) > submittedProvince.MarketValue)
                        {
                            ResolveCompetetion(submittedProvince, PlayerInTurn);
                        }
                        else
                        {
                            var gamePresence = GetGameMapPresence(PlayerInTurn.Capital, submittedProvince.NameAsText);
                            if (submittedPresence.Strength > 0)
                            {
                                if (gamePresence == null)
                                {
                                    Map.Provinces.Find(m => m.Name == submittedProvince.Name)
                                        .Presences.Add(new Presence
                                        {
                                            Capital = PlayerInTurn.Capital,
                                            Original = 0,
                                            Locked = submittedPresence.Strength,
                                            Strength = submittedPresence.Strength
                                        });
                                    var text = (submittedPresence.Strength == submittedProvince.MarketValue)
                                        ? " Dom"
                                        : submittedPresence.Strength + "T";
                                    AddToHistory(PlayerInTurn.Capital + " establishes" + text + " in " + submittedProvince.Name, false);
                                }
                                else
                                {
                                    gamePresence.Strength = submittedPresence.Strength;
                                }
                            }
                        }
                    }
                }
            }
        }

        private Presence GetGameMapPresence(Enums.Capital capital, string provinceName)
        {
            var province = Map.Provinces.Find(m => m.NameAsText == provinceName);
            return province.Presences.Find(m => m.Capital == capital);
        }

        private static IEnumerable<Province> GetAllProvincesByCapital(Map submittedMap, Enums.Capital capital)
        {
            var list = new List<Province>();
            foreach (var province in submittedMap.Provinces.Where(province => province.Presences != null))
            {
                list.AddRange(from presence in province.Presences where presence.Capital == capital select province);
            }
            return list;
        }

        //private void DoCompetitionRoll(Province province)
        //{
        //    var green = Common.GetRandomNumber(6);
        //    var black = Common.GetRandomNumber(6);
        //    var white = Common.GetRandomNumber(6);
        //    var victory = (black > white || green > GetModifiedPlayOrder(PlayerInTurn.PlayOrder));
        //    var gameProvince = Map.Provinces.Find(m => m.Name == province.Name);
        //    if (victory)
        //    {
        //        var originalPresence = province.Presences.Find(m => m.Capital == PlayerInTurn.Capital).Original;
        //        gameProvince.Presences.Clear();
        //        gameProvince.Presences.Add(new Presence { Capital = PlayerInTurn.Capital, Original = originalPresence, Strength = province.MarketValue });
        //    }
        //    else
        //    {
        //        gameProvince.Presences.Remove(gameProvince.Presences.Find(m => m.Capital == PlayerInTurn.Capital));
        //    }
        //    AddToHistory(PlayerInTurn.Capital + ((victory) ? " won" : " lost") + " in " + province.Name + ". B:" + black + " W:" + white + " G:" + green, false);

        //}

        private static int GetStrengthInProvince(Province province)
        {
            return province.Presences.Sum(m => m.Strength);
        }

        public int GetModifiedPlayOrder(int order)
        {
            switch (NumberOfPlayers)
            {
                case 6:
                case 5:
                    return order;
                case 4:
                    return (order == 4) ? 5 : order;
                default:
                    return (order == 1) ? 1 : order + 1;
            }
        }

        private void ResolveTaxIncome()
        {
            foreach (var player in Players.OrderBy(m => m.PlayOrder))
            {
                GiveTaxIncome(player, GetModifiedPlayOrder(player.PlayOrder));
            }
        }

        private void ResolveHolyIndulgence()
        {
            var playersWithoutHolyIndulgence = AdvanceHandler.AllPlayersNotHaveAdvance(Players, Enums.AdvanceType.HolyIndulgence);
            if (playersWithoutHolyIndulgence.Count == NumberOfPlayers)
            {
                return;
            }
            var playersWithHolyIndulgence = AdvanceHandler.AllPlayersHaveAdvance(Players, Enums.AdvanceType.HolyIndulgence);
            var pays = playersWithHolyIndulgence.Count * ((NumberOfPlayers > 4) ? 2 : 3);
            foreach (var player in playersWithoutHolyIndulgence)
            {   // Player will not pay more than in stock
                player.Tokens -= (pays >= player.Tokens) ? pays : player.Tokens;
                AddToHistory("Holy Indulgence. " + player.Capital + " pays $" + pays, false);
            }
            var receives = playersWithoutHolyIndulgence.Count * ((NumberOfPlayers > 4) ? 2 : 3);
            foreach (var player in playersWithHolyIndulgence)
            {
                player.Tokens += receives;
                AddToHistory("Holy Indulgence. " + player.Capital + " receives $" + receives, false);
            }
        }

        private void GiveTaxIncome(Player player, int position)
        {
            var tokens = (position - 1) * 5;
            var misery = (position - 1) * 1;
            player.Tokens += tokens;
            player.Misery += misery;
            AddToHistory(player.Capital + " tax income: " + tokens + "T " + misery + "M", false);

        }

        private int GetUsedTokensInExpansion(Map map)
        {
            var count = 0;
            foreach (var province in Map.Provinces)
            {
                if (province.Presences == null) continue;
                foreach (var presence in province.Presences.FindAll(m => m.Capital == PlayerInTurn.Capital))
                {
                    if (presence.Strength > presence.Original)
                    {
                        count += (presence.Strength - presence.Original);
                    }
                }
            }
            return count;
        }

        private bool MayEnterProvince(Province province, Player player)
        {
            if (province.Area == "New World")
            {
                return player.HasAdvance(Enums.AdvanceType.NewWorld) && !player.IsAtMaximumOverseasProvinces(Map);
            }

            if (province.Area == "Far East")
            {
                return player.HasAdvance(Enums.AdvanceType.NewWorld) && !player.IsAtMaximumOverseasProvinces(Map); ;
            }

            if (province.Area == "V")
            {
                return player.HasAdvance(Enums.AdvanceType.OverlandEast);
            }

            if (province.Name == Enums.Pname.Iceland || province.Name == Enums.Pname.WestAfrica)
            {
                return player.HasAdvance(Enums.AdvanceType.TheHeavens);
            }
            return true;
        }

        public int GetNeededTokenNumberForCompetition(Player attacker, Province province)
        {
            if (!province.Presences.Exists(presence => presence.Capital != attacker.Capital))
            {
                return 0;
            }

            var neededTokens = province.MarketValue;
            var otherPresenceCount = 0;
            foreach (var presence in province.Presences.Where(presence => presence.Capital != attacker.Capital))
            {
                otherPresenceCount += presence.Strength;
            }
            neededTokens += otherPresenceCount;
            var ownPresence = province.Presences.Find(m => m.Capital == attacker.Capital);
            if (ownPresence != null)
            {
                if (ownPresence.Strength == province.MarketValue)
                {
                    return 0;
                }
                neededTokens -= ownPresence.Strength;
            }

            // add tokens from defence from Satelites
            neededTokens += province.GetDefenseTokenNumberFromSatelites(attacker, Map);

            // reduce tokens from Satelites using Cosmopilitan
            neededTokens -= province.GetAttackTokenNumberFromSatelites(attacker, Map);

            // add tokens from Nationalism
            foreach (var presence in province.Presences.FindAll(m=>m.Capital != attacker.Capital))
            {
                var player = Players.Find(m => m.Capital == presence.Capital);
                if (player.HomeArea == province.Area && player.HasAdvance(Enums.AdvanceType.Nationalism))
                {
                    neededTokens ++; // Only add nationalism once even if more than one nation can apply it
                    break;
                }
            }

            // reduce tokens from nationalism
            if (attacker.HomeArea == province.Area && attacker.HasAdvance(Enums.AdvanceType.Nationalism))
            {
                neededTokens--;
            }

            // Add tokens when attacking a foreign capital
            {
                foreach (var presence in province.Presences.FindAll(m => m.Capital != attacker.Capital))
                {
                    var player = Players.Find(m => m.Capital == presence.Capital);
                    if (player.Capital.ToString() == province.NameAsText)
                    {
                        neededTokens += province.MarketValue;
                    }
                }
            }

            // Reduce tokens when adding own capital
            {
                if (province.NameAsText == attacker.Capital.ToString())
                {
                    neededTokens -= province.MarketValue;
                }
            }

            // Add for cards
            foreach (var cardPlay in CardPlays)
            {
                if (cardPlay.Capital == attacker.Capital &&
                    (cardPlay.Card.Name == "Stirrups" || cardPlay.Card.Name == "Gunpowder" ||
                     cardPlay.Card.Name == "Long Bow"))
                {
                    neededTokens --;
                }
            }

            return neededTokens < province.MarketValue ? province.MarketValue : neededTokens;
        }

        private bool ResolveCompetetion(Province submittedProvince, Player attacker)
        {
            var province = Map.Provinces.Find(m => m.Name == submittedProvince.Name);
            var neededTokens = GetNeededTokenNumberForCompetition(attacker, province);
            if (attacker.Tokens < neededTokens)
            {
                AddToHistory(attacker.Capital + " did not have " + neededTokens + "T to attack " + province.Name, false);
                return false;
            }

            attacker.Tokens -= neededTokens;

            DiceResult = DiceResult.RollDice();
            ModifyDiceResult(attacker, province);
            if (CompetitionSuccess())
            {
                province.Presences.Clear();
                province.Presences.Add(new Presence { Capital = attacker.Capital, Original = 0, Locked = province.MarketValue, Strength = province.MarketValue });
                AddToHistory(attacker.Capital + " wins attack in " + GetDiceResultText(province, DiceResult), false);
                DiceResult.Information = attacker.Capital + " wins G" + DiceResult.Green + " B:" + DiceResult.Black + " W:" +DiceResult.White;
                return true;
            }
            DiceResult.Information = attacker.Capital + " looses G" + DiceResult.Green + " B:" + DiceResult.Black + " W:" + DiceResult.White;
            var presence = province.Presences.Find(m => m.Capital == attacker.Capital);
            if (presence != null)
            {
                province.Presences.Remove(presence);
            }
            AddToHistory(attacker.Capital + " fails attack in " + GetDiceResultText(province, DiceResult), false);
            return false;
        }

        private void ModifyDiceResult(Player attacker, Province province)
        {
            //Nationalism
            if (attacker.HasAdvance(Enums.AdvanceType.Nationalism) && (attacker.HomeArea == province.Area))
            {
                DiceResult.Black++;
                AddToHistory(province.Name + " att:" + attacker.Capital + " Nationalism +1D", false);
            }
            var defenderCapitalInHomeArea = province.GetDefenderInHomeArea(Players, attacker.Capital);
            if (defenderCapitalInHomeArea != Enums.Capital.NotSelected)
            {
                var defendingPlayer = Players.Find(m => m.Capital == defenderCapitalInHomeArea);
                if (defendingPlayer.HasAdvance(Enums.AdvanceType.Nationalism))
                {
                    AddToHistory(province.Name + " def:" + defendingPlayer.Capital + " Nationalism +1D", false);
                    DiceResult.White++;
                }
            }

            // Gunpowder neutralizes Long Bow and Stirrups denying them +1 and wins ties
            var gunpowderCard = CardPlays.Find(m => m.Card.Name == "Gunpowder");
            if (gunpowderCard != null)
            {
                if (gunpowderCard.Capital == attacker.Capital)
                {
                    DiceResult.Black++;
                    DiceResult.TieBreaker = 1;
                    AddToHistory(province.Name + " att:" + attacker.Capital + " Gunpowder +1D", false);
                }
                else if (province.Presences.Exists(m => m.Capital == gunpowderCard.Capital))
                {
                    DiceResult.White++;
                    DiceResult.TieBreaker = -1;
                    AddToHistory(province.Name + " def:" + province.Presences.Find(m => m.Capital == gunpowderCard.Capital) + " Gunpowder +1D", false);
                }
            }

            // Long Bow
            var longBowCard = CardPlays.Find(m => m.Card.Name == "Long Bow");
            if (longBowCard != null)
            {
                if (longBowCard.Capital == attacker.Capital )
                {
                    if (gunpowderCard != null && province.Presences.Exists(m => m.Capital == gunpowderCard.Capital))
                    {
                        AddToHistory(longBowCard.Capital + "'s Long Bow neutralized by gun powder", false);
                    }
                    else
                    {
                        DiceResult.Black++;
                        DiceResult.TieBreaker = 1;
                        AddToHistory(province.Name + " att:" + attacker.Capital + " Long Bow +1D", false);
                    }
                }
                else if (province.Presences.Exists(m => m.Capital == longBowCard.Capital))
                {
                    if (gunpowderCard != null && longBowCard.Capital == attacker.Capital)
                    {
                        AddToHistory(longBowCard.Capital + "'s Long Bow neutralized by gun powder", false);
                    }
                    else
                    {
                        DiceResult.White++;
                        DiceResult.TieBreaker = -1;
                        AddToHistory(
                            province.Name + " def:" + province.Presences.Find(m => m.Capital == longBowCard.Capital) +
                            " Long Bow +1D", false);
                    }
                }
            }

            // Stirrups
            var stirrupCard = CardPlays.Find(m => m.Card.Name == "Stirrups");
            if (stirrupCard != null)
            {
                if (stirrupCard.Capital == attacker.Capital)
                {
                    if (gunpowderCard != null && province.Presences.Exists(m => m.Capital == gunpowderCard.Capital))
                    {
                        AddToHistory(stirrupCard.Capital + "'s Stirrup neutralized by gun powder", false);
                    }
                    else if (longBowCard != null && province.Presences.Exists(m => m.Capital == longBowCard.Capital))
                    {
                        AddToHistory(stirrupCard.Capital + "'s Stirrup neutralized by long bow", false);
                    }
                    else
                    {
                        DiceResult.Black++;
                        DiceResult.TieBreaker = 1;
                        AddToHistory(province.Name + " att:" + attacker.Capital + " Stirrups +1D", false);                        
                    }
                }
                else if (province.Presences.Exists(m => m.Capital == stirrupCard.Capital))
                {
                    if (gunpowderCard != null && gunpowderCard.Capital == attacker.Capital)
                    {
                        AddToHistory(stirrupCard.Capital + "'s Stirrups neutralized by gun powder", false);
                    }
                    else if (longBowCard != null && longBowCard.Capital == attacker.Capital)
                    {
                        AddToHistory(stirrupCard.Capital + "'s Stirrups neutralized by long bow", false);
                    }
                    else
                    {
                        DiceResult.White++;
                        DiceResult.TieBreaker = -1;
                        AddToHistory(
                            province.Name + " def:" + province.Presences.Find(m => m.Capital == stirrupCard.Capital) +
                            " Stirrups +1D", false);
                    }
                }
            }
        }

        private static string GetDiceResultText(Province province, DiceResult diceResult)
        {
            return province.Name + " G:" + diceResult.Green + " B:" + diceResult.Black + " W:" + diceResult.White;
        }

        private bool CompetitionSuccess()
        {
            switch (DiceResult.TieBreaker)
            {
                case 1:
                    return (DiceResult.Black >= DiceResult.White || DiceResult.Green > PlayerInTurn.PlayPosition);
                case 0:
                case -1:
                default:
                    return (DiceResult.Black > DiceResult.White || DiceResult.Green > PlayerInTurn.PlayPosition);
            }
        }
        public void NeoJ()
        {
            
        }
        #endregion expansion

        #region Game end
        public void EndGame()
        {
            AddToHistory("Game end", true);
            CalculatePoints();
            GameOver = true;
        }

        private void CalculatePoints()
        {
            foreach (var player in Players)
            {
                player.Vp = player.AdvanceString.Length;
                AddToHistory(player.Capital + " Advances ==> " + player.Vp + " Vp", false);
                var incomeArea = (int) Math.Floor(player.Tokens / 10d);
                player.Vp += incomeArea;
                AddToHistory(player.Capital + " Area Income / 10 ==> " + incomeArea + " Vp", false);
                player.Vp += player.NumberOfprovinces(Map);
                AddToHistory(player.Capital + " Province DOMs ==> " + player.NumberOfprovinces(Map) + " Vp", false);
                player.Vp -= player.Misery;
                AddToHistory(player.Capital + " Misery deducted ==> " + player.Misery + " Vp", false);
                AddToHistory(player.Capital + " Total score ==> " + player.Vp + " Vp", false);
            }

            var position = 1;
            AddToHistory(" Final standing", true);
            foreach (var player in Players.OrderByDescending(m=>m.Vp))
            {
                AddToHistory("Position: " + position + " Vp:" + player.Vp, false);
                position ++;
            }
        }
        #endregion Game end

        #region Conversions


        public int GetGoodValue(Enums.GoodType good)
        {
            switch (good)
            {
                case Enums.GoodType.Stone:
                    return 2;
                case Enums.GoodType.Wool:
                    return 3;
                case Enums.GoodType.Timber:
                    return 4;
                case Enums.GoodType.Grain:
                    return 5;
                case Enums.GoodType.Cloth:
                    return 6;
                case Enums.GoodType.Wine:
                    return 7;
                case Enums.GoodType.Metal:
                    return 8;
                case Enums.GoodType.Fur:
                    return 9;
                case Enums.GoodType.Silk:
                    return 10;
                case Enums.GoodType.Spice:
                    return 11;
                default:
                    return 12;
            }
        }

        public Enums.Capital GetCapitalFromStringName(string capital)
        {
            switch (capital.ToLower())
            {
                case "barcelona":
                    return Enums.Capital.Barcelona;
                case "genoa":
                    return Enums.Capital.Genoa;
                case "hamburg":
                    return Enums.Capital.Hamburg;
                case "london":
                    return Enums.Capital.London;
                case "paris":
                    return Enums.Capital.Paris;
                case "venice":
                    return Enums.Capital.Venice;
                default:
                    return Enums.Capital.NotSelected;
            }
        }

        public Enums.OrderOfPlayChoice GetOrderOfPlayChoiceByString(string orderOfPlayChoice)
        {
            switch (orderOfPlayChoice)
            {
                default:
                case "Provinces":
                    return Enums.OrderOfPlayChoice.Provinces;
                case "Cards":
                    return Enums.OrderOfPlayChoice.Cards;
                case "Misery":
                    return Enums.OrderOfPlayChoice.Misery;}
        }
        #endregion Conversions

        #region Misery



        #endregion Misery

        #region general

        private void ChangePlayOrder(Player playerMoved)
        {
            foreach (var player in Players.Where(player => PlayOrder > playerMoved.PlayOrder))
            {
                player.PlayOrder--;
            }
            playerMoved.PlayOrder = Players.Count;
        }

        public int DivideByTwo(int number)
        {
            return (int)(Math.Ceiling((decimal) number/2));
        }
        #endregion general

    }
}