using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace lecture_13_asyn_await
{
    class Program
    {
        static void Main(string[] args)
        {
            //callMethod();
            // callMethod2();
             callMethod3();
            Console.ReadKey();
        }

        public static void callMethod3()
        {
            int irResult = 0;

            Task.Factory.StartNew(() => { irResult = Method1alt(); }).ContinueWith(pr => Console.WriteLine(irResult) );
            Method2();
           
        }

        public static void callMethod2()
        {
            int irResult = 0;
            var vrTask1 = Task.Factory.StartNew(() => { irResult = Method1alt(); });
            Method2();
            Console.Clear();
            Console.WriteLine(irResult);
            vrTask1.Wait();
            Method3(irResult);
            
        }

        public static async void callMethod()
        {
            Task<int> task = Method1();//this starts and continue. does not wait to finish
            Method2();//this starts and program waits until it is done
            int count = await task;//here it waits until method 1 is done
            Method3(count);//and this executes
        }

        public static int Method1alt()
        {
            int count = 0;
        
                for (int i = 0; i < 100; i++)
                {
                    Console.WriteLine(" Method 1");
                    count += 1;
                }
          
            return count;
        }

        public static async Task<int> Method1()
        {
            int count = 0;
            await Task.Run(() =>
            {
                for (int i = 0; i < 100; i++)
                {
                    Console.WriteLine(" Method 1");
                    count += 1;
                }
            });
            return count;
        }

        public static void Method2()
        {
            for (int i = 0; i < 25; i++)
            {
                Console.WriteLine(" Method 2");
            }
        }

        public static void Method3(int count)
        {
            Console.WriteLine("Total count is " + count);
        }
    }
}
