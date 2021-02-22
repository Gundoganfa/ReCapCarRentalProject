using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleUI.Interfaces
{
    public class CarUI : UI, IUserInterface
    {
        CarManager carManager;
        public CarUI()
        {
            carManager = new CarManager(new EfCarDal());
        }

        public void Show(List<IUserInterface> userInterfaces = null)
        {
            int command;
            List<Car> cars = new List<Car> { null };

            Console.Clear();
            do
            {
                cars = carManager.GetAll().Data;
                Console.WriteLine("1.ARABA Listele");
                Console.WriteLine("2.ARABA Ekle");
                Console.WriteLine("3.ARABA Düzelt");
                Console.WriteLine("4.ARABA Sil");
                Console.WriteLine("0.Ana Menüye Dön");
                command = Convert.ToInt32(RequestDecimal());
                switch (command)
                {
                    case 1:
                        ListCars(cars);
                        break;
                    case 2:
                        AddCars();
                        break;
                    case 3:
                        UpdateCar();
                        break;
                    case 4:
                        DeleteCar();
                        break;
                    default:
                        Console.WriteLine("Unsupported Request!");
                        break;
                }

            } while (command != 0);

            static void ListCars(List<Car> cars)
            {
                Console.Clear();
                Console.WriteLine("ARABA LISTESI");
                Console.WriteLine("#############\n");
                foreach (var item in cars)
                {
                    Console.WriteLine("Id:" + item.CarId + "\tbrandId:" + item.BrandId + "\tcolorId:" + item.ColorId + "\tprice:" + item.DailyPrice + "\tDesc:" + item.Description);
                }
            }

            void AddCars()
            {
                Console.Clear();
                Console.WriteLine("ARABA EKLEME MENUSU");
                Console.WriteLine("###################\n");
                Console.WriteLine("ARABA Tanımı:");
                Car carToAdd = new Car();
                carToAdd.Description = Console.ReadLine();
                Console.WriteLine("brandId:");
                carToAdd.BrandId = (int)RequestDecimal();
                Console.WriteLine("colorId:");
                carToAdd.ColorId = (int)RequestDecimal();
                Console.WriteLine("Daily Price :");
                carToAdd.DailyPrice = (int)RequestDecimal();

                Console.WriteLine("Sonuç:" + carManager.Add(carToAdd).Message);
            }

            void UpdateCar()
            {
                Console.Clear();
                Console.WriteLine("ARABA DUZELTME MENUSU");
                Console.WriteLine("#####################\n");
                Console.WriteLine("Mevcut ARABA Id'si:");
                Car carToUpdate = new Car();
                carToUpdate.CarId = Convert.ToInt32(RequestDecimal());
                carToUpdate = carManager.GetById(carToUpdate.CarId).Data;
                Console.WriteLine("Güncellenmiş ARABA Adı:");
                carToUpdate.Description = Console.ReadLine();
                Console.WriteLine("Sonuç:" + carManager.Update(carToUpdate).Message);
            }

            void DeleteCar()
            {
                Console.Clear();
                Console.WriteLine("ARABA SİLME MENUSU");
                Console.WriteLine("###################\n");
                Console.WriteLine("Silinecek Marka Id'si:");
                int carId = Convert.ToInt32(RequestDecimal());
                Console.WriteLine("Sonuç:" + carManager.DeleteById(carId).Message);
            }
        }
    }
}
