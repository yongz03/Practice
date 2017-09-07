using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERMSolution;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ERMSolutionTest
{
    [TestClass]
    public class TouMeterPointCreatorTest
    {
        [TestMethod]
        public void GetMeaterPointsMean()
        {
            var list = new List<MeterPoint>
            {
                new TouMeterPoint
                {
                    Energy = 1
                },
                new TouMeterPoint
                {
                    Energy = 2
                },
                new TouMeterPoint
                {
                    Energy = 3
                },
                new TouMeterPoint
                {
                    Energy = 4
                }
            };

            var creator = new TouMeterPointCreator();
            var mean = creator.GetMeterPointsMean(list);
            Assert.AreEqual((1M + 2M + 3M + 4M) / 4M, mean);
        }

        [TestMethod]
        public void GetAboveMeanList()
        {
            var list = new List<MeterPoint>
            {
                new TouMeterPoint
                {
                    Energy = 1
                },
                new TouMeterPoint
                {
                    Energy = 2
                },
                new TouMeterPoint
                {
                    Energy = 3
                },
                new TouMeterPoint
                {
                    Energy = 4
                }
            };

            var creator = new TouMeterPointCreator();
            var mean = creator.GetMeterPointsMean(list);

            var aboveMeanList = creator.GetAboveMeanList(list, mean, 0.2M);
            Assert.AreEqual(1, aboveMeanList.Count());
        }

        [TestMethod]
        public void GetBelowMeanList()
        {
            var list = new List<MeterPoint>
            {
                new TouMeterPoint
                {
                    Energy = 1
                },
                new TouMeterPoint
                {
                    Energy = 2
                },
                new TouMeterPoint
                {
                    Energy = 3
                },
                new TouMeterPoint
                {
                    Energy = 4
                }
            };

            var creator = new TouMeterPointCreator();
            var mean = creator.GetMeterPointsMean(list);

            var belowMeanList = creator.GetBelowMeanList(list, mean, 0.2M);
            Assert.AreEqual(1, belowMeanList.Count());
        }

        [TestMethod]
        public void GetMeterPoints()
        {
            const string constents = "MeterPoint Code,Serial Number,Plant Code,Date/Time,Data Type,Energy,Maximum Demand,Time of Max Demand,Units,Status,Period,DLS Active,Billing Reset Count,Billing Reset Date/Time,Rate\r\n212621145,212621145,ED011300245,11/09/2015 00:41:02,Export Wh Total,409646.700000,1275.368000,30/12/1899 00:00:00,kwh,.....R....,Total,False,26,01/09/2015 00:00:00,Unified\r\n";

            var creator = new TouMeterPointCreator();
            var list = creator.GetMeterPoints(constents);
            Assert.AreEqual(1, list.Count);
            Assert.IsInstanceOfType(list.First(), typeof(TouMeterPoint));
        }

        [TestMethod]
        public void GetMeanColumnValue()
        {
            var creator = new TouMeterPointCreator();
            var value = creator.GetMeanColumnValue(new TouMeterPoint { Energy = 1 });

            Assert.AreEqual(1, value);
        }
    }
}
