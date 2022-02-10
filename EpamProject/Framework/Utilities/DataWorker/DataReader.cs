using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System.IO;
using System.Linq;

namespace EpamProject.WebDriver.Utilities.DataWorker
{
    public class DataReader
    {
        private static string FilePath = "F:/RAB/EpamProject/EpamProject/WebDriver/Data/";
        private static string Environment = TestContext.Parameters["environment"];

        public static string GetTestData(string key)
        {
            var text = File.ReadAllText($"{FilePath}{Environment}.json");
            JObject json = JObject.Parse(text);

            return json.Descendants().OfType<JProperty>().Where(x => x.Name == key).First().Value.ToString();
        }
    }
}
