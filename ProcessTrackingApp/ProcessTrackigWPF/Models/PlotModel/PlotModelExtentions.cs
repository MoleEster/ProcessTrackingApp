using OxyPlot;
using OxyPlot.Series;
using ProcessTrackingApp.Data.Extentions;
using ProcessTrackingApp.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace ProcessTrackigWPF.Models.PlotModel
{
    public static class PlotModelExtentions
    {
        /// <summary>
        /// Построение графика использования за 24 часа
        /// </summary>
        /// <param name="model"></param>
        /// <param name="facts"></param>
        public static void Create24HourGraff(this OxyPlot.PlotModel model, IEnumerable<ProcessFact> facts)
        {
            double[] results = new double[24];

            var mergedRegions = facts.MergeTimeRegions();

            foreach (var region in mergedRegions)
            {
                var totalMinutes = region.duration.TotalMinutes;
                var start = region.timeStart.Minute;
                int i = 0;
                do
                {
                    if (60 - start >= totalMinutes)
                    {
                        results[region.timeStart.Hour + i] = totalMinutes / 60;
                    }
                    else
                    {
                        results[region.timeStart.Hour + i] = (60 - start) / 60;
                        start = 0;
                        i++;
                    }
                } while ((totalMinutes -= 60) > 0);
            }

            for (int i = 0; i < 23; i++)
            {
                (model.Series.ElementAt(0) as LineSeries).Points.Add(new DataPoint(i,results[i]));

            }
        }


        public static double[] Create24HourGraffTest(this OxyPlot.PlotModel model, IEnumerable<ProcessFact> facts)
        {
            double[] results = new double[24];

            var mergedRegions = facts.MergeTimeRegions();

            foreach (var region in mergedRegions)
            {
                var totalMinutes = region.duration.TotalMinutes;
                var start = region.timeStart.Minute;
                int i = 0;
                do
                {
                    if (60 - start >= totalMinutes)
                    {
                        results[region.timeStart.Hour + i] = totalMinutes / 60;
                    }
                    else
                    {
                        results[region.timeStart.Hour + i] = (60 - start) / 60;
                        start = 0;
                        i++;
                    }
                } while ((totalMinutes -= 60) > 0);
            }
            return results;
        }
    }
}
