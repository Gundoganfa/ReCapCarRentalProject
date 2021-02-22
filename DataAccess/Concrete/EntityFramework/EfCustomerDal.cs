using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCustomerDal: EfEntityRepositoryBase<Customer,CarRentalPortalDBContext>, ICustomerDal
    {
        public List<CustomerDetailDto> GetCustomerDetails()
        {
            // IDisposable
            using (CarRentalPortalDBContext context = new CarRentalPortalDBContext())
            {
                var result = from customer in context.Customers
                             join user in context.Users on customer.UserId equals user.UserId
                             select new CustomerDetailDto
                             {
                                 CustomerId = customer.CustomerId,
                                 Name = user.Name,
                                 LastName = user.LastName,
                                 CompanyName = customer.CompanyName,
                                 Email = user.Email,
                                 Password = user.Password
                             };
                return result.ToList();
            }
        }
    }
}
