using System.Collections.Generic;

namespace WebsiteCopier.Entity.Layout
{
    public class LayoutPage
    {
        public Container Container { get; set; }

        public List<Link> links { get; set; }
        public List<Meta> metas { get; set; }
        public List<Script> scripts { get; set; }
        public Header header { get; set; }
        public Nav nav { get; set; }
        public Footer footer { get; set; }
    }

    public class Path
    {
        public string Parent { get; set; }
        public Path Child { get; set; }
        public List<string> ChildString { get; set; }
    }
}