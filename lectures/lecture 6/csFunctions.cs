using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lecture_6
{
    //    Static Polymorphism
    //The mechanism of linking a function with an object during compile time is called early binding.It is also called static binding.C# provides two techniques to implement static polymorphism. They are −

    //Function overloading
    //Operator overloading
    //https://www.c-sharpcorner.com/UploadFile/ff2f08/understanding-polymorphism-in-C-Sharp/
    //https://www.tutorialspoint.com/csharp/csharp_polymorphism.htm

    public static class csFunctions
    {
        //Function overloading
        public static string returnFormattedString(int irVal)//signature int
        {
            return irVal.ToString("N0");
        }

        public static string returnFormattedString(double irVal)//signature double
        {
            return irVal.ToString("N2");
        }

        public static string returnFormattedString(Int64 irVal)//signature Int64
        {
            return (irVal*irVal).ToString("N0");
        }

        public static string returnFormattedString(int irVal, double irVal2)//signature int double
        {
            return (irVal* irVal2).ToString("N0");
        }

        public static string returnFormattedString(double irVal2, int irVal)//signature double int 
        {
            return (irVal * irVal2).ToString("N0");
        }
    }
}
