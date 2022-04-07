using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class CarValidator : AbstractValidator<Car>
    {
        public CarValidator()
        {
            RuleFor(c => c.CarDescription).NotEmpty();
            RuleFor(c => c.CarDescription).MinimumLength(15);
            RuleFor(c => c.SegmentId).NotEmpty();
            RuleFor(c => c.CarModelYear).GreaterThan(Convert.ToInt16(2015)); 
            RuleFor(c => c.CarModelYear).GreaterThanOrEqualTo(Convert.ToInt16(2020)).When(c => c.SegmentId == 1);

        }
    }
}
