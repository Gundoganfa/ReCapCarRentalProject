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
            Car car = new Car();

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
                        Console.Clear();
                        Console.WriteLine("ARABA LISTESI");
                        Console.WriteLine("#############\n");
                        foreach (var item in cars)
                        {
                            Console.WriteLine("Id:"+item.CarId + "\tbrandId:" + item.BrandId+"\tcolorId:"+item.ColorId+"\tprice:"+item.DailyPrice+"\tDesc:"+item.Description);
                        }
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("ARABA EKLEME MENUSU");
                        Console.WriteLine("###################\n");
                        Console.WriteLine("ARABA Tanımı:");
                        car.Description = Console.ReadLine();
                        Console.WriteLine("brandId:");
                        car.BrandId = (int)RequestDecimal();
                        Console.WriteLine("colorId:");
                        car.ColorId = (int)RequestDecimal();
                        Console.WriteLine("Daily Price :");
                        car.DailyPrice = (int)RequestDecimal();

                        Console.WriteLine("Sonuç:" + carManager.Add(car).Message);
                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine("ARABA DUZELTME MENUSU");
                        Console.WriteLine("#####################\n");
                        Console.WriteLine("Mevcut ARABA Id'si:");
                        car.CarId = Convert.ToInt32(RequestDecimal());
                        Console.WriteLine("Güncellenmiş ARABA Adı:");
                        car.Description = Console.ReadLine();
                        Console.WriteLine("Sonuç:" + carManager.Update(car).Message);
                        break;
                    case 4:
                        Console.Clear();
                        Console.WriteLine("ARABA SİLME MENUSU");
                        Console.WriteLine("###################\n");
                        Console.WriteLine("Silinecek Marka Id'si:");
                        car.CarId = Convert.ToInt32(RequestDecimal());
                        Console.WriteLine("Sonuç:" + carManager.DeleteById(car.CarId).Message);
                        break;
                    default:
                        Console.WriteLine("Unsupported Request!");
                        break;
                }

            } while (command != 0);
        }
    }
}
