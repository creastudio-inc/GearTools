using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOEntityFramework.Validation
{
    internal class ValidationReussie : ResultatValidation
    {
        public override bool EstUnEchec()
        {
            return false;
        }

        public override bool EstUnSucces()
        {
           return true;
        }

        public override List<string> ObtenirListeErreur()
        {
           return null;
        }
    }
}
