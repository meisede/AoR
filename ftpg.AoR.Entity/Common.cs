using System;

namespace ftpg.AoR.Entity
{
    public class Common
    {
        // returns a number from 0 to bagsize - 1
        public static int GetRandomNumber(int bagsize)
        {
            var rnd = new Random(Guid.NewGuid().GetHashCode());
            return rnd.Next(0, bagsize);
        }

        public static int GetHalfOfRoundedDown(int number)
        {
            return Convert.ToInt32(Math.Floor((number / 2.0d)));
        }
    }
}
