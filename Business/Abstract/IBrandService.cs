using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IBrandService
    {
        IResult Update(Brand brand);
        IResult DeleteById(int brandId);

        IDataResult<List<Brand>> GetAll();
        IDataResult<Brand> GetById(int brandId);
        IResult Insert(Brand brand);
        IResult Delete(Brand brand);



    }
}
