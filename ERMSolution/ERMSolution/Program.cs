using System;
using System.Configuration;
using System.IO;
using System.Linq;

namespace ERMSolution
{
    class Program
    {
        /// <summary>
        /// while adding new processor, a new creator based on base class Creator and a new class based on
        /// MeterPoint need to be created, then creating a new concrete creastor below inside if statement
        /// if file size is huge(1GB+), the perfemance of reading it may suffer, even an exception may be thrown
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            try
            {
                //read all csv file names in configured location
                var fileLocation = ConfigurationManager.AppSettings["CsvFileLocation"];
                var csvFileNames = Directory.GetFiles(fileLocation, "*.*", SearchOption.TopDirectoryOnly)
                    .Where(s => Path.GetExtension(s) == ".csv");

                foreach (var fileName in csvFileNames)
                {
                    var contents = File.ReadAllText(fileName);

                    var creator = GenerateCreator(fileName);

                    if (creator != null)
                    {
                        //calculate values and get lists for display
                        var meterPoints = creator.GetMeterPoints(contents);
                        var mean = creator.GetMeterPointsMean(meterPoints);
                        var aboveMeans = creator.GetAboveMeanList(meterPoints, mean, 0.2M);
                        var belowMeans = creator.GetBelowMeanList(meterPoints, mean, 0.2M);

                        //display values
                        foreach (var aboveMean in aboveMeans)
                        {
                            Console.WriteLine(string.Format("{0} {1} {2} {3}", "{" + fileName + "}",
                                "{" + aboveMean.SampleDateTime + "}",
                                "{" + creator.GetMeanColumnValue(aboveMean) + "}", "{" + mean + "}"));
                        }

                        foreach (var belowMean in belowMeans)
                        {
                            Console.WriteLine(string.Format("{0} {1} {2} {3}", "{" + fileName + "}",
                                "{" + belowMean.SampleDateTime + "}",
                                "{" + creator.GetMeanColumnValue(belowMean) + "}", "{" + mean + "}"));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("The error detail is " + ex.Message);
            }

            Console.ReadLine();
        }

        private static IMeterPointCreator GenerateCreator(string fileName)
        {
            //generate creator based on file name
            IMeterPointCreator creator = null;
            if (fileName.Contains("LP"))
            {
                creator = new LpMeterPointCreator();
            }
            else if (fileName.Contains("TOU"))
            {
                creator = new TouMeterPointCreator();
            }
            return creator;
        }
    }
}
