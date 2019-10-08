using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lecture_5_access_modifiers
{
    public class Cars
    {
        public enum rims
        {
            rim_1 = 500,
            rim_2 = 700,
            rim_3 = 1200,
            rim_other=0
        }

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

        public static void Printgg(Cars gg)
        {
            Console.WriteLine($"year: {gg.productionYear}");
        }

        public void Printgg()
        {
            Console.WriteLine($"year: {productionYear}");
        }
    }
}
