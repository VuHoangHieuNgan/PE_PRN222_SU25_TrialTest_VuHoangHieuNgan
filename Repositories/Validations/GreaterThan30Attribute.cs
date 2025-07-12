using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Validations
{
    public class GreaterThan30Attribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is double doubleValue)
            {
                return doubleValue > 30;
            }
            return false;
        }
    }
}
