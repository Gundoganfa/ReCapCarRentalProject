using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleUI.UserInterfaces
{
    public class UserUI: UI, IUserInterface
    {
        UserManager userManager;
        public UserUI()
        {
            userManager = new UserManager(new EfUserDal());
        }
        public void Show(List<IUserInterface> userInterfaces = null)
        {
            int command;
            List<User> users = new List<User> { null };
            User user = new User();
            Console.Clear();
            do
            {
                users = userManager.GetAll().Data;
                Console.WriteLine("1.Kullanıcıları Listele");
                Console.WriteLine("2.Kullanıcı Ekle");
                Console.WriteLine("4.Kullanıcı Sil");
                Console.WriteLine("0.Ana Menüye Dön");
                command = Convert.ToInt32(RequestDecimal()); 
                switch (command)
                {
                    case 1:
                        ListUsers(users);
                        break;
                    case 2:
                        AddUser();
                        break;
                    case 4:
                        DeleteUser();
                        break;
                    default:
                        Console.WriteLine("Unsupported Request!");
                        break;
                }
            } while (command != 0);
        }

        void DeleteUser()
        {
            Console.Clear();
            Console.WriteLine("KULLANICI SİLME MENUSU");
            Console.WriteLine("######################\n");
            Console.WriteLine("Silinecek KULLANICI Id'si:");
            int userId = Convert.ToInt32(RequestDecimal());
            Console.WriteLine("Sonuç:" + userManager.DeleteById(userId).Message);
        }
        void AddUser()
        {
            Console.Clear();
            Console.WriteLine("KULLANICI EKLEME MENUSU");
            Console.WriteLine("#######################\n");
            Console.WriteLine("KULLANICI Adı:");
            User userToAdd = new User();
            userToAdd.Name = Console.ReadLine();
            Console.WriteLine("KULLANICI Soyadı:");
            userToAdd.LastName = Console.ReadLine();
            Console.WriteLine("KULLANICI Email:");
            userToAdd.Email = Console.ReadLine();
            Console.WriteLine("KULLANICI Password:");
            userToAdd.Password = Console.ReadLine();
            Console.WriteLine("Sonuç:" + userManager.Add(userToAdd).Message);
        }
        static void ListUsers(List<User> users)
        {
            Console.Clear();
            Console.WriteLine("KULLANICI LISTESI");
            Console.WriteLine("#############\n");
            foreach (var item in users)
            {
                Console.WriteLine("userID: "+item.UserId + "\tAd: " + item.Name + "\tSoyad: " + item.LastName + "\tEmail: " + item.Email);
            }
        }
    }
}
