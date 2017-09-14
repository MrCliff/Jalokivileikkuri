using System.Collections.Generic;
using System.Json;
using System.Threading.Tasks;

namespace Jalokivileikkuri
{
    /// <summary>
    /// Handles all the cutting.
    /// </summary>
    class Cutter
    {
        //private object tLock = new object();


        /// <summary>
        /// The set of possible cuts for the gem of this cutter.
        /// </summary>
        private SortedSet<Cut> PossibleCuts { get; }

        /// <summary>
        /// Contains all the possible sets of cuts for each individual gem chunk size.
        /// </summary>
        private CutCombinationContainer CutCombinations { get; }

        /// <summary>
        /// Contains all the optimal cuts already calculated.
        /// </summary>
        private IDictionary<int, CutProduct> OptimalCuts { get; }


        /// <summary>
        /// Initializes a new Cutter for use.
        /// </summary>
        /// <param name="cuts">The cuts for this cutter.</param>
        public Cutter(IEnumerable<JsonValue> cuts)
        {
            PossibleCuts = new SortedSet<Cut>();
            foreach (IDictionary<string, JsonValue> cut in cuts)
            {
                PossibleCuts.Add(new Cut(cut));
            }
            CutCombinations = new CutCombinationContainer(PossibleCuts);
            OptimalCuts = new Dictionary<int, CutProduct>();
        }


        /// <summary>
        /// Cuts the gem chunk of the given size in the most optimal way.
        /// </summary>
        /// <param name="rawChunkSize">The size of the cuttable gem chunk.</param>
        /// <returns>The most optimal cut for the given chunk size.</returns>
        public CutProduct Cut(int rawChunkSize)
        {
            CutProduct result;
            // TODO: Add thread lock. The best is if it locks out only the threads that have the same rawChunkSize.
            //lock (tLock)
            //{
            if (OptimalCuts.TryGetValue(rawChunkSize, out result)) return result;
            result = CalculateBestCut(rawChunkSize);
            OptimalCuts.Add(rawChunkSize, result);
            //}

            return result;
        }


        /// <summary>
        /// Calculates the best cut from the cut sets in the PossibleCutSets.
        /// </summary>
        /// <param name="rawChunkSize">The size of the cuttable gem chunk.</param>
        /// <returns>The most optimal cut for the given chunk size.</returns>
        private CutProduct CalculateBestCut(int rawChunkSize)
        {
            var cutCombinations = CutCombinations.GetCutCombinations(rawChunkSize);

            CutProduct best = null;
            int bestProfit = int.MinValue;
            foreach (var cutCombination in cutCombinations)
            {
                CutProduct current = new CutProduct(rawChunkSize, cutCombination);
                int currentProfit = current.Profit;
                if (currentProfit > bestProfit)
                {
                    best = current;
                    bestProfit = currentProfit;
                }
            }

            return best;
        }
    }
}
