using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDbDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            string command = string.Empty;

            Console.WriteLine("Start Demos...");
            Console.WriteLine("Welcome to Mongodb Demo System (MDS) ");
            Console.WriteLine("You can enter commands that display some demos. ");
            Console.WriteLine("Or you wanna to exit this application, you can enter 'exit' to leave. ");

            while ((command = Console.ReadLine().ToLower()) != "exit")
            {
                if (!string.IsNullOrWhiteSpace(command))
                {
                    switch (command.ToLower())
                    {
                        case "1":
                            {
                                Demo1();
                                break;
                            }
                        case "2":
                            {
                                Demo2();
                                break;
                            }
                        case "3":
                            {
                                Demo3();
                                break;
                            }
                        default:
                            {
                                Console.WriteLine("not valid command.");
                                break;
                            }
                    }
                }
            }

            Console.WriteLine("End Demos...");
            Console.WriteLine("請按下任意鍵結束畫面...");
            Console.Read();
        }

        private static void Demo1()
        {
            Console.WriteLine("------------------ Demo 1 Start------------------");

            MongoDemo1 demo1 = new MongoDemo1();
            DemoViewModel demoResult = demo1.create();
            Console.WriteLine("new document id is {0}", demoResult._id);
            Console.WriteLine("new document date is {0}", demoResult.date);

            List<MongodbParameter> paras = new List<MongodbParameter>();
            paras.Add(new MongodbParameter() { key = "key", value = "demo_test_update" });
            paras.Add(new MongodbParameter() { key = "message", value = "demo測試mongo update" });

            demoResult.key = "demo_test_update";
            demoResult.message = "demo測試mongo update";
            demoResult.date = DateTime.Now;
            demoResult.packages = new List<DemoPackageViewModel>();
            demoResult.packages.Add(new DemoPackageViewModel() { name = "QQ軟糖", price = 100, quantity = 3 });
            demoResult.packages.Add(new DemoPackageViewModel() { name = "電腦", price = 18000, quantity = 1 });
            demoResult.packages.Add(new DemoPackageViewModel() { name = "MAC", price = 48000, quantity = 10 });
            demoResult.packages.Add(new DemoPackageViewModel() { name = "滑鼠", price = 390, quantity = 7 });

            paras.Add(new MongodbParameter() { key = "package", value = demoResult.packages });

            demo1.update(demoResult);

            Console.WriteLine("------------------ Demo 1 End------------------");
        }
        private static void Demo2()
        {
            Console.WriteLine("------------------ Demo 2 Start------------------");
            Console.WriteLine("target: update with parameters, but it cant update sub objects like packages");

            MongoDemo1 demo2 = new MongoDemo1();
            DemoViewModel demoResult = demo2.create();

            Console.WriteLine("new document id is {0}", demoResult._id);
            
            List<MongodbParameter> paras = new List<MongodbParameter>();
            paras.Add(new MongodbParameter() { key = "key", value = "demo_test_update" });
            paras.Add(new MongodbParameter() { key = "message", value = "demo測試mongo update" });

            demo2.update(demoResult._id, "date", paras);

            Console.WriteLine("ready to update...");

            Console.WriteLine("document key changed {0}", "demo_test_update");

            Console.WriteLine("------------------ Demo 2 End------------------");
        }

        private static void Demo3()
        {
            Console.WriteLine("------------------ Demo 3 Start------------------");
            Console.WriteLine("target: update with the new document, it will add packages in the document");
            MongoDemo1 demo3 = new MongoDemo1();
            DemoViewModel demoResult = demo3.create();

            Console.WriteLine("new document id is {0}", demoResult._id);
            Console.WriteLine("new document date is {0}", demoResult.date);

            List<MongodbParameter> paras = new List<MongodbParameter>();
            paras.Add(new MongodbParameter() { key = "key", value = "demo_test_update" });
            paras.Add(new MongodbParameter() { key = "message", value = "demo測試mongo update" });

            demoResult.key = "demo_test_update";
            demoResult.message = "demo測試mongo update";
            demoResult.date = DateTime.Now;
            demoResult.packages = new List<DemoPackageViewModel>();
            demoResult.packages.Add(new DemoPackageViewModel() { name = "QQ軟糖", price = 100, quantity = 3 });
            demoResult.packages.Add(new DemoPackageViewModel() { name = "電腦", price = 18000, quantity = 1 });
            demoResult.packages.Add(new DemoPackageViewModel() { name = "MAC", price = 48000, quantity = 10 });
            demoResult.packages.Add(new DemoPackageViewModel() { name = "滑鼠", price = 390, quantity = 7 });

            paras.Add(new MongodbParameter() { key = "package", value = demoResult.packages });

            demo3.update(demoResult);

            Console.WriteLine("ready to update...");
            Console.WriteLine("add packages");

            Console.WriteLine("document date changed {0}", demoResult.date);
            Console.WriteLine("document packages count is {0}", demoResult.packages.Count);

            Console.WriteLine("------------------ Demo 3 End------------------");
        }
    }
}
