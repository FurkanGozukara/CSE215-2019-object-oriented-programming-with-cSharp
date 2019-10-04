using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lecture_4_reference_passing_methods
{
    class Program
    {


        static void Main(string[] args)
        {
            List<string> lstTemp = new List<string>();
            addList(lstTemp);
            addList(lstTemp);
            foreach (var vrVal in lstTemp)
            {
                Console.WriteLine(vrVal);
            }

            Console.WriteLine();
            addList2(lstTemp);
            foreach (var vrVal in lstTemp)
            {
                Console.WriteLine(vrVal);
            }

            Console.WriteLine();
            addList3(ref lstTemp);
            foreach (var vrVal in lstTemp)
            {
                Console.WriteLine(vrVal);
            }
            Console.WriteLine();

            int irNum = 110;
            Console.WriteLine(irNum);
            intFunc1(irNum);
            Console.WriteLine(irNum);

            Console.WriteLine();

            intFunc2(ref irNum);
            Console.WriteLine(irNum);

            Console.ReadLine();
        }

        static int irRow = 0;

        static void addList(List<string> lstTemp)
        {
            lstTemp.Add(irRow+"\tgga");
            irRow++;
        }

        static void addList2(List<string> lstTemp)
        {
            lstTemp = new List<string>();
            lstTemp.Add("temp 1");
        }

        static void addList3(ref List<string> lstTemp)
        {
            lstTemp = new List<string>();
            lstTemp.Add("temp 1");
        }

        static void intFunc1(int irNum)
        {
            irNum++;
        }

        static void intFunc2(ref int irNum)
        {
            irNum++;
        }
    }
}
