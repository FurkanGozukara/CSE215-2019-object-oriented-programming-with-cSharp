using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lecture_11_console
{
    class Program
    {
        static void Main(string[] args)
        {
            var myTuple = returnVals();
            Console.WriteLine($"1 = { myTuple.Item1}");
            //myTuple.Item2 = "new item 2";//this gives error read only
            Console.WriteLine($"2 = { myTuple.Item2}");
            Console.WriteLine($"3 = { myTuple.Item3}");
            Console.WriteLine($"4 = { myTuple.Item4.ToString("N0")}");
            Console.WriteLine($"5 = { myTuple.Item5.ToString("N0")}");
            Console.WriteLine($"6 = { myTuple.Item6.ToString("N2")}");
            Console.WriteLine($"7 = { myTuple.Item7}");
            Console.WriteLine($"8 = { myTuple.Rest.Item1}");
            Console.WriteLine($"9 = { myTuple.Rest.Item2}");

            Console.Clear();

            csRefType ref1 = new csRefType();
            csRefType ref2 = ref1;
            ref2.gg = "ref 2";//ref 2 and ref 1 points to the same value same variable
            ref1.gg = "ref 1";

            Console.WriteLine($"ref 1.gg = {ref1.gg}");
            Console.WriteLine($"re2 2.gg = {ref2.gg}");

            valType val1;
            val1.wp = "g";
            valType val2 = val1;
            val2.wp = "val 2";//ref 2 and ref 1 points to the same value same variable
            val1.wp = "val 1";

            Console.WriteLine($"val 1.wp = {val1.wp}");
            Console.WriteLine($"val 2.wp = {val2.wp}");

          

            Console.ReadLine();
        }

        struct valType
        {
            public string wp;

            public valType(string srval)
            {
                wp = srval;
            }
        }

        class csRefType
        {
            public string gg = "";
        }

        static private Tuple<string, string, string, int, long, double, string, Tuple<string, string>> returnVals()
        {
            return new Tuple<string, string, string, int, long, double, string, Tuple<string, string>>
            (
                "first name",
                 "second name",
                 "school name",
                 11422,
                 6756867823,
                 123124.43953,
                 "street name",
                 new Tuple<string, string>("home office", "office home")
            );
        }
    }
}
