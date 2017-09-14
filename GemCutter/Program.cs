using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Json;
using System.Diagnostics;

namespace Jalokivileikkuri
{
    /// <summary>
    /// The main program class.
    /// </summary>
    class Program
    {
        /// <summary>
        /// The main subroutine, where the execution starts from.
        /// </summary>
        /// <param name="args">Not in use currently.</param>
        static void Main(string[] args)
        {
            Stopwatch stopWatch = Stopwatch.StartNew();

            string webAddress = "https://raw.githubusercontent.com/wunderdogsw/wunderpahkina-vol7/master/input.json";

            WebClient wc = new WebClient();
            byte[] rawData = wc.DownloadData(webAddress);
            string jsonString = Encoding.UTF8.GetString(rawData);

            var dataRetrievalTime = stopWatch.ElapsedMilliseconds;
            //Dictionary<string, Dictionary<string, object[]>> result;
            //result = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, object[]>>>(jsonString);
            //JsonObject result = (JsonObject)JsonValue.Parse(jsonString);

            //foreach (var val in result["diamond"]["rawChunks"] as JsonArray)
            //{
            //    Console.Write(", " + val.ToString());
            //}
            //Console.WriteLine();

            CutController cutCont = new CutController();
            cutCont.ReadData(jsonString);

            var initTime = stopWatch.ElapsedMilliseconds;

            int result = cutCont.CalculateProfit();
            stopWatch.Stop();

            var totalTime = stopWatch.ElapsedMilliseconds;

            Console.WriteLine("Total profit: " + result);
            Console.WriteLine("Data retrieval time: " + (double)dataRetrievalTime / 1000 + "s");
            Console.WriteLine("Initialization time: " + (double)initTime / 1000 + "s");
            Console.WriteLine("Data handling time: " + (double)(totalTime - dataRetrievalTime) / 1000 + "s");
            Console.WriteLine("Total execution time: " + (double)totalTime / 1000 + "s");
        }
    }
}
