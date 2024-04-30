using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOEntityFramework.Validation
{
    public class ValidationEchoue : ResultatValidation
    {
        private readonly List<string> _listeerreurmessage;
        public ValidationEchoue(string erreurmessage) {
            _listeerreurmessage = new List<string>()
            {
                erreurmessage
            };
        }

        public override bool EstUnEchec()
        {
            return true;
        }

        public override bool EstUnSucces()
        {
            return false;
        }

        public override List<string> ObtenirListeErreur()
        {
            return _listeerreurmessage;
        }
    }
}
