using System;

namespace ProcessTrackingApp.Data.Models.Interfaces
{
    public interface IProcess
    {
        string ProcessName { get; set; }
        DateTime StartOfProcess { get; set; }
        DateTime? EndOfProcess { get; set; }

        TimeSpan Duration { get; }
    }
}
