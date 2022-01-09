using ProcessTrackingApp.Data.Models.Interfaces;
using ProcessTrackingApp.Data.SavingData;
using System;
using System.Diagnostics;

namespace ProcessTrackingData.Data.Models
{

    public class TrackingProcess : Process, IProcess
    {
        public TrackingProcess(string name, DateTime start)
        {
            ProcessName = name;
            StartOfProcess = start;
        }

        new public string ProcessName { get; set; }
        new public int BasePriority { get; set; }
        private DateTime _stratOfProcess;
        public DateTime StartOfProcess
        { 
            get
            { 
                return _stratOfProcess.ToUniversalTime();
            }
            set
            {
                _stratOfProcess = value; 
            } 
        }
        public DateTime? EndOfProcess { get; set; }
        public TimeSpan Duration
        {
            get
            {
                if (!EndOfProcess.HasValue)
                    EndOfProcess = DateTime.Now;
                return (EndOfProcess.Value - StartOfProcess);
            }
        }
        public ProcessType ProcessType { get; set; }
        new public void OnExited()
        {   
            JsonWorker.SaveDataToJsonAsync(this);
        }
    }
}
