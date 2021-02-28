using Business.Abstract;
using Business.Constants;
using Business.Validation.FluentValidation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        IRentalDal _rentalDal;
        RentalValidator _rentalValidator;

        public RentalManager(IRentalDal rentalDal)
        {
            _rentalDal = rentalDal;
            _rentalValidator = new RentalValidator();
        }

        public IResult Add(Rental rental)
        {
            Rental searchItem = new Rental();

            var validationResult = _rentalValidator.Validate(rental);
            if (!validationResult.IsValid)
            {
                return new ErrorResult(Messages.rentalValidation);
            }

            searchItem = _rentalDal.Get(r => r.CarId == rental.CarId);
            if ((searchItem==null) || (searchItem.ReturnDate != null))
            {
                _rentalDal.Add(rental);
                return new SuccessResult();
            } 
            return new ErrorResult();
        }

        public IResult EndRental(int rentalId)
        {
            Rental rental = new Rental();
            rental = _rentalDal.Get(r => r.RentalId == rentalId);
            if (rental.ReturnDate == null)
            {
                rental.ReturnDate = DateTime.Today;
                _rentalDal.Update(rental);
                Console.WriteLine("Araç teslim edildi");
                return new SuccessResult();
            } 
            else
            {
                Console.WriteLine("Bu araç daha önce teslim edilmiş, kiralama id'sini kontrol edin");
                return new ErrorResult();
            }
            
        }

        public IResult DeleteById(int rentalId)
        {
            _rentalDal.Delete(_rentalDal.Get(c => c.RentalId == rentalId));
            return new SuccessResult();
        }

        public IResult Update(Rental rental)
        {
            _rentalDal.Update(rental);
            return new SuccessResult();
        }

        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll());
        }

        public IDataResult<Rental> GetById(int rentalId)
        {
            return new SuccessDataResult<Rental>(_rentalDal.Get(c => c.RentalId == rentalId));
        }

        public IResult Delete(Rental rental)
        {
            _rentalDal.Delete(rental);
            return new SuccessResult();
        }

        public IDataResult<List<RentalDetailDto>> GetRentalDetails()
        {

            return new SuccessDataResult<List<RentalDetailDto>>(_rentalDal.GetRentalDetails());
        }
    }
}
