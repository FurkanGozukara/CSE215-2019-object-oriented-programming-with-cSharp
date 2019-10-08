using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lecture_5_dll;

namespace lecture_5_access_modifiers
{
    class Program
    {
        static void Main(string[] args)
        {
            Cars myFirstCar = new Cars();
            myFirstCar.brand = "bmv";
            myFirstCar.productionYear = 2012;
            myFirstCar.weight = 3500;
            myFirstCar.setId(321);
            Cars.Printgg(myFirstCar);
            myFirstCar.Printgg();

            Trucks myFirstTruck = new Trucks();
            myFirstTruck.brand = "mercedes";
            myFirstTruck.capacity = 4566;
            myFirstTruck.productionYear = 412;
            myFirstTruck.weight = 4311;
            myFirstTruck.setId(342);
            myFirstTruck.setModel("a101");

            Console.WriteLine($"My first car brand: {myFirstCar.brand}\tProduction Year: {myFirstCar.productionYear}\t Weight: {myFirstCar.getWeightKg()} \t Id: {myFirstCar.readId()}");

            Console.WriteLine($"My first track brand: {myFirstTruck.brand}\tProduction Year: {myFirstTruck.productionYear}\t Weight: {myFirstTruck.getWeightKg()} \t Id: {myFirstTruck.readId()} \t Capacity: {myFirstTruck.capacity}");

            Taxi myTaxi = new Taxi();
            //since srTaxiBrand is internal and from different assembly i can not access it
            myTaxi.srTaxiName = "gg";

            Console.WriteLine($"max speed 1 : {PublicVariables.maxSpeed} \t max speed 2 : {PublicVariables.maxSpeed2}");

            Console.WriteLine("please select a rim from below list");

            foreach (Cars.rims vrRim in (Cars.rims[])Enum.GetValues(typeof(Cars.rims)))
            {
                Console.WriteLine(vrRim + " \t price: " + (int)vrRim);
            }

            var vrSelectedRim = Console.ReadLine();

            Cars.rims selectedRim = Cars.rims.rim_other;

            foreach (Cars.rims vrRim in (Cars.rims[])Enum.GetValues(typeof(Cars.rims)))
            {
                if (vrSelectedRim == vrRim.ToString())
                {
                    selectedRim = vrRim;
                }
            }

            switch (selectedRim)
            {
                case Cars.rims.rim_1:
                    Console.WriteLine("you have selected "+ selectedRim.ToString());
                    break;
                case Cars.rims.rim_2:
                    Console.WriteLine("you have selected " + selectedRim.ToString());
                    break;
                case Cars.rims.rim_3:
                    Console.WriteLine("you have selected " + selectedRim.ToString());
                    break;
                case Cars.rims.rim_other:
                    Console.WriteLine("you have selected " + selectedRim.ToString());
                    break;
            }

            Console.ReadLine();
        }
    }
}
