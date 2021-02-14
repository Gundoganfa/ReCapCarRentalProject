using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface ICarDal : IEntityRepository<Car>
    {
        //IEntityRepository interfaceinde tanımlanmamış fakat burada tanımlanabilecek
        //bir metod olabilir. Aşağıdaki örneği incele.
        List<CarDetailDto> GetCarDetails();

    }
}
