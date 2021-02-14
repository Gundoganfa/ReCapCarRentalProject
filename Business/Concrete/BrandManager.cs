using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class BrandManager : IBrandService
    {
        IBrandDal _brandDal;

        public BrandManager(IBrandDal brandDal)
        {
            _brandDal = brandDal;
        }

        public IResult Insert(Brand brand)
        {
            try
            {
                _brandDal.Add(brand); 
                return new SuccessResult(Messages.AddSuccess);
            }
            catch (Exception)
            {
                return new ErrorResult(Messages.AddError);
            }
        }

        public IResult DeleteById(int brandId)
        {
            try
            {
                _brandDal.Delete(_brandDal.Get(b => b.BrandId == brandId));
                return new SuccessResult(Messages.DeleteSuccess);
            }
            catch (Exception)
            {
                return new ErrorResult(Messages.DeleteError);
            }  
        }

        public IResult Update(Brand brand)
        {
            try
            {
                _brandDal.Update(brand);
                return new SuccessResult(Messages.UpdateSuccess);
            }
            catch (Exception)
            {
                return new ErrorResult(Messages.UpdateError);
            }
        }
        public IResult Delete(Brand brand)
        {
            try
            {
                _brandDal.Delete(brand);
                return new SuccessResult(Messages.DeleteSuccess);
            }
            catch (Exception)
            {
                return new ErrorResult(Messages.DeleteError);
            }
        }
        public IDataResult<List<Brand>> GetAll()
        {
            return new SuccessDataResult<List<Brand>>(_brandDal.GetAll(), Messages.ProductsListed);
        }

        public IDataResult<Brand> GetById(int brandId)
        {
            return new SuccessDataResult<Brand>(_brandDal.Get(b => b.BrandId == brandId), Messages.ProductsListed);
        }
    }
}
