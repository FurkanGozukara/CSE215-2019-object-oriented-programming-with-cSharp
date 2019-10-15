using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lecture_6
{
    public class customShapes
    {
        public class Shape
        {
            // A few example members
            public int X { get; private set; }
            public int Y { get; private set; }
            public int Height { get; set; }
            public int Width { get; set; }

            // Virtual method so i can override in inherited class
            public virtual void Draw()
            {
                Debug.WriteLine("Performing base class drawing tasks");
            }
        }

        public class Circle : Shape
        {
            public override void Draw()
            {
                // Code to draw a circle...
                Debug.WriteLine("Drawing a circle");
                base.Draw();
            }
        }
        public class Rectangle : Shape
        {
            public override void Draw()
            {
                // Code to draw a rectangle...
                Debug.WriteLine("Drawing a rectangle");
                base.Draw();
            }
        }
        public class Triangle : Shape
        {
            public override void Draw()
            {
                // Code to draw a triangle...
                Debug.WriteLine("Drawing a triangle");
                base.Draw();
            }
        }

    }
}
