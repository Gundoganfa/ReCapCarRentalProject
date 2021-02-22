using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;
using System.Linq;



namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, CarSalesPortalDBContext>, ICarDal
    {
        public List<CarDetailDto> GetCarDetails()
        {
            // IDisposable
            using (CarSalesPortalDBContext context = new CarSalesPortalDBContext())
            {
                var result = from car in context.Cars
                             join color in context.Colors on car.ColorId equals color.ColorId
                             join brand in context.Brands on car.BrandId equals brand.BrandId
                             select new CarDetailDto
                             {
                                 CarId = car.CarId,
                                 CarName = car.Description,
                                 Brand = brand.Name,
                                 Color = color.Name,
                                 DailyPrice = car.DailyPrice
                             };
                return result.ToList();
            }
        }

    }
}
