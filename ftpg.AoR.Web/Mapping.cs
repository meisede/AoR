using System.Collections.Generic;
using System.Linq;
using AoR.Entity;
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

            };
            gameModel.InitAdvancesPurchased();
            gameModel.MapModel = MapToMapModel(game.Map);

            foreach (var playerModel in game.Players.Select(player => new PlayerModel
            {
                Nick = player.Nick,
                PlayOrder = player.PlayOrder,
                Misery = player.Misery,
                Capital = player.Capital,
                CapitalOrder = player.CapitalOrder, 
                Cards = player.Cards,
                Stock = player.Stock,
                AdvanceString = player.AdvanceString,
                PreferredCapitals = player.PreferredCapitals,
                ShipType = player.ShipType,
                DidAction = player.DidAction,
                Tokens = player.Tokens,
            }))
            {
                gameModel.Players.Add(playerModel);
            }
            if (user != null)
            {
                gameModel.PlayerDisplay = gameModel.Players.Find(x => x.Nick == user.Nick);
            }
            return gameModel;
        }

        private static MapModel MapToMapModel(Map map)
        {
            var mapModel = new MapModel();
            mapModel.Provinces = new List<ProvinceModel>();
      
            foreach (var province in map.Provinces)
            {
                var provinceModel = new ProvinceModel();
                provinceModel.Name = province.Name;
                provinceModel.Good = province.Good.ToString();
                provinceModel.MarketValue = province.MarketValue;
                provinceModel.Loc = new LocModel();
                provinceModel.Loc.Polygon = new List<int>();
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
                mapModel.Provinces.Add(provinceModel);
            }
            return mapModel;
        }

    }
}