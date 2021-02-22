using Business.Concrete;
using Core.Utilities.Results;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleUI.Interfaces
{
    public class BrandUI : UI, IUserInterface
    {
        BrandManager brandManager;
        public BrandUI()
        {
            brandManager = new BrandManager(new EfBrandDal());
        }

        public void Show(List<IUserInterface> userInterfaces = null)
        {
            int command;
            List<Brand> brands = new List<Brand> { null };
            Brand brand = new Brand();

            Console.Clear();
            do
            {
                brands = brandManager.GetAll().Data;
                Console.WriteLine("1.Markaları Listele");
                Console.WriteLine("2.Marka Ekle");
                Console.WriteLine("3.Marka Düzelt");
                Console.WriteLine("4.Marka Sil");
                Console.WriteLine("0.Ana Menüye Dön");
                command = Convert.ToInt32(RequestDecimal());
                switch (command)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("MARKA LISTESI");
                        Console.WriteLine("#############\n");
                        foreach (var item in brands)
                        {
                            Console.WriteLine(item.BrandId + " " + item.Name);
                        }
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("MARKA EKLEME MENUSU");
                        Console.WriteLine("###################\n"); 
                        Console.WriteLine("Marka Adı:");
                        brand.Name = Console.ReadLine();
                        Console.WriteLine("Sonuç:" + brandManager.Add(brand).Message);
                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine("MARKA DUZELTME MENUSU");
                        Console.WriteLine("#####################\n");
                        Console.WriteLine("Mevcut Marka Id'si:");
                        brand.BrandId = Convert.ToInt32(RequestDecimal());
                        Console.WriteLine("Güncellenmiş Marka Adı:");
                        brand.Name = Console.ReadLine();
                        Console.WriteLine("Sonuç:" + brandManager.Update(brand).Message);
                        break;
                    case 4:
                        Console.Clear();
                        Console.WriteLine("MARKA SİLME MENUSU");
                        Console.WriteLine("###################\n");
                        Console.WriteLine("Silinecek Marka Id'si:");
                        brand.BrandId = Convert.ToInt32(RequestDecimal());
                        Console.WriteLine("Sonuç:" + brandManager.DeleteById(brand.BrandId).Message);
                        break;
                    default:
                        Console.WriteLine("Unsupported Request!");
                        break;
                }

            } while (command != 0);
        }
    }
}
