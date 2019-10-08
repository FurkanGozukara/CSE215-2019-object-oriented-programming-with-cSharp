using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lecture_5_access_modifiers
{
    public class Cars
    {
        public int productionYear { get; set; }
        public int weight { get; set; }

        protected string model { get; set; }

        private int Id { get; set; }

        internal string brand { get; set; }

        public void setId(int irId)
        {
            Id = irId;
        }

        public int readId()
        {
            return Id;
        }

        public string getWeightKg()
        {
            return weight + " kg";
        }
    }
}
