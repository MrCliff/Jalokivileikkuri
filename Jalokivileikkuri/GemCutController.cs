using System.Collections.Generic;
using System.Json;

namespace GemCutter
{
    /// <summary>
    /// Calculates the gem cut profit from json data.
    /// </summary>
    class GemCutController
    {
        /// <summary>
        /// A list of all the gems.
        /// </summary>
        private IList<Gem> Gems { get; set; }


        /// <summary>
        /// Initializes a new CutController for use.
        /// </summary>
        public GemCutController()
        {
            Gems = new List<Gem>();
        }


        /// <summary>
        /// Parses the given json data and gets ready to work.
        /// </summary>
        /// <param name="json">The json data as a string. The data should contain
        /// JsonObject with gem names (strings) as keys and JsonObjects as values.</param>
        public void ReadData(string json)
        {
            JsonObject data = JsonValue.Parse(json) as JsonObject;

            foreach (var item in data)
            {
                Gems.Add(new Gem(item));
            }
        }


        /// <summary>
        /// Calculates the profit from cutting the current gems.
        /// </summary>
        /// <returns>The total maximum profit of the gems.</returns>
        public int CalculateProfit()
        {
            int sum = 0;
            foreach (Gem gem in Gems)
            {
                sum += gem.CalculateProfit();
                //Console.WriteLine(sum);
            }
            return sum;
        }
    }
}
