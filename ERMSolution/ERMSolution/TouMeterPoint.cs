using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERMSolution
{
    public class TouMeterPoint : MeterPoint
    {
        public decimal Energy { get; set; }
        public decimal MaxDemand { get; set; }
        public DateTime MaxDemandDateTime { get; set; }
        public string Period { get; set; }
        public bool DslActive { get; set; }
        public int NumBillingReset { get; set; }
        public DateTime BillingResetDateTime { get; set; }
        public string Rate { get; set; }

    }
}
