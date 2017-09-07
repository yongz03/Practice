using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERMSolution
{
    public interface IMeterPointCreator
    {
        List<MeterPoint> GetMeterPoints(string csvContent);
        decimal GetMeterPointsMean(List<MeterPoint> meatPoints);
        IEnumerable<MeterPoint> GetAboveMeanList(List<MeterPoint> meatPoints, decimal mean, decimal above);
        IEnumerable<MeterPoint> GetBelowMeanList(List<MeterPoint> meatPoints, decimal mean, decimal below);
        decimal GetMeanColumnValue(MeterPoint meterPoint);
    }
}
