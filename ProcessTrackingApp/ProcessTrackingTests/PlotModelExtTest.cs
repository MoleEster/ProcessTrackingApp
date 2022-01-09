using Microsoft.VisualStudio.TestTools.UnitTesting;
using OxyPlot;
using ProcessTrackigWPF.Models.PlotModel;
using ProcessTrackingApp.Data.Models;
using System;
using System.Collections.Generic;

namespace ProcessTrackingTests
{
    [TestClass]
    public class PlotModelExtTest
    {
        [TestMethod]
        public void OnCreateGraff_ShouldReturn_Grapf_When_BoundariesWithinAnHour_AndEqual()
        {
            var oxy = new PlotModel();

            List<ProcessFact> testFacts = new List<ProcessFact>();
            double[] expected = new double[24];
            for (int i = 0; i < expected.Length; i++)
            {
                expected[i] = 0;
            }
            for (int i = 0; i < 10; i++)
            {
                expected[i] = 0.5;
                testFacts.Add(new ProcessFact("test" + i, new DateTime(0001, 1, 1, i, 0, 0), new DateTime(0001, 1, 1, i, 30, 0),new TimeSpan(0,30,0)));
            }

            var actual = oxy.Create24HourGraffTest(testFacts);
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void OnCreateGraff_ShouldReturn_Grapf_When_BoundariesOutOfAnHour_AndEqual()
        {
            var oxy = new PlotModel();

            List<ProcessFact> testFacts = new List<ProcessFact>();
            double[] expected = new double[24];
            for (int i = 0; i < expected.Length; i+=3)
            {
                expected[i] = 1;
                expected[i+1] = 1;
                expected[i+2] = 0;
            }

            for (int i = 0; i < 24; i+=3)
            {
                testFacts.Add(new ProcessFact("test" + i, new DateTime(0001, 1, 1, i, 0, 0), new DateTime(0001, 1, 1, i+2, 0, 0), new TimeSpan(2, 0, 0)));
            }

            var actual = oxy.Create24HourGraffTest(testFacts);
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void OnCreateGraff_ShouldReturn_Grapf_When_BoundariesOutOfAnHour_NotEqual()
        {
            var oxy = new PlotModel();

            List<ProcessFact> testFacts = new List<ProcessFact>();
            double[] expected = new double[24];
            for (int i = 0; i < expected.Length; i++)
            {
                expected[i] = 0;
            }
            expected[0] = 1;
            expected[1] = 0.5;
            expected[2] = 0.25;
            expected[3] = 1;
            expected[4] = 1;
            expected[5] = 1;
            expected[6] = 1;
            expected[7] = 1;

            var fact1 = new ProcessFact("test" + 1, new DateTime(0001, 1, 1, 0, 0, 0), new DateTime(0001, 1, 1, 1, 30, 0), new TimeSpan(1, 30, 0));
            var fact2 = new ProcessFact("test" + 2, new DateTime(0001, 1, 1, 2, 45, 0), new DateTime(0001, 1, 1, 3, 0, 0), new TimeSpan(0, 15, 0));
            var fact3 = new ProcessFact("test" + 3, new DateTime(0001, 1, 1, 3, 0, 0), new DateTime(0001, 1, 1, 8, 0, 0), new TimeSpan(5, 0, 0));

            testFacts.Add(fact1);
            testFacts.Add(fact2);
            testFacts.Add(fact3);

            var actual = oxy.Create24HourGraffTest(testFacts);
            CollectionAssert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void OnCreateGraff_ShouldReturn_Grapf_When_FactsCross()
        {
            var oxy = new PlotModel();

            List<ProcessFact> testFacts = new List<ProcessFact>();
            double[] expected = new double[24];
            for (int i = 0; i < expected.Length; i++)
            {
                expected[i] = 0;
            }
            expected[0] = 1;
            expected[1] = 1;
            expected[2] = 1;
            expected[3] = 0;
            expected[4] = 1;
            expected[5] = 1;
            expected[6] = 1;
            expected[7] = 1;

            var fact1 = new ProcessFact("test" + 1, new DateTime(0001, 1, 1, 0, 0, 0), new DateTime(0001, 1, 1, 1, 30, 0), new TimeSpan(1, 30, 0));
            var fact2 = new ProcessFact("test" + 2, new DateTime(0001, 1, 1, 1, 0, 0), new DateTime(0001, 1, 1, 3, 0, 0), new TimeSpan(2, 0, 0));
            var fact3 = new ProcessFact("test" + 3, new DateTime(0001, 1, 1, 4, 0, 0), new DateTime(0001, 1, 1, 8, 0, 0), new TimeSpan(4, 0, 0));
            var fact4 = new ProcessFact("test" + 4, new DateTime(0001, 1, 1, 4, 0, 0), new DateTime(0001, 1, 1, 7, 0, 0), new TimeSpan(3, 0, 0));

            testFacts.Add(fact1);
            testFacts.Add(fact2);
            testFacts.Add(fact3);
            testFacts.Add(fact4);

            var actual = oxy.Create24HourGraffTest(testFacts);
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
