using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Interceptors;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Aspects.Autofac.Validation
{
    public class ValidationAspect : MethodInterception
    {
        private Type _validatorType;
        public ValidationAspect(Type validatorType)
        {
            if (!typeof(IValidator).IsAssignableFrom(validatorType))
            {
                throw new System.Exception("Bu bir doğrulama sınıfı değildir");
            }

            _validatorType = validatorType;
        }
        protected override void OnBefore(IInvocation invocation)
        {
            var validator = (IValidator)Activator.CreateInstance(_validatorType);       //instance oluştruma işlemi (reflection)
            var entityType = _validatorType.BaseType.GetGenericArguments()[0];          //Validator type ın base type ının generic argumanın ilki
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType);  //ilgili metotun parametrelerini bulur, entity type eşit olanı yakalar
            foreach (var entity in entities)
            {
                ValidationTool.Validate(validator, entity);                             //ValidationTool kullanılarak validate işlemini yap                 
            }
        }
    }
}
