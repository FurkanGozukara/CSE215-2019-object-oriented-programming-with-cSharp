using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lecture_6
{
    /// <summary>
    /// https://www.geeksforgeeks.org/c-sharp-abstraction/
    /// </summary>
    public class csAbstract
    {
        public abstract class Shape//abstract class is used to enforce rules
        {
            public abstract string srKeyword { get; set; } //abstract property        
            public abstract int area();  // abstract method 
        }

        // square class inherting  the Shape class 
        public class Square : Shape
        {
            private int side;
            private int irKey = 1231;
            public Square(int x = 0)
            {
                side = x;
            }
            // overriding of the abstract method of Shape class using the "override" keyword 
            public override int area()
            {
                Console.Write("Area of Square: ");
                return (side * side);
            }

            private string _keyword = "";
            //this is encapsulation
            public override string srKeyword { get { return _keyword; } set { _keyword = value; } }
            
            public int returnKey()//encapsulation
            {
                return irKey;
            }
        }
    }
}
