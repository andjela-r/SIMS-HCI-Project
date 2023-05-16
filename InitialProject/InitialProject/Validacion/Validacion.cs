using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace InitialProject.Validacion
{
    public class MinMaxValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            //if (value is int)
            //{
                int d = (int)value;
                if (d < 1) return new ValidationResult(false, "value must be greater than 1.");
                if (d > 5) return new ValidationResult(false, "value must be less than 5.");
                return new ValidationResult(true, null);
           // }
            /*else
            {
                return new ValidationResult(false, "Unknown error occured.");
            }*/
        }
    }
}
