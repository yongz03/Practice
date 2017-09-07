using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ERMSolution;

namespace ERMSolutionTest
{
    [TestClass]
    public class LpMeterPointCreatorTest
    {
        [TestMethod]
        public void GetMeaterPointsMean()
        {
            var list = new List<MeterPoint>
            {
                new LpMeterPoint
                {
                    DataValue = 1
                },
                new LpMeterPoint
                {
                    DataValue = 2
                },
                new LpMeterPoint
                {
                    DataValue = 3
                },
                new LpMeterPoint
                {
                    DataValue = 4
                }
            };

            var creator = new LpMeterPointCreator();
            var mean = creator.GetMeterPointsMean(list);
            Assert.AreEqual((1M + 2M + 3M + 4M)/4M, mean);
        }

        [TestMethod]
        public void GetAboveMeanList()
        {
            var list = new List<MeterPoint>
            {
                new LpMeterPoint
                {
                    DataValue = 1
                },
                new LpMeterPoint
                {
                    DataValue = 2
                },
                new LpMeterPoint
                {
                    DataValue = 3
                },
                new LpMeterPoint
                {
                    DataValue = 4
                }
            };

            var creator = new LpMeterPointCreator();
            var mean = creator.GetMeterPointsMean(list);

            var aboveMeanList = creator.GetAboveMeanList(list, mean, 0.2M);
            Assert.AreEqual(1, aboveMeanList.Count());
        }

        [TestMethod]
        public void GetBelowMeanList()
        {
            var list = new List<MeterPoint>
            {
                new LpMeterPoint
                {
                    DataValue = 1
                },
                new LpMeterPoint
                {
                    DataValue = 2
                },
                new LpMeterPoint
                {
                    DataValue = 3
                },
                new LpMeterPoint
                {
                    DataValue = 4
                }
            };

            var creator = new LpMeterPointCreator();
            var mean = creator.GetMeterPointsMean(list);

            var belowMeanList = creator.GetBelowMeanList(list, mean, 0.2M);
            Assert.AreEqual(1, belowMeanList.Count());
        }

        [TestMethod]
        public void GetMeterPoints()
        {
            const string constents = "MeterPoint Code,Serial Number,Plant Code,Date/Time,Data Type,Data Value,Units,Status\r\n210095893,210095893,ED031000001,31/08/2015 00:45:00,Import Wh Total,0.000000,kwh,\r\n";

            var creator = new LpMeterPointCreator();
            var list = creator.GetMeterPoints(constents);
            Assert.AreEqual(1, list.Count);
            Assert.IsInstanceOfType(list.First(), typeof (LpMeterPoint));
        }

        [TestMethod]
        public void GetMeanColumnValue()
        {
            var creator = new LpMeterPointCreator();
            var value = creator.GetMeanColumnValue(new LpMeterPoint {DataValue = 1});

            Assert.AreEqual(1, value);
        }
    }
}
