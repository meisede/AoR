using ftpg.AoR.Entity;

namespace FTPG.AoR.Test
{
    public class Util
    {
        public static Game GetGame(int noPlayers)
        {
            var game = new Game("test", noPlayers);
            game.Players[0].Capital = Enums.Capital.Venice;
            game.Players[1].Capital = Enums.Capital.Genoa;
            game.Players[2].Capital = Enums.Capital.Barcelona;
            return game;
        }

        public static void AddDomsAndTokens(Game game)
        {
            //game.Map.AddDomOrSatelite(Enums.Capital.Barcelona, "Barcelona");
            game.Map.AddDomOrSatelite(Enums.Capital.Barcelona, "Leon");
            //game.Map.AddDomOrSatelite(Enums.Capital.Genoa, "Genoa");
            game.Map.AddDomOrSatelite(Enums.Capital.Genoa, "Marseilles");
            game.Map.AddDomOrSatelite(Enums.Capital.Genoa, "Valencia");
            game.Map.AddDomOrSatelite(Enums.Capital.Genoa, "Toledo");
        }
    }
}
