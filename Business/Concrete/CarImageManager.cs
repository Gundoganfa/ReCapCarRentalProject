using Business.Abstract;
using Business.Constants;
using Core.Utilities.Helpers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        ICarImageDal _carImageDal;
        
        public CarImageManager(ICarImageDal carImageDal)
        {

            _carImageDal = carImageDal;
        }

        public IResult Add(IFormFile file, CarImage carImage)
        {
            carImage.ImagePath = FileHelper.Add(file);
            var result = CheckCarImageCountLimit(carImage, 5);

            if ((!result.Success) || (carImage.ImagePath==null))
            {
                return new ErrorResult();
            }

            carImage.Date = DateTime.Now;
            _carImageDal.Add(carImage);
            return new SuccessResult();
        }

        public IResult Delete(CarImage carImage)
        {
            FileHelper.Delete(carImage.ImagePath);
            _carImageDal.Delete(carImage);
            return new SuccessResult();
        }

        public IDataResult<CarImage> GetById(int carImageId)
        {
            return new SuccessDataResult<CarImage>(_carImageDal.Get(c => c.CarImageId == carImageId));
        }

        public IDataResult<List<CarImage>> GetAll()
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll(), Messages.ImagesListed);
        }

        public IDataResult<List<CarImage>> GetImagesByCarId(int carId)
        {
            List<CarImage> carImages = _carImageDal.GetAll(c => c.CarId == carId);
            if (carImages.Count < 1)
            {
                carImages.Add(_carImageDal.Get(ci => ci.ImagePath.Contains("default")));
            }

            return new SuccessDataResult<List<CarImage>>(carImages, Messages.ImagesListed);
        }

        public IResult Update(IFormFile file, CarImage carImage)
        {
            carImage.ImagePath = FileHelper.Update(_carImageDal.Get(p => p.CarImageId == carImage.CarImageId).ImagePath, file);
            carImage.Date = DateTime.Now;
            _carImageDal.Update(carImage);
            return new SuccessResult();
        }

        private IResult CheckCarImageCountLimit(CarImage carImageToAdd, int maxCountOfImagesForACar)
        {
            var imageCount = _carImageDal.GetAll(ci => ci.CarId == carImageToAdd.CarId).Count;

            if (imageCount >= maxCountOfImagesForACar)
            {
                return new ErrorResult(Messages.ImageCapacityForACarReached);
            }
            return new SuccessResult();
        }
    }
}
