using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERMSolution
{
    public class TouMeterPointCreator : IMeterPointCreator
    {
        public List<MeterPoint> GetMeterPoints(string csvContent)
        {
            var csv = new CsvHelper(csvContent);
            var meterPoints = new List<MeterPoint>();

            foreach (var line in csv)
            {
                var csvValues = line.Split(',');
                var meterPoint = new TouMeterPoint
                {
                    Code = csvValues[0],
                    SerialNumber = csvValues[1],
                    PlantCode = csvValues[2],
                    SampleDateTime = csvValues[3],
                    DataType = csvValues[4],
                    Energy = decimal.Parse(csvValues[5]),
                    MaxDemand = decimal.Parse(csvValues[6]),
                    MaxDemandDateTime = DateTime.Parse(csvValues[7]),
                    Units = csvValues[8],
                    Status = csvValues[9],
                    Period = csvValues[10],
                    DslActive = bool.Parse(csvValues[11]),
                    NumBillingReset = int.Parse(csvValues[12]),
                    Rate = csvValues[13]
                };

                meterPoints.Add(meterPoint);
            }

            return meterPoints;

        }

        public decimal GetMeterPointsMean(List<MeterPoint> lpMeterPoints)
        {
            return lpMeterPoints.Average(l => ((TouMeterPoint)l).Energy);
        }

        public IEnumerable<MeterPoint> GetAboveMeanList(List<MeterPoint> lpMeterPoints, decimal mean, decimal above)
        {
            return lpMeterPoints.Where(l => ((TouMeterPoint)l).Energy > mean * (1M + above));
        }

        public IEnumerable<MeterPoint> GetBelowMeanList(List<MeterPoint> lpMeterPoints, decimal mean, decimal below)
        {
            return lpMeterPoints.Where(l => ((TouMeterPoint)l).Energy < mean * (1M - below));
        }
        public decimal GetMeanColumnValue(MeterPoint meterPoint)
        {
            return ((TouMeterPoint)meterPoint).Energy;
        }
    }
}
