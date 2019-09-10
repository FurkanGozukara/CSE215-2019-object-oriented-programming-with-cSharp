using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myCustomMethodExtensions
{
    public static class csCustomExtensions
    {
        public static bool isInteger(this char c)
        {
            int irOut = 0;
            if (Int32.TryParse(c.ToString(), out irOut) == true)
                return true;
            return false;
        }
    }
}
