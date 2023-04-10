using HtmlAgilityPack;
using System;
using WebsiteCopier.Entity.Layout;

namespace WebsiteCopier
{
    public static class Step1
    {
        public static Entity.Layout.Container RendeContainer(HtmlNode node)
        {
            var container = new Entity.Layout.Container();
            container.className = string.Join(" ", node.GetClasses());
            container.innerHTml = node.InnerHtml;
            container.xPath = node.XPath.Replace("/html[1]/body[1]/", "") + "/Container[1]";
            return container;
        }

        public static Header RenderHeader(HtmlNode node)
        {
            var header = new Header();
            header.className = string.Join(" ", node.GetClasses());
            header.innerHTml = node.InnerHtml;
            header.xPath = node.XPath.Replace("/html[1]/body[1]/", "");
            return header;
        }

        public static Nav RendeNav(HtmlNode node)
        {
            var nav = new Nav();
            nav.className = string.Join(" ", node.GetClasses());
            nav.innerHTml = node.InnerHtml;
            nav.xPath = node.XPath.Replace("/html[1]/body[1]/", "");
            return nav;
        }

        public static Link RenderLink(HtmlNode node)
        {
            var link = new Link();
            link.href = node.GetAttributeValue("href", string.Empty);
            link.rel = node.GetAttributeValue("rel", string.Empty);
            link.type = node.GetAttributeValue("type", string.Empty);
            return link;
        }


        public static Meta RenderMeta(HtmlNode node)
        {
            var meta = new Meta();
            meta.content = node.GetAttributeValue("content", string.Empty);
            meta.httpequiv = node.GetAttributeValue("http-equiv", string.Empty);
            meta.name = node.GetAttributeValue("name", string.Empty);
            meta.charset = node.GetAttributeValue("charset", string.Empty);
            return meta;
        }

        public static Footer RenderFooter(HtmlNode node)
        {
            var footer = new Footer();
            footer.className = string.Join(" ", node.GetClasses());
            footer.innerHTml = node.InnerHtml;
            footer.xPath = node.XPath.Replace("/html[1]/body[1]/", "");
            return footer;
        }

        public static Script RenderScript(HtmlNode node)
        {
            var script = new Script();
            script.src = node.GetAttributeValue("src", string.Empty);
            return script;
        }
    }
}