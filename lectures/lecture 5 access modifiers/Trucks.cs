using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lecture_5_access_modifiers
{
    public class Trucks : Cars
    {
        public int capacity { get; set; }

        public void setModel(string srModel)
        {
            this.model = srModel;
        }
    }
}
