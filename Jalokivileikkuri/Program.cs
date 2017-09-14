using System;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Net;

namespace GemCutter
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

            string filePath = "input.json";
            string jsonString;

            try
            {
                Console.WriteLine("Yritetään avata tiedostoa \"{0}\"", filePath);
                jsonString = File.ReadAllText(filePath, Encoding.UTF8);
            }
            catch (IOException e)
            {
                Console.WriteLine("Tiedostoa ei pystytty lukemaan: {0}", e.Message);

                try
                {
                    Console.WriteLine("Yritetään lukea tiedostoa osoitteesta: \"{0}\"", webAddress);
                    WebClient wc = new WebClient();
                    byte[] rawData = wc.DownloadData(webAddress);
                    jsonString = Encoding.UTF8.GetString(rawData);
                }
                catch (WebException we)
                {
                    Console.WriteLine("Tiedostoa ei pystytty lukemaan: {0}\n", we.Message);
                    return;
                }
            }

            Console.WriteLine("Tiedoston luku onnistui!");

            var dataRetrievalTime = stopWatch.ElapsedMilliseconds;

            GemCutController cutCont = new GemCutController();
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

            Console.ReadKey();
        }
    }
}
