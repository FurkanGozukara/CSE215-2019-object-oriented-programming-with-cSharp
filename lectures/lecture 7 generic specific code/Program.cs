using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lecture_7_generic_specific_code
{
    class Program
    {
        public class myCustomClass
        {
            public int customProp { get; set; }

            public myCustomClass(int irVal)
            {
                customProp = irVal;
            }
        }

        static void Main(string[] args)
        {
            // int is the type argument
            GenericList<int> list = new GenericList<int>();

            for (int x = 0; x < 10; x++)
            {
                list.AddHead(x);
            }

            var index14 = list[14];

            foreach (int i in list)
            {
                System.Console.Write(i + " ");
            }

            Console.WriteLine();
            System.Console.WriteLine(list[12]);
            System.Console.WriteLine("\nDone");

            GenericList<string> lstStr = new GenericList<string>();

            lstStr.AddHead("deneme 1");
            lstStr.AddHead("deneme 2");

            var index3 = lstStr[3];

            foreach (var vrItem in lstStr)
            {
                Console.WriteLine(vrItem);
            }

            GenericList<myCustomClass> lstCustom = new GenericList<myCustomClass>();

            Console.WriteLine();
            for (int i = 0; i < 100; i++)
            {
                lstCustom.AddHead(new myCustomClass(i));
            }

            foreach (var vrPerCustom in lstCustom)
            {
                Console.Write("\t" + vrPerCustom.customProp);
            }
            Console.WriteLine();
            Console.WriteLine("special yield return");

            foreach (int i in SpecialPower(2, 8))
            {
                Console.Write("{0} ", i);
            }

            Console.ReadLine();
        }

        // type parameter T in angle brackets
        // where T : struct 

        //public class GenericList where T : struct
        public class GenericList<T>// where T : struct // e.g. if i force it to be struct i can not use string or class types 
        {
            // The nested class is also generic on T.
            private class Node
            {
                // T used in non-generic constructor.
                public Node(T t)
                {
                    next = null;
                    data = t;
                }

                private Node next;
                public Node Next
                {
                    get { return next; }
                    set { next = value; }
                }

                // T as private member data type.
                private T data;

                // T as return type of property.
                public T Data
                {
                    get { return data; }
                    set { data = value; }
                }
            }

            private Node head;

            // constructor
            public GenericList()
            {
                Lenght = 0;
                head = null;
            }

            public int Lenght { get; set; }

            // T as method parameter type:
            public void AddHead(T t)
            {
                Lenght++;
                Node n = new Node(t);
                n.Next = head;
                head = n;
            }

            public IEnumerator<T> GetEnumerator()
            {
                Node current = head;

                while (current != null)
                {
                    yield return current.Data;
                    current = current.Next;
                }
            }

            public T this[int index]
            {
                get
                {
                    if (index >= Lenght)
                    {
                        return default(T);//if t is class type null, if primite 0, if struct default of struct
                        throw new System.ArgumentException("Out of range error", "index");
                    }
                    Node current = head;
                    for (int i = 0; i < index; i++)
                    {
                        current = current.Next;
                    }
                    return current.Data;
                }
            }
        }

        public static IEnumerable<int> SpecialPower(int number, int exponent)
        {
            int result = 1;

            for (int i = 0; i < exponent; i++)
            {
                result = result * number;
                yield return result;
            }
        }
    }
}
