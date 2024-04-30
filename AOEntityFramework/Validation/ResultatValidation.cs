using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOEntityFramework.Validation
{
    public abstract class ResultatValidation
    {
        public abstract List<string> ObtenirListeErreur();
        public abstract bool EstUnEchec();
        public abstract bool EstUnSucces();
    }
}
