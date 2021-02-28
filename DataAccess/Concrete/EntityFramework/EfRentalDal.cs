using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRentalDal: EfEntityRepositoryBase<Rental,CarRentalPortalDBContext>, IRentalDal
    {
        public List<RentalDetailDto> GetRentalDetails()
        {
            using (CarRentalPortalDBContext context = new CarRentalPortalDBContext())
            {
                System.Console.WriteLine("yaziyor");
                var result = from rental in context.Rentals
                             join car in context.Cars on rental.CarId equals car.CarId

                             select new RentalDetailDto
                             {
                                 RentalId = rental.RentalId,
                                 CarId = rental.CarId,
                                 CustomerId = rental.CustomerId,
                                 RentDate = rental.RentDate,
                                 ReturnDate = rental.ReturnDate,
                                 DailyPrice = car.DailyPrice
                                 //CustomerName = user.Name,
                                 //CustomerSurname = user.LastName
                             };

                return result.ToList();
            }
        }
    }
}
