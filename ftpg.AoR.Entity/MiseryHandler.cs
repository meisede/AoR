using System;
using System.Collections.Generic;
using System.Linq;

namespace ftpg.AoR.Entity
{
    public class MiseryHandler
    {
        public static List<int> MiseryLadder => new List<int> { 0, 10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 125, 150, 175, 200, 250, 300, 350, 400, 450, 500, 600, 700, 800, 900, 1000, 1001,1002,1003 };

        public int NumberOfTiers(Player player)
        {
            if ("A,E,I,N,R,V".Split(',').Any(letter => player.AdvanceString.IndexOf(letter) < 0)) { return 0; }
            if ("B,F,K,O,S,W".Split(',').Any(letter => player.AdvanceString.IndexOf(letter) < 0)) { return 1; }
            if ("C,G,K,P,T,X,".Any(letter => player.AdvanceString.IndexOf(letter) < 0)) { return 2; }
            return "D,H,L,Q,U,Y".Split('.').Any(letter => player.AdvanceString.IndexOf(letter) < 0) ? 3 : 4;
        }

        public static int MiseryChangeByStep(int misery, int step)
        {
            var position = MiseryLadder.FindIndex(m=>m==misery) + step;
            position = (position < 0) ? 0 : position;
            return MiseryLadder[position];
        }

        public int MiseryReliefPotentialCost(Player player)
        {
            var numberOfTiers = NumberOfTiers(player);
            var position = -1;
            foreach (var index in MiseryLadder.Where(index => index == player.Misery))
            {
                position = index;
            }
            var postionPotential = (position - numberOfTiers) > -1 ? (position - numberOfTiers) : 0;
            return MiseryLadder[position] - MiseryLadder[position - postionPotential];
        }

        private static int GetMiseryLadderStep(int misery)
        {
            return MiseryLadder.FirstOrDefault(step => misery == MiseryLadder[step]);
        }

        public int NewMisery(Player player, int miseryReliefPaid)
        {
            var numberOfTiers = NumberOfTiers(player);
            if (numberOfTiers == 0)
            {
                return player.Misery;
            }
            var miseryStep = GetMiseryLadderStep(player.Misery);
            for (var index = miseryStep - numberOfTiers; index < miseryStep; index ++)
            {
                if (miseryReliefPaid >= player.Misery - MiseryLadder[index])
                {
                    return MiseryLadder[index];
                }
            }
            return player.Misery;
        }

        public static int GetMiseryReliefCost(int misery)
        {
            if (misery < 125)
            {
                return 10;
            }
            if (misery < 250)
            {
                return 25;
            }
            return misery < 600 ? 50 : 100;
        }

        public static int GetMiseryIncrease(int misery)
        {
            if (misery > 450)
            {
                return 100;
            }
            if (misery > 175)
            {
                return 50;
            }
            return misery > 90 ? 25 : 10;
        }

        public static int GetStabilizationMiseryPenalty(int startingMisery, int stabilizationCost)
        {
            var misery = startingMisery;
            while (misery < startingMisery + stabilizationCost)
            {
                misery += GetMiseryIncrease(misery);
            }
            return misery - startingMisery;
        }
    }
}
