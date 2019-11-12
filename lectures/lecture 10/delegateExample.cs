using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//2019103004
namespace custom_delagates
{
    //https://www.geeksforgeeks.org/c-sharp-delegates/
    public static class delegateExample
    {
        public delegate int NumberChanger(int n);
        //default private
        static int num = 10;

        public static int AddNum(int p)
        {
            num += p;
            return num;
        }
        public static int MultNum(int q)
        {
            num *= q;
            return num;
        }
        public static int getNum()
        {
            return num;
        }
    }
}
