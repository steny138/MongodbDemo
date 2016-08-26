using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDbDemo
{
    class LogMongodbDemo
    {
        static void Main(string[] args)
        {
            string command = string.Empty;

            Console.WriteLine("Start Demos...");
            Console.WriteLine("Welcome to Mongodb Demo System (MDS) ");
            Console.WriteLine("You can enter commands that display some demos. ");
            Console.WriteLine("Or you wanna to exit this application, you can enter 'exit' to leave. ");

            MongoDemo2 demo = new MongoDemo2();
            demo.t();

            //while ((command = Console.ReadLine().ToLower()) != "exit")
            //{
            //    if (!string.IsNullOrWhiteSpace(command))
            //    {
            //        switch (command.ToLower())
            //        {
            //            case "1":
            //                {
            //                    Demo1();
            //                    break;
            //                }
            //            case "2":
            //                {
            //                    Demo2();
            //                    break;
            //                }
            //            case "3":
            //                {
            //                    Demo3();
            //                    break;
            //                }
            //            default:
            //                {
            //                    Console.WriteLine("not valid command.");
            //                    break;
            //                }
            //        }
            //    }
            //}

            Console.WriteLine("End Demos...");
            Console.WriteLine("請按下任意鍵結束畫面...");
            Console.Read();
        }
    }
}
