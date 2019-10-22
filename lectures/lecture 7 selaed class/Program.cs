using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lecture_7_selaed_class
{
    class Program
    {
        static void Main(string[] args)
        {
            Z myClass = new Z();
            myClass.F();
            myClass.F2();
            myClass.F3();
            Console.ReadKey();
        }

        sealed class csMine
        {
            public int Add(int x, int y)
            {
                return x + y;
            }
        }

        //class csOther : csMine//can not derive from a sealed type
        //{

        //}

        class X
        {
            //we put virtual so derived class can override it
            public virtual void F()
            {
                Console.WriteLine("X.F");
            }
            public virtual void F2()
            {
                Console.WriteLine("X.F2");
            }
        }
        class Y : X
        {
            sealed public override void F()
            {
                Console.WriteLine("Y.F");
            }
            public override void F2()
            {
                Console.WriteLine("X.F3");
            }
        }
        class Z : Y
        {
            // Attempting to override F causes compiler error CS0239.  
            //   
            //protected override void F()//you can not override sealed method
            //{
            //    Console.WriteLine("C.F");
            //}
            // Overriding F2 is allowed.   
            public override void F2()
            {
                Console.WriteLine("Z.F2");
            }

            public void F3()
            {
                base.F2();
            }
        }
    }
}
