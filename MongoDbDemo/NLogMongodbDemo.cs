using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDbDemo
{
    public class NLogMongodbDemo
    {
        static void Main(string[] args)
        {
            string command = string.Empty;

            Console.WriteLine("Start Demos...");
            Console.WriteLine("Welcome to Mongodb Demo System (MDS) ");
            Console.WriteLine("You can enter commands that display some demos. ");
            Console.WriteLine("Or you wanna to exit this application, you can enter 'exit' to leave. ");

            MongoDemo3 demo = new MongoDemo3();
            demo.demo_insert();

            Console.WriteLine("End Demos...");
            Console.WriteLine("請按下任意鍵結束畫面...");
            Console.Read();
        }

    }
}
