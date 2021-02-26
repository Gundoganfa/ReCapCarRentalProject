using Business.Concrete;
using Core.Utilities.Results;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleUI.UserInterfaces
{
    public class CustomerUI : UI, IUserInterface
    {
        CustomerManager customerManager;
        public CustomerUI()
        {
            customerManager = new CustomerManager(new EfCustomerDal());
        }

        public void Show(List<IUserInterface> userInterfaces = null)
        {
            int command;
            List<Customer> customers = new List<Customer> { null };
            Customer customer = new Customer();
            List<CustomerDetailDto> customerDetails = new List<CustomerDetailDto> { null };

            Console.Clear();
            do
            {
                customerDetails = customerManager.GetCustomerDetails().Data;

                Console.WriteLine("1.Müşterileri Listele");
                Console.WriteLine("2.Müşteri Ekle");
                Console.WriteLine("3.Müşteri Sil");
                Console.WriteLine("0.Ana Menüye Dön");
                command = Convert.ToInt32(RequestDecimal());
                switch (command)
                {
                    case 1:
                        ListCustomers(customerDetails);
                        break;
                    case 2:
                        AddCustomer();
                        break;
                    case 3:
                        DeleteCustomer();
                        break;
                    default:
                        Console.WriteLine("Unsupported Request!");
                        break;
                }

            } while (command != 0);

            void DeleteCustomer()
            {
                Console.Clear();
                Console.WriteLine("MÜŞTERİ SİLME MENUSU");
                Console.WriteLine("###################\n");
                Console.WriteLine("Silinecek müşteri id'si:");
                int customerId = Convert.ToInt32(RequestDecimal());
                Console.WriteLine("Sonuç:" + customerManager.DeleteById(customerId).Message);
            }

            void AddCustomer()
            {
                Console.Clear();
                Console.WriteLine("MÜŞTERİ EKLEME MENUSU");
                Console.WriteLine("###################\n");
                Console.WriteLine("Müşteri olacak üye IDsi:");
                Customer customerToAdd = new Customer();
                customerToAdd.UserId = Convert.ToInt32(RequestDecimal());
                Console.WriteLine("Sonuç:" + customerManager.Add(customerToAdd).Message);
            }

            static void ListCustomers(List<CustomerDetailDto> customers)
            {
                Console.Clear();
                Console.WriteLine("MÜŞTERİ LİSTESİ");
                Console.WriteLine("#############\n");
                foreach (var item in customers)
                {
                    Console.WriteLine("Customer Id:{0} \tName:{1} \tSurname:{2} \tEmail:{3} \tCompanyName:{4}", item.CustomerId, item.Name, item.LastName, item.Email, item.CompanyName);
                }
            }
        }
    }
}
