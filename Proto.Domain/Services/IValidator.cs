using System;
using System.ComponentModel.DataAnnotations;
using SimpleInjector;

namespace Proto.Domain.Services
{
    public interface IValidator
    {
        /// <summary>Validates the given instance.</summary>
        /// <param name="instance">The instance to validate.</param>
        /// <exception cref="ArgumentNullException">Thrown when the instance is a null reference.</exception>
        /// <exception cref="ValidationException">Thrown when the instance is invalid.</exception>
        void ValidateObject(object instance);
    }


    public class DataAnnotationsValidator : IValidator
    {
        private readonly IServiceProvider container;

        public DataAnnotationsValidator(Container container)
        {
            this.container = container;
        }

        void IValidator.ValidateObject(object instance)
        {
            var context = new ValidationContext(instance,
                this.container, null);

            // Throws an exception when instance is invalid.
            Validator.ValidateObject(instance, context);
        }
    }

}