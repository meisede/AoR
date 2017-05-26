namespace ftpg.AoR.Entity
{
    public class DiceResult
    {
        public int Black { get; set; }
        public int White { get; set; }
        public int Green { get; set; }
        public int TieBreaker { get; set; }
        public string Information { get; set; }


        public static DiceResult RollDice()
        {
            return new DiceResult
            {
                Black = Common.GetRandomNumber(6) + 1,
                White = Common.GetRandomNumber(6) + 1,
                Green = Common.GetRandomNumber(6) + 1,
                TieBreaker = 0
            };
        }
    }
}
