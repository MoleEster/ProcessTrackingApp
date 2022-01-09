using Newtonsoft.Json;
using ProcessTrackingData.Data.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ProcessTrackingApp.Data
{
    public static class TrackingProcessesData
    {
        public static IEnumerable<TrackingProcess> DeserializeDataFromJson()
        {
            string jsonString = File.ReadAllText("Data.json");
            var deserializedProcesses = JsonConvert.DeserializeObject<IEnumerable<TrackingProcess>>(jsonString);
            foreach (var deserializedProcess in deserializedProcesses)
            {
                yield return deserializedProcess;
            }
        }

        public static void SerializeData(this IEnumerable<TrackingProcess> processes)
        {
            var existingData = DeserializeDataFromJson();
            existingData.Concat(processes);
            var data = JsonConvert.SerializeObject(existingData);
            File.WriteAllText("Data.json", data);
        }
    }
}
