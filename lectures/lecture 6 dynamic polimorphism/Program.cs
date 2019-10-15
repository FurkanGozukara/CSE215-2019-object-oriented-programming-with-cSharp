using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lecture_6_dynamic_polimorphism
{
    class Program
    {
        static void Main(string[] args)
        {

            Drawing circle = new Circle();
            Console.WriteLine("Area :" + circle.Area());

            Drawing square = new Square();
            Console.WriteLine("Area :" + square.Area());

            Drawing rectangle = new Rectangle();
            Console.WriteLine("Area :" + rectangle.Area());

            Rectangle2 rectangle2 = new Rectangle2();
            Console.WriteLine("Area 2 :" + rectangle2.Area2());

            Console.WriteLine("Area 3 :" + rectangle2.Area3());

            Console.ReadLine();
        }
    }

    public class Drawing
    {
        public virtual double Area()
        {
            return 0;
        }
    }

    public class Circle : Drawing
    {
        public double Radius { get; set; }
        public Circle()
        {
            Radius = 5;
        }
        public override double Area()
        {
            return (3.14) * Math.Pow(Radius, 2);
        }
    }

    public class Square : Drawing
    {
        public double Length { get; set; }
        public Square()
        {
            Length = 6;
        }
        public override double Area()
        {
            return Math.Pow(Length, 2);
        }
    }

    public class Rectangle : Drawing
    {
        public double Height { get; set; }
        public double Width { get; set; }
        public Rectangle()
        {
            Height = 5.3;
            Width = 3.4;
        }
        public sealed override double Area()
        {
            return Height * Width;
        }
    }

    public class Rectangle2 : Rectangle
    {
        public new double Height { get; set; }//this hides parent class 
        public new double Width { get; set; }//this hides parent class 
        public Rectangle2()
        {
            Height = 1.3;
            Width = 1.4;
        }
        //public override double Area() this does not work because sealed
        //{
        //    return Height * Width;
        //}

        public double Area2()
        {
            return base.Height * base.Width;
        }

        public double Area3()
        {
            return Height * Width;
        }
    }

}
