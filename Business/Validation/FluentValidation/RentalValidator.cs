using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Validation.FluentValidation
{
    public class RentalValidator : AbstractValidator<Rental> 
    {
        public RentalValidator()
        {
            RuleFor(r => r.RentDate).NotEmpty();
        }
    }
}
