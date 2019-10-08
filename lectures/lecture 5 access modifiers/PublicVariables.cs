using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lecture_5_access_modifiers
{
    public static class PublicVariables
    {
        //this can not be changed ever any place of the software and it is defined at compile time
        public const int maxSpeed = 300;
        //this can be changed during constructor of the static class
        public static readonly int maxSpeed2 = 300;

        static PublicVariables()
        {
            maxSpeed2 = Convert.ToInt32(File.ReadAllText("speed.txt"));
        }
    }
}
