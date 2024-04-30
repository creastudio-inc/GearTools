using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOEntityFramework.Validation
{
    public abstract class ValidationBase<T> where T : class

    {
        public abstract ResultatValidation Valider(T intrant);
    }
}
