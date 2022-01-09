using ProcessTrackingApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProcessTrackingApp.Data.Extentions
{
    public static class ProcessFactEnumarebleExtentions
    {
        /// <summary>
        /// Возвращает коллекцию (начало, продолжительность),отсортированную по возрастанию начала, для коллекции фактов процессов
        /// </summary>
        /// <param name="processes"></param>
        /// <returns></returns>
        public static IEnumerable<(DateTime timeStart, TimeSpan duration)> MergeTimeRegions(this IEnumerable<ProcessFact> processes)
        {
            List<(DateTime timeStart, TimeSpan duration)> result = new List<(DateTime timeStart, TimeSpan duration)>();


            var sorted = processes.OrderBy(x => x.StartOfProcess).ThenBy(x => x.Duration);

            bool addLastElement = false;
            for (int i = 0; i <= sorted.Count() - 1; i++)
            {
                DateTime startofProcessMergedProcesses = sorted.ElementAt(i).StartOfProcess;
                TimeSpan durationOfMergedProcesses = sorted.ElementAt(i).Duration;
                while (i < sorted.Count() - 1)
                {
                    if (sorted.ElementAt(i).EndOfProcess.Value <= sorted.ElementAt(i + 1).StartOfProcess)
                    {
                        if (i + 1 == sorted.Count())
                            addLastElement = true;
                        break;
                    }
                    else
                    {
                        durationOfMergedProcesses += sorted.ElementAt(i + 1).Duration - (sorted.ElementAt(i).EndOfProcess.Value - sorted.ElementAt(i + 1).StartOfProcess);
                        i++;
                    }
                }
                result.Add((startofProcessMergedProcesses, durationOfMergedProcesses));
            }
            if (addLastElement)
            {
                result.Add((sorted.Last().StartOfProcess, sorted.Last().Duration));
            }
            return result;
        }
    }
}
