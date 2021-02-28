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
            //defensive coding - aşağıdaki kodu hiç yazmasak da kod çalışır.
            if (!typeof(IValidator).IsAssignableFrom(validatorType))
            {
                throw new System.Exception("Bu bir doğrulama sınıfı değil");// AspectMessages.WrongValidationType);
            }

            _validatorType = validatorType;
        }
        protected override void OnBefore(IInvocation invocation)
        {
            // gelen tipte bir instance (validator) yaratmak için activator.CreateInstance kullanılır.
            var validator = (IValidator)Activator.CreateInstance(_validatorType);

            // Product manager içinden typeof(ProductValidator) ile çağırılmış ise _validatorType.BaseType;
            // productValidator.Basetype anlamına gelir. ProductValidator nasıl tanımlanıyordu ona bakalım...
            // public class ProductValidator:AbstractValidator<Product> şeklinde tanımlamışız.
            // Bu durumda _validatorType.BaseType => AbstractValidator<Product> olmuş oluyor. 
            // .GetGenericArguments()[0] PRoduct tipini yakalamış olduk.
            var entityType = _validatorType.BaseType.GetGenericArguments()[0];

            // Method'un argümanlarını gez, yukarıda yakalanan bir tip varsa onu yakala.
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType); //invocation method demek, methodun parametrelerini bul 

            // Validate etmek istediğim tipi içeren tüm entitilerde, ilgili tipi validate et.
            foreach (var entity in entities) // her parametreyi gez, validation toolu kullanarak valide et
            {
                ValidationTool.Validate(validator, entity);
            }
        }
    }
}
