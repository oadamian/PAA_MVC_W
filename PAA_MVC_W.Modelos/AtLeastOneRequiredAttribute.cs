using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAA_MVC_W.Modelos
{
    public class AtLeastOneRequiredAttribute : ValidationAttribute
    {
        private readonly string[] _propertyNames;

        public AtLeastOneRequiredAttribute(params string[] propertyNames)
        {
            _propertyNames = propertyNames;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            foreach (var propertyName in _propertyNames)
            {
                var propertyInfo = validationContext.ObjectType.GetProperty(propertyName);
                if (propertyInfo == null)
                {
                    return new ValidationResult($"Unknown property: {propertyName}");
                }

                var propertyValue = propertyInfo.GetValue(validationContext.ObjectInstance, null);

                if (propertyValue != null && (DateTime?)propertyValue != null)
                {
                    return ValidationResult.Success;
                }
            }

            return new ValidationResult(ErrorMessage ?? "At least one of these fields must be filled.");
        }
    }
}
