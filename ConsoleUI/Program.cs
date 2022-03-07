using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            CarManager carManager = new CarManager(new EfCarDal());

            
            foreach (var car in carManager.GetAll())
            {
                Console.WriteLine(car.CarDescription);
            }

            
            carManager.Add(new Car
            {
                CarDescription = "mercedes a200 bişey bişey",
                CarModelYear = 2000,
                ColorId = 5,
                SegmentId = 1,
                CarStatus = false,
                BrandId = 10
            });



        }
    }
}
