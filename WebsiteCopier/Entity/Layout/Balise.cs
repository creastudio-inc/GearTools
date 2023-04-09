using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteCopier.Entity.Layout
{
    public class Balise
    {
        public int BaliseId { get; set; }
        public string Id { get; set; }
        public string ClassName { get; set; }
        public string type { get; set; }
        public int ParentId { get; set; }
    }
}
