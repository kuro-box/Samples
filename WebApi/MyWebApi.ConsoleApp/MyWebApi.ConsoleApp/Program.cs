using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MyWebApi.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ExecuteAction(CallWebApi);
        }

        private static void ExecuteAction(Action action)
        {
            try { action.Invoke(); }
            catch (Exception ex) { Console.WriteLine(JsonConvert.SerializeObject(ex)); }
            Console.Read();
        }

        private static void Print()
        {
            Console.WriteLine("Helloworld");
        }

        private static void CallWebApi()
        {
            var httpClient = new HttpClient();
            var response = httpClient.GetAsync("http://localhost:8087/api/test/stream").Result;
            //var content = response.Content.ReadAsStringAsync();
            //var result = JsonConvert.SerializeObject(content);
            var result = response.Content.ReadAsStreamAsync().Result;

            var buffer = new byte[result.Length];
            result.Read(buffer, 0, buffer.Length);
            result.Seek(0, SeekOrigin.Begin);

            var msg = Encoding.ASCII.GetString(buffer);

            Console.WriteLine(msg);
        }
    }

}
