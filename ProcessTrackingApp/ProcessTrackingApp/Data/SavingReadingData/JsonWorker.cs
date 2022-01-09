using Newtonsoft.Json;
using ProcessTrackingApp.Data.Models;
using ProcessTrackingApp.Data.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessTrackingApp.Data.SavingData
{
    public static class JsonWorker
    {
        /// <summary>
        /// Название файла сохранения
        /// </summary>
        private static readonly string fileName = @"D:\программы на C#\ProcessTrackingApp\ProcessTrackingApp\Data.json";

        /// <summary>
        /// Асинхронное сохранение данных в файл
        /// </summary>
        /// <param name="process"></param>
        public static async void SaveDataToJsonAsync(IProcess process)
        {
            await SaveDataToJson(process);
        }
        private static Task SaveDataToJson(IProcess process)
        {
            var tcs = new TaskCompletionSource<IProcess>(process);
            var fact = new ProcessFact(process.ProcessName, process.StartOfProcess, DateTime.Now, process.Duration);
            var dataInFile = new List<ProcessFact>();
            dataInFile = GetDataFromFile().ToList();
            dataInFile.Add(fact);
            string data = JsonConvert.SerializeObject(dataInFile);
            //File.WriteAllText(fileName, data);
            Console.WriteLine(fact.ProcessName + "   Saved");
            return tcs.Task;
        }

        public static void SaveDataForTest(ProcessFact process)
        {
            var dataInFile = new List<ProcessFact>();
            dataInFile = GetDataFromFile().ToList();
            dataInFile.Add(process);
            string data = JsonConvert.SerializeObject(dataInFile);
            File.WriteAllText(fileName, data);
            Console.WriteLine(process.ProcessName + "   Saved");
        }

        /// <summary>
        /// Получение данных из файла
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ProcessFact> GetDataFromFile()
        {
            string textInFile = File.ReadAllText(fileName);
            var deserializedEnumerable = JsonConvert.DeserializeObject<IEnumerable<ProcessFact>>(textInFile);
            if (deserializedEnumerable != null)
                return deserializedEnumerable;
            else
                return Enumerable.Empty<ProcessFact>();
        }
        /// <summary>
        /// Получение данных имеющих определенную дату
        /// </summary>
        /// <param name="GetForExactDate"></param>
        /// <returns></returns>
        public static IEnumerable<ProcessFact> GetDataFromFile(Func<DateTime,bool> GetForExactDate)
        {
            string textInFile = File.ReadAllText(fileName);
            var deserializedEnumerable = JsonConvert.DeserializeObject<IEnumerable<ProcessFact>>(textInFile);
            if (deserializedEnumerable != null)
                return deserializedEnumerable.Where(x=> GetForExactDate(x.StartOfProcess));
            else
                return Enumerable.Empty<ProcessFact>();
        }

    }
}
