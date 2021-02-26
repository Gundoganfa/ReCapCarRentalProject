using Business.Concrete;
using Core.Utilities.Results;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleUI.UserInterfaces
{
    public class RentalUI : UI, IUserInterface
    {
        RentalManager rentalManager;
        public RentalUI()
        {
            rentalManager = new RentalManager(new EfRentalDal());
        }
        public void Show(List<IUserInterface> userInterfaces = null)
        {
            int command;
            List<RentalDetailDto> rentalDetails = new List<RentalDetailDto> { null };
            Rental rental = new Rental();

            Console.Clear();
            do
            {
                Console.WriteLine("1.Kira Kayıtlarını Listele");
                Console.WriteLine("2.Araç Kirala");
                Console.WriteLine("3.Araç Teslimi");
                Console.WriteLine("0.Ana Menüye Dön");
                command = Convert.ToInt32(RequestDecimal());
                switch (command)
                {
                    case 1:
                        rentalDetails = ListRentals();
                        break;
                    case 2:
                        NewRental(rental);
                        break;
                    case 3:
                        EndRental(ref rentalDetails);
                        break;
                    default:
                        Console.WriteLine("Unsupported Request!");
                        break;
                }

            } while (command != 0);

            void NewRental(Rental rental)
            {
                Console.Clear();
                Console.WriteLine("ARAÇ KİRALAMA MENÜSÜ");
                Console.WriteLine("###################\n");
                Console.WriteLine("Araç kiralayacak müşteri idsi:");
                rental.CustomerId = Convert.ToInt32(RequestDecimal());
                Console.WriteLine("Kiralanacak araç idsi:");
                rental.CarId = Convert.ToInt32(RequestDecimal());
                rental.RentDate = DateTime.Today;
                Console.WriteLine("Kiralama Başlangıç tarihi:{0}", rental.RentDate);
                if (rentalManager.Add(rental).Success)
                {
                    Console.WriteLine("Araç kiralandı");
                } else {
                    Console.WriteLine("Araç şu anda başkası tarafından kiralanmış durumda");
                }
            }
            List<RentalDetailDto> ListRentals()
            {
                List<RentalDetailDto> rentalDetails = rentalManager.GetRentalDetails().Data;
                Console.WriteLine("{0} Kayıt Listeleniyor", rentalDetails.Count);
                foreach (var rental in rentalDetails)
                {
                    Console.WriteLine("kayıt id:{0}\tcustomerId:{1}\tname:{2}\tsurname:{3}\tdailyPrice:{4}\tRent Date:{5}\tReturn Date:{6}",
                        rental.RentalId, rental.CustomerId, rental.CustomerName, rental.CustomerSurname, rental.DailyPrice, rental.RentDate, rental.ReturnDate);
                }
                return rentalDetails;
            }

            void EndRental(ref List<RentalDetailDto> rentalDetails)
            {
                int id;
                Console.Clear();
                Console.WriteLine("ARAÇ TESLİM ETME MENÜSÜ");
                Console.WriteLine("#######################\n");
                Console.WriteLine("Teslim edilecek araç idsi:");
                id = Convert.ToInt32(RequestDecimal());
                bool result = rentalManager.EndRental(id).Success;
                if (result)
                {
                    rentalDetails = rentalManager.GetRentalDetails().Data;
                    RentalDetailDto rentalDetail = new RentalDetailDto();
                    rentalDetail = rentalDetails.SingleOrDefault(r => r.RentalId == id);

                    decimal borc = rentalDetail.DailyPrice * CountDays(rentalDetail.RentDate, rentalDetail.ReturnDate);
                    Console.Clear();
                    Console.WriteLine("Borcunuz: ", borc);

                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Araç teslimi YAPILAMADI. Yeni bir id ile tekrar deneyiniz");
                }
            }

        }
    }
}
