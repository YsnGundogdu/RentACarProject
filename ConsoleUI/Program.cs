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
            //CarTest();
            //CarDeleteTest();
            //CarDtoTest();
            RentalManager rentalManager = new RentalManager(new EfRentalDal());
            rentalManager.Add(new Rental
            {
            });
                
                

        }

        private static void CarDtoTest()
        {
            CarManager carManager = new CarManager(new EfCarDal());

            var result = carManager.GetCarDetails();

            if (result.Success == true)
            {
                foreach (var car in result.Data)
                {
                    Console.WriteLine(car.CarDescription + "-----" + car.ColorName + "-----" + car.SegmentName + "-----" + car.DailyPrice);
                }
            }
            else
            {
                Console.WriteLine(result.Message);
            }
        }

        private static void CarDeleteTest()
        {
            CarManager carManager = new CarManager(new EfCarDal());

            carManager.Delete(new Car
            {
                CarId = 5
            });
        }

        private static void CarTest()
        {
            CarManager carManager = new CarManager(new EfCarDal());


            foreach (var car in carManager.GetAll().Data)
            {
                Console.WriteLine(car.CarDescription);
            }

            //carManager.Add(new Car
            //{
            //    CarDescription = "mercedes a200 bişey bişey",
            //    CarModelYear = 2020,
            //    ColorId = 5,
            //    SegmentId = 1,
            //    CarStatus = false,
            //    BrandId = 10
            //});
        }
    }
}
