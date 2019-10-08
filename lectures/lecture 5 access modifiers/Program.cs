using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            Trucks myFirstTruck = new Trucks();
            myFirstTruck.brand = "mercedes";
            myFirstTruck.capacity = 4566;
            myFirstTruck.productionYear = 412;
            myFirstTruck.weight = 4311;
            myFirstTruck.setId(342);

            Console.WriteLine($"My first car brand: {myFirstCar.brand}\tProduction Year: {myFirstCar.productionYear}\t Weight: {myFirstCar.getWeightKg()} \t Id: {myFirstCar.readId()}");

            Console.WriteLine($"My first track brand: {myFirstTruck.brand}\tProduction Year: {myFirstTruck.productionYear}\t Weight: {myFirstTruck.getWeightKg()} \t Id: {myFirstTruck.readId()} \t Capacity: {myFirstTruck.capacity}");

            Console.ReadLine();
        }
    }
}
