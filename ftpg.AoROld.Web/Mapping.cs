using System;
using System.Collections.Generic;
using System.Linq;
using ftpg.AoR.Entity;
using ftpg.AoR.Web.Models;

namespace ftpg.AoR.Web
{
    public class Mapping
    {
        public static Game GameModelToGameEntity(GameModel gameModel)
        {
            var game = new Game
            {
                Name = gameModel.Name,
                Players = new List<Player>(),
                PlayOrder = gameModel.PlayOrder,
                Epoch = gameModel.Epoch,
            };

            foreach (var player in gameModel.Players.Select(playerModel => new Player()))
            {
                game.Players.Add(player);
            }
            return game;
        }

        public static GameModel GameToGameModel(Game game, User user)
        {
            var gameModel = new GameModel
            {
                Game = game,
                Name = game.Name,
                Phase = game.Phase,
                Turn = game.Turn,
                PlayOrder = game.PlayOrder,
                Players = new List<PlayerModel>(),
                ShortageSurplus = game.ShortageSurplus,
                CardsLastingEffect = game.CardsLastingEffect,
                DiceResult = game.DiceResult,
                BuyCardExpansion = game.BuyCardExpansion,
                Unplayable = game.Unplayable,
                WarResult = game.WarResult
            };
            // Special handling. One time notification to player.
            game.BuyCardExpansion = string.Empty;
            gameModel.InitAdvancesPurchased();
            gameModel.MapModel = MapToMapModel(game);

            foreach (var playerModel in game.Players.Select(player => new PlayerModel
            {
                Nick = player.Nick,
                Cash = player.Cash,
                WrittenCash = player.WrittenCash,
                PlayOrder = player.PlayOrder,
                PlayPosition = player.PlayPosition,
                ModifiedPlayOrder = game.GetModifiedPlayOrder(player.PlayOrder),
                Misery = player.Misery,
                Capital = player.Capital,
                CapitalName = player.Capital.ToString(),
                CapitalOrder = player.CapitalOrder, 
                Bid = player.Bid,
                Cards = player.Cards,
                Stock = player.Stock,
                AdvanceString = player.AdvanceString,
                ShipType = player.ShipType,
                DidAction = player.DidAction,
                Tokens = player.Tokens,
                BoughtCardExpansion = player.BoughtCardExpansion

            }))
            {

                gameModel.Players.Add(playerModel);
            }
            if (user != null)
            {
                gameModel.PlayerDisplay = gameModel.Players.Find(x => x.Nick == user.Nick);
            }
            gameModel.Purchase = BuildPurchaseModel(game, user);
            return gameModel;
        }

        private static PurchaseModel BuildPurchaseModel(Game game, User user)
        {
            var purchase = new PurchaseModel();
            var player = game.Players.Find(m => m.Nick == user.Nick);
            purchase.OriginalCash = player.Cash;
            purchase.Misery = player.Misery;
            purchase.MiseryRelief = 0;
            purchase.MiseryReliefSpent = 0;
            purchase.OriginalMisery = player.Misery;
            purchase.ShipUpgrade = false;
            purchase.Stabilization = true;
            purchase.StabilzationCost = player.Stabilization;
            purchase.Cash = purchase.OriginalCash - purchase.StabilzationCost;
            purchase.StabilzationMiseryPenalty = MiseryHandler.GetStabilizationMiseryPenalty(player.Misery,purchase.StabilzationCost);
            purchase.OriginalTierLevel = AdvanceHandler.TierLevel(player);
            purchase.TierLevel = purchase.OriginalTierLevel;
            purchase.Advances = PlayerAdvancesToAdvancesModel(game, player);
            purchase.CardPlays = MapToCardPlaysModel(game, player);
            return purchase;
        }

        public static Purchase PurchaseModelToPurchase(PurchaseModel purchaseModel)
        {
            var purchase = new Purchase
            {
                ShipUpgrade = purchaseModel.ShipUpgrade,
                LeadersUsed = new List<string>(),
                Stabilization = purchaseModel.Stabilization,
                TierLevel = purchaseModel.TierLevel,
                AdvancesPurchased = string.Empty
            };
            foreach (var advance in purchaseModel.Advances)
            {
                if (advance.Purchased)
                {
                    purchase.AdvancesPurchased += advance.Letter;
                }
            }
            if (purchaseModel.CardPlays == null)
            {
                purchaseModel.CardPlays = new List<CardPlayModel>();
            }
            foreach (var leader in purchaseModel.CardPlays)
            {
                if (leader.Used)
                {
                    purchase.LeadersUsed.Add(leader.Name);
                }
            }
            return purchase;
        }

        private static List<AdvanceModel> PlayerAdvancesToAdvancesModel(Game game, Player player)
        {
            var advancesList = new List<AdvanceModel>();
            var researchDiscount = (player.HasAdvance(Enums.AdvanceType.InstitutionalResearch)) ? 10 : 0;
            foreach (var advance in game.Advances)
            {
                var advanceModel = new AdvanceModel
                {
                    Letter = advance.Letter,
                    Name = advance.Name,
                    Description = advance.Description,
                    FullCost = advance.Cost,
                    Credits = advance.Credits,
                    Group = advance.Group.ToString(),
                    MiseryRelief = advance.MiseryRelief,
                    MiseryChange = advance.MiseryChange,
                    Purchased = false,
                    PreOwned = player.HasAdvance(advance.AdvanceType),
                    Requisite = advance.Requisite,
                    Cost =
                        AdvanceHandler.GetPriceForAdvance(player, advance.Letter, game.CardsLastingEffect,
                            game.CardPlays, player.HasAdvance(Enums.AdvanceType.InstitutionalResearch),
                            player.HasAdvance(Enums.AdvanceType.WrittenRecord))
                };
                advanceModel.OriginalCost = advanceModel.Cost;
                advanceModel.Restricted = AdvanceHandler.IsAdvanceRestrcited(advance ,player.AdvanceString);
                var list = game.Players.FindAll(m => m.Capital != player.Capital).Select(nation => nation.HasAdvance(advance.AdvanceType)).ToList();
                advanceModel.OtherNations = list;
                advancesList.Add(advanceModel);
            }
            return advancesList;
        }

        private static MapModel MapToMapModel(Game game)
        {
            var mapModel = new MapModel {Provinces = new List<ProvinceModel>()};

            foreach (var province in game.Map.Provinces)
            {
                var provinceModel = new ProvinceModel
                {
                    Name = province.NameAsText,
                    Good = province.Good.ToString(),
                    MarketValue = province.MarketValue,
                    Loc = new LocModel {Polygon = new List<int>()}
                };
                if (province.Loc == null)
                {
                    province.Loc.Polygon = new List<int>();
                }
                foreach (var point in province.Loc.Polygon)
                {
                    provinceModel.Loc.Polygon.Add(point);
                }

                provinceModel.Presences = new List<PresenceModel>();
                if (provinceModel.Presences == null) continue;
                foreach (var presence in province.Presences)
                {
                    provinceModel.Presences.Add(new PresenceModel
                    {
                        Capital = presence.Capital.ToString(),
                        Original = presence.Original,
                        Strength = presence.Strength});
                }
                if (game.Phase == Enums.GamePhase.Expansion)
                {
                    provinceModel.AttackTokensNeeded = game.GetNeededTokenNumberForCompetition(game.PlayerInTurn, province);
                }
                mapModel.Provinces.Add(provinceModel);
            }
            return mapModel;
        }

        private static List<CardPlayModel> MapToCardPlaysModel(Game game, Player player)
        {
            return game.CardPlays.Select(cardPlay => new CardPlayModel
            {
                Name = cardPlay.Card.Name,
                AdvanceLetters = cardPlay.Card.AdvanceDiscountLetters.Select(c => c.ToString()).ToList(),
                Capital = cardPlay.Capital,
                Fee = cardPlay.Fee,
                Protected = cardPlay.FirstLeaderPlay,
                Discount = cardPlay.Card.Discount(cardPlay.Card.Name, 
                game.CardsLastingEffect), Text = cardPlay.Card.Description,
                Used = (cardPlay.Capital == player.Capital)
            }).ToList();
        }
    }
}