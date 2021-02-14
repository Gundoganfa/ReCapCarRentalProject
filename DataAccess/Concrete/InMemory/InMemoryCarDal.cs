using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete
{
    public class InMemoryCarDal : ICarDal
    {
        List<Car> _cars;

        public InMemoryCarDal() // constructur DAL Newlendiğinde çağırılır
        {
            _cars = new List<Car>
            {
                new Car{CarId=1, BrandId=100, ColorId=1, DailyPrice=7500, Description="Mazda 2020 Model İlk Sahibinden", ModelYear=2020},
                new Car{CarId=2, BrandId=101, ColorId=1, DailyPrice=12000, Description="Hyundai 2020 Model Doktordan", ModelYear=2020},
                new Car{CarId=3, BrandId=102, ColorId=1, DailyPrice=10500, Description="Toyota 1998 Model En sağlam bu", ModelYear=2020},
                new Car{CarId=4, BrandId=102, ColorId=2, DailyPrice=9850, Description="Toyota 2000 Model Hafif yıpranmış", ModelYear=2020},
                new Car{CarId=5, BrandId=102, ColorId=3, DailyPrice=11200, Description="Toyota Corolla 2019", ModelYear=2020},
                new Car{CarId=6, BrandId=105, ColorId=1, DailyPrice=6000, Description="Mercedes Siyah", ModelYear=2020},
                new Car{CarId=7, BrandId=106, ColorId=1, DailyPrice=28000, Description="Volvo 2021 Model paran varsa al", ModelYear=2020}
            };
        }
        public void Add(Car car)
        {
            _cars.Add(car);
        }

        public void Delete(Car car)
        {
            Car carToDelete = _cars.SingleOrDefault(c => car.CarId == c.CarId);
            _cars.Remove(carToDelete);
        }

        public Car Get(Expression<Func<Car, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public List<Car> GetAll()
        {
            return _cars;
        }

        public List<Car> GetAll(Expression<Func<Car, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public List<Car> GetById(int id)
        {
            return _cars.Where(c => c.CarId == id).ToList();
        }

        public List<CarDetailDto> GetCarDetails()
        {
            throw new NotImplementedException();
        }

        public void Update(Car car)
        {
            Car carToUpdate = _cars.SingleOrDefault(c => car.CarId == c.CarId);
            carToUpdate.BrandId = car.BrandId;
            carToUpdate.ColorId = car.ColorId;
            carToUpdate.DailyPrice = car.DailyPrice;
            carToUpdate.Description = car.Description;
            carToUpdate.ModelYear = car.ModelYear;
        }
    }
}

