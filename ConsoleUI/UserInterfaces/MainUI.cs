using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleUI.UserInterfaces
{
    public class MainUI : UI, IUserInterface
    {
        public void Show(List<IUserInterface> userInterfaces)
        {
            int command;
            IUserInterface brandUI = userInterfaces[0];
            IUserInterface carUI = userInterfaces[1];
            IUserInterface userUI = userInterfaces[2];
            IUserInterface customerUI = userInterfaces[3];
            IUserInterface rentalUI = userInterfaces[4]; 

            do
            {
                Console.Clear();
                Console.WriteLine("1.Marka Operasyonları");
                Console.WriteLine("2.Araba Operasyonları");
                Console.WriteLine("3.Renk Operasyonları");
                Console.WriteLine("4.Kullanıcı Operasyonları");
                Console.WriteLine("5.Müşteri Operasyonları");
                Console.WriteLine("6.Kiralama Operasyonları");
                Console.WriteLine("0.Bye");
                command = Convert.ToInt32(RequestDecimal());
                switch (command)
                {
                    case 1:
                        brandUI.Show();
                        break;
                    case 2:
                        carUI.Show();
                        break;
                    case 4:
                        userUI.Show();
                        break;
                    case 5:
                        customerUI.Show();
                        break;
                    case 6:
                        rentalUI.Show();
                        break;
                    default:
                        Console.WriteLine("Unsupported Request!");
                        break;
                }
            } while (command != 0);
        }

    }
}
