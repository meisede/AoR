namespace ftpg.AoR.Entity
{
    public class Presence
    {
        public Enums.Capital Capital { get; set; }
        public int Strength { get; set; }
        public int Original { get; set; }
        public int Locked { get; set; }


        public void AddStrength(int marketValue, int strength)
        {
            Strength += strength;
            if (strength > marketValue)
            {
                Strength = marketValue;
            }
        }

        public void AddStrength(int strength)
        {
            Strength += strength;
        }
    }
}