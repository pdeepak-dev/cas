using System;
using System.Globalization;
using System.ComponentModel.DataAnnotations;

namespace CasSys.Application.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class MaximumAttribute : ValidationAttribute
    {
        public readonly int _maximumValue;

        public MaximumAttribute(int maximumValue)
            : base(errorMessage: "The {0} field value must be maximum {1}.")
        {
            this._maximumValue = maximumValue;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(CultureInfo.CurrentCulture, base.ErrorMessageString, name, _maximumValue);
        }

        public override bool IsValid(object value)
        {
            int intValue;
            if (value != null && int.TryParse(Convert.ToString(value), out intValue))
            {
                return (intValue <= _maximumValue);
            }

            return false;
        }
    }
}