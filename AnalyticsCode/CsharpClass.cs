using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalyticsCode
{
    public class CsharpClass
    {
        public string Name { get; set; }

        public string Namespace { get; set; }

        public List<CsharpProperty> Properties { get; set; }

        public string PrimaryKeyType { get; set; }

        public class CsharpProperty
        {
            public string Name { get; set; }
            public string Type { get; set; }

            public CsharpProperty(string name, string type)
            {
                Name = name;
                Type = type;
            }
        }

        public CsharpClass()
        {
            Properties = new List<CsharpProperty>();
        }
    }
}
