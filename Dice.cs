namespace otel
{
    public class Dice
    {
        private int min;
        private int max;

        public Dice(int min, int max)
        {
            this.min = min;
            this.max = max;
        }

        public List<int> rollTheDice(int rolls)
        {
            List<int> results = new List<int>();

            for (int i = 0; i < rolls; i++)
            {
                results.Add(rollOnce());
            }
            
            return results;
        }

        private int rollOnce()
        {
            return Random.Shared.Next(min, max + 1);
        }
    }
}