using System;
using System.Collections.Generic;
using System.Json;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Jalokivileikkuri
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
        private IList<JsonValue> RawChunkSizes { get; }

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
            RawChunkSizes = chunks as IList<JsonValue>;

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
            //object tLock = new object();
            //List<Thread> threads = new List<Thread>();

            int sum = 0;
            foreach (int chunkSize in RawChunkSizes)
            {
                sum += Cutter.Cut(chunkSize).Profit;
            }

            //int jump = 200;
            //int max = RawChunkSizes.Count;
            //for (int i = 0; i <= max + jump; i += jump)
            //{
            //    Thread worker = new Thread(delegate ()
            //    {
            //        int start = i;
            //        int profit = 0;
            //        for (int j = i; j < start + jump && j < max; j++)
            //        {
            //            int chunkSize = RawChunkSizes[j];

            //            profit += Cutter.Cut(chunkSize).Profit;
            //        }

            //        lock (tLock)
            //        {
            //            sum += profit;
            //        }
            //    });

            //    worker.Start();

            //    threads.Add(worker);
            //}
            //threads.ForEach(worker => worker.Join());

            return sum;
        }
    }
}
