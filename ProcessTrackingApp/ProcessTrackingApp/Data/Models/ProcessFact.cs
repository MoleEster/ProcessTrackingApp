using ProcessTrackingApp.Data.Models.Interfaces;
using System;

namespace ProcessTrackingApp.Data.Models
{
    public class ProcessFact : IProcess
    {
        public ProcessFact(string processName, DateTime startOfTheProcess, DateTime? endOfTheProcess, TimeSpan duration)
        {
            ProcessName = processName;
            StartOfProcess = startOfTheProcess;
            if(!endOfTheProcess.HasValue)
                Duration = duration;
            else
                EndOfProcess = endOfTheProcess;
        }

        public string ProcessName { get; set; }
        public DateTime StartOfProcess { get; set; }
        public DateTime? EndOfProcess { get; set; }
        private TimeSpan? _duration;
        public TimeSpan Duration
        {
            get
            {
                return (EndOfProcess.Value - StartOfProcess);
            }
            private set
            {
                _duration = value;
            }
        }
    }
}
