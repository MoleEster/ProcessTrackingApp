using ProcessTrackingData.Data.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessTrackingData
{
    public static class ProcessesEnumerbleExtentions
    {
        private static List<TrackingProcess> currentProcesses = new List<TrackingProcess>();

        public static async Task<IEnumerable<TrackingProcess>> GetTrackingProcessesAsync(this IEnumerable<Process> allProcesses)
        {
            var result = await allProcesses.GetTrackingProcesses();
            return await Task.FromResult(result);
        }

        /// <summary>
        /// Возвращает коллекцию активных процессов
        /// </summary>
        /// <param name="allProcesses"></param>
        /// <returns></returns>
        public static Task<List<TrackingProcess>> GetTrackingProcesses(this IEnumerable<Process> allProcesses)
        {
            foreach (var process in allProcesses)
            {
                TrackingProcess newProcess = null;
                //создание нового процесса
                if (!string.IsNullOrEmpty(process.MainWindowTitle))
                {
                    DateTime startTime = DateTime.Now;
                    try
                    {
                        startTime = process.StartTime;
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        continue;
                    }
                    newProcess = new TrackingProcess(process.ProcessName, startTime);
                    newProcess.BasePriority = process.BasePriority;
                    //newProcess.Exited += newProcess.OnExited;
                    //newprocess.enableraisingevents = true;
                }
                else
                {
                    continue;
                }


                //Проверка на существование процесса
                var containsInList = currentProcesses.Where(x => x.ProcessName == newProcess.ProcessName && x.StartOfProcess == newProcess.StartOfProcess);
                if (!containsInList.Any() || containsInList == null)
                {
                    currentProcesses.Add(newProcess);
                }
                else
                {
                    if(containsInList.Count() != 1)
                    {
                        throw new Exception("More than one existing processes with same name and start of process");
                    }
                    else
                    {
                        var matchedProcess = currentProcesses.Find(x=> x == containsInList.First());
                        matchedProcess.EndOfProcess = newProcess.EndOfProcess;
                    }
                }
            }
            return Task.FromResult(currentProcesses);
        }


        /// <summary>
        /// В случае, если какой-то из процессов завершился, удаляет его из коллекции и запускает процесс сохранения завершившегося процесса
        /// </summary>
        /// <param name="processes"></param>
        /// <returns></returns>
        public static IEnumerable<TrackingProcess> CleanFromExitedProcesses(this IEnumerable<TrackingProcess> processes)
        {
            List<TrackingProcess> copy = processes.ToList();
            foreach (var process in copy)
            {
                var current = Process.GetProcessesByName(process.ProcessName);
                if (current.Length == 0)
                {
                    process.OnExited();
                    currentProcesses.Remove(process);
                }
            }
            return copy;
        }


       


        #region FirstAttempt
        //public static IEnumerable<TrackingProcess> GetTrackingProcesses(this IEnumerable<Process> allProcesses)
        //{
        //    Dictionary<string, int> Processes = new Dictionary<string, int>();
        //    foreach (var process in allProcesses)
        //    {
        //        string name = process.ProcessName;
        //        if (!Processes.ContainsKey(name) && !string.IsNullOrEmpty(process.MainWindowTitle))
        //        {
        //            var newProcess = new TrackingProcess(name);
        //            newProcess.Exited += newProcess.OnProcessExited;
        //            newProcess.EnableRaisingEvents = true;
        //            var containsInList = currentProcesses.Where(x => x.ProcessName == newProcess.ProcessName && x.StartOfProcess == newProcess.StartOfProcess);
        //            if (!containsInList.Any() || containsInList == null)
        //                currentProcesses.Add(newProcess);
        //            else
        //            {
        //                bool alreadyExists = false;
        //                foreach (var itemIncontains in containsInList)
        //                {
        //                    if (itemIncontains.EndOfProcess == newProcess.EndOfProcess)
        //                    {
        //                        alreadyExists = true;
        //                        break;
        //                    }
        //                }
        //                if (!alreadyExists)
        //                {
        //                    currentProcesses.Find()
        //                    currentProcesses.Add(newProcess));
        //                }
        //            }

        //            yield return newProcess;
        //        }
        //    }
        //}

        //public static IEnumerable<TrackingProcess> CompareTrakingProcesses(this IEnumerable<TrackingProcess> processesToCompare)
        //{
        //    foreach (var process in processesToCompare)
        //    {
        //        var containsInList = currentProcesses.Where(x => x.processName == process.ProcessName);
        //        if (containsInList.Any())
        //        {
        //            TrackingProcess processFromDictionary = containsInList.ElementAt(0).process;
        //            process.EndOfProcess = processFromDictionary.EndOfProcess;
        //        }
        //        yield return process;
        //    }
        //}

        #endregion
    }
}
