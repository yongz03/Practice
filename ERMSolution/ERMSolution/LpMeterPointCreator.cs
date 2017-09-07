using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ERMSolution
{
    public class LpMeterPointCreator : IMeterPointCreator
    {
        public List<MeterPoint> GetMeterPoints(string csvContent)
        {
            var csv = new CsvHelper(csvContent);
            var meterPoints = new List<MeterPoint>();

            foreach (var line in csv)
            {
                var csvValues = line.Split(',');
                var meterPoint = new LpMeterPoint
                {
                    Code = csvValues[0],
                    SerialNumber = csvValues[1],
                    PlantCode = csvValues[2],
                    SampleDateTime = csvValues[3],
                    DataType = csvValues[4],
                    DataValue = decimal.Parse(csvValues[5]),
                    Units = csvValues[6],
                    Status = csvValues[7]
                };

                meterPoints.Add(meterPoint);
            }

            return meterPoints;
        }

        public decimal GetMeterPointsMean(List<MeterPoint> lpMeterPoints)
        {
            return lpMeterPoints.Average(l => ((LpMeterPoint)l).DataValue);    
        }

        public IEnumerable<MeterPoint> GetAboveMeanList(List<MeterPoint> lpMeterPoints, decimal mean, decimal above)
        {
            return lpMeterPoints.Where(l => ((LpMeterPoint)l).DataValue > mean * (1M + above));    
        }

        public IEnumerable<MeterPoint> GetBelowMeanList(List<MeterPoint> lpMeterPoints, decimal mean, decimal below)
        {
            return lpMeterPoints.Where(l => ((LpMeterPoint)l).DataValue < mean * (1M - below));  
        }

        public decimal GetMeanColumnValue(MeterPoint meterPoint)
        {
            return ((LpMeterPoint) meterPoint).DataValue;
        }
    }
}
