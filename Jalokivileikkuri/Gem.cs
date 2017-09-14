using System;
using System.Collections.Generic;
using System.Json;

namespace GemCutter
{
    /// <summary>
    /// A class for cutting different sizes of chunks of a gem in the most optimal way.
    /// </summary>
    class Gem
    {
        /// <summary>
        /// The name of this gem type.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// An array of all the chunk sizes to be cut.
        /// </summary>
        private IEnumerable<JsonValue> RawChunkSizes { get; }

        /// <summary>
        /// The cutter for cutting different sizes of gem chunks.
        /// </summary>
        private Cutter Cutter { get; }


        /// <summary>
        /// Initializes a new Gem object for use.
        /// </summary>
        /// <param name="data">The data of this object as a KeyValuePair.
        /// The data should contain at least JsonObject "cuts" and JsonArray "rawChunks".</param>
        public Gem(KeyValuePair<string, JsonValue> data)
        {
            string errorMessage = "The key {0} was not found in the correct spot from the JSON-data";
            string chunkKey = "rawChunks";
            string cutKey = "cuts";

            Name = data.Key;
            JsonObject gemData = data.Value as JsonObject;

            JsonValue chunks;
            if (!gemData.TryGetValue(chunkKey, out chunks)) throw new KeyNotFoundException(String.Format(errorMessage, chunkKey));
            RawChunkSizes = chunks as IEnumerable<JsonValue>;

            JsonValue cuts;
            if (!gemData.TryGetValue(cutKey, out cuts)) throw new KeyNotFoundException(String.Format(errorMessage, cutKey));
            Cutter = new Cutter(cuts as IEnumerable<JsonValue>);
        }


        /// <summary>
        /// Calculates the maximum profit from cutting all the gem chunks.
        /// </summary>
        /// <returns>The maximum profit possible to get from the gem chunks.</returns>
        public int CalculateProfit()
        {
            int sum = 0;
            foreach (int chunkSize in RawChunkSizes)
            {
                sum += Cutter.Cut(chunkSize).Profit;
            }

            return sum;
        }
    }
}
