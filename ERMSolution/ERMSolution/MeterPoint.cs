using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERMSolution
{
    public class MeterPoint
    {
        public string Code { get; set; }
        public string SerialNumber { get; set; }
        public string PlantCode { get; set; }
        public string SampleDateTime { get; set; }
        public string DataType { get; set; }
        public string Units { get; set; }
        public string Status { get; set; }
    }
}
