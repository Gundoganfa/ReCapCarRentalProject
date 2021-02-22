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
                var result = from rental in context.Rentals
                             join customer in context.Customers on rental.CustomerId equals customer.CustomerId
                             join user in context.Users on customer.UserId equals user.UserId
                             join car in context.Cars on rental.CarId equals car.CarId
                             select new RentalDetailDto
                             {
                                 RentalId = rental.RentalId,
                                 CarId = rental.CarId,
                                 CustomerId = customer.CustomerId,
                                 CustomerName = user.Name,
                                 CustomerSurname = user.LastName,
                                 DailyPrice = car.DailyPrice,
                                 RentDate = rental.RentDate,
                                 ReturnDate = rental.ReturnDate
                             };
                return result.ToList();
            }
        }
    }
}
