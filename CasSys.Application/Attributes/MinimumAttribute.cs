using System;
using System.Globalization;
using System.ComponentModel.DataAnnotations;

namespace CasSys.Application.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class MinimumAttribute : ValidationAttribute
    {
        private readonly int _minimumValue;

        public MinimumAttribute(int minimumValue)
            : base(errorMessage: "The {0} field value must be minimum {1}.")
        {
            this._minimumValue = minimumValue;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(CultureInfo.CurrentCulture, base.ErrorMessageString, name, _minimumValue);
        }

        public override bool IsValid(object value)
        {
            int intValue;
            if (value != null && int.TryParse(Convert.ToString(value), out intValue))
            {
                return (intValue >= _minimumValue);
            }

            return false;
        }
    }
}