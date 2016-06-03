using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactory.App.Bootstarp
{
    class Program
    {
        static void Main(string[] args)
        {
        }

        public class Road
        {
            public string Id { get; set; }

            public string Code { get; set; }
        }

        public class RoadFactory
        {
            public static Road CreateRoad()
            {
                return new Road();
            }
        }
    }
}
