using HtmlAgilityPack;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using WebsiteCopier.Entity;
using WebsiteCopier.Entity.Layout;
using Header = WebsiteCopier.Entity.Layout.Header;

namespace WebsiteCopier
{
    public class Tools
    {
        public static List<Form> RemplirForm(string url)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument document = web.Load(url);
            var resultat = new List<Form>();
            HtmlNode[] nodes = document.DocumentNode.SelectNodes("//form").ToArray();
            foreach (HtmlNode item in nodes)
            {
                var form = new Form();
                var className = item.Attributes.Where(x => x.Name == "class").ToList().Select(x => x.Value);
                form.className = String.Join(" ", className);
                resultat.Add(form);
            }
            return resultat;
        }

        public static List<Panel> RemplirPanel(string url)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument document = web.Load(url);
            var resultat = new List<Panel>();
            HtmlNode[] nodes = document.DocumentNode.SelectNodes("//panel").ToArray();
            foreach (HtmlNode item in nodes)
            {
                var panel = new Panel();
                var className = item.Attributes.Where(x => x.Name == "class").ToList().Select(x => x.Value);
                panel.className = String.Join(" ", className);
                resultat.Add(panel);
            }
            return resultat;
        }

        public static List<Select> RemplirSelect(string url)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument document = web.Load(url);
            var resultat = new List<Select>();
            HtmlNode[] nodes = document.DocumentNode.SelectNodes("//panel").ToArray();
            foreach (HtmlNode item in nodes)
            {
                var select = new Select();
                var className = item.Attributes.Where(x => x.Name == "class").ToList().Select(x => x.Value);
                select.className = String.Join(" ", className);
                resultat.Add(select);
            }
            return resultat;
        }

        public static List<Table> RemplirTable(string url)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument document = web.Load(url);
            var resultat = new List<Table>();
            HtmlNode[] nodes = document.DocumentNode.SelectNodes("//panel").ToArray();
            foreach (HtmlNode item in nodes)
            {
                var table = new Table();
                var className = item.Attributes.Where(x => x.Name == "class").ToList().Select(x => x.Value);
                table.className = String.Join(" ", className);
                resultat.Add(table);
            }
            return resultat;
        }

        public static List<Input> RemplirText(string url)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument document = web.Load(url);
            var resultat = new List<Input>();
            HtmlNode[] nodes = document.DocumentNode.SelectNodes("//input").ToArray();
            foreach (HtmlNode item in nodes)
            {
                var input = new Input();
                var className = item.Attributes.Where(x => x.Name == "class").ToList().Select(x => x.Value);
                input.className = String.Join(" ", className);
                var typeName = item.Attributes.Where(x => x.Name == "type").FirstOrDefault().Value;
                input.typeName = typeName;
                if (!resultat.Any(x => x.className == input.className && x.typeName == input.typeName))
                    resultat.Add(input);
            }
            return resultat;
        }

        public static (string Layout, string Header, string Nav, string Footer, string Script) RemplirLayout(string url,string wrapperClass)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument document = web.Load(url);
            var layoutPage = Init(document.DocumentNode, wrapperClass);

            string Layout = BuildPage(layoutPage, document);
            string Header = BuildHeader(layoutPage.header);
            string Nav = BuildNav(layoutPage.nav);
            string Script = BuildScript(layoutPage.scripts);
            string Footer = BuildFooter(layoutPage.footer);
            return (Layout, Header, Nav, Footer, Script)  ;
        }

        #region level 1

        public static LayoutPage Init(HtmlNode DocumentNode,string wrapperClass)
        {
            var layoutPage = new LayoutPage();
            layoutPage.links = DocumentNode.SelectNodes("//link").Select(x => RenderLink(x)).ToList();
            layoutPage.metas = DocumentNode.SelectNodes("//meta").Select(x => RenderMeta(x)).ToList();
            layoutPage.scripts = DocumentNode.SelectNodes("//script").Select(x => RenderScript(x)).ToList();
            layoutPage.header = DocumentNode.SelectNodes("//header").Select(x => RenderHeader(x)).FirstOrDefault();
            layoutPage.nav = DocumentNode.SelectNodes("//nav").Select(x => RendeNav(x)).FirstOrDefault();
            layoutPage.Container = DocumentNode.SelectNodes("//div[@class='"+ wrapperClass + "']").Select(x => RendeContainer(x)).FirstOrDefault();
            layoutPage.footer = DocumentNode.SelectNodes("//footer").Select(x => RenderFooter(x)).FirstOrDefault();
            return layoutPage;
        }

        public static Entity.Layout.Container RendeContainer(HtmlNode node)
        {
            var container = new Entity.Layout.Container();
            container.className = string.Join("", node.GetClasses());
            container.innerHTml = node.InnerHtml;
            container.xPath = node.XPath.Replace("/html[1]/body[1]/", "")+ "/Container[1]";
            return container;
        }

        public static Header RenderHeader(HtmlNode node)
        {
            var header = new Header();
            header.className = string.Join("", node.GetClasses());
            header.innerHTml = node.InnerHtml;
            header.xPath = node.XPath.Replace("/html[1]/body[1]/", "");
            return header;
        }

        public static Nav RendeNav(HtmlNode node)
        {
            var nav = new Nav();
            nav.className = string.Join("", node.GetClasses());
            nav.innerHTml = node.InnerHtml;
            nav.xPath = node.XPath.Replace("/html[1]/body[1]/", "");
            return nav;
        }

        private static Link RenderLink(HtmlNode node)
        {
            var link = new Link();
            link.href = node.GetAttributeValue("href", string.Empty);
            link.rel = node.GetAttributeValue("rel", string.Empty);
            link.type = node.GetAttributeValue("type", string.Empty);
            return link;
        }

        private static Meta RenderMeta(HtmlNode node)
        {
            var meta = new Meta();
            meta.content = node.GetAttributeValue("content", string.Empty);
            meta.httpequiv = node.GetAttributeValue("http-equiv", string.Empty);
            meta.name = node.GetAttributeValue("name", string.Empty);
            meta.charset = node.GetAttributeValue("charset", string.Empty);
            return meta;
        }

        private static Footer RenderFooter(HtmlNode node)
        {
            var footer = new Footer();
            footer.className = string.Join("", node.GetClasses());
            footer.innerHTml = node.InnerHtml;
            footer.xPath = node.XPath.Replace("/html[1]/body[1]/", "");
            return footer;
        }

        private static Script RenderScript(HtmlNode node)
        {
            var script = new Script();
            script.src = node.GetAttributeValue("src", string.Empty);
            return script;
        }

        #endregion level 1

        #region level 2

        public static string BuildPage(LayoutPage layoutPage, HtmlDocument document)
        {
            String Html = "";
            Html += "<!DOCTYPE html>   " + Environment.NewLine;
            Html += "<html>" + Environment.NewLine;
            Html += "<head>" + Environment.NewLine;
            Html += "<title> Dashboard</title>" + Environment.NewLine;
            Html += BuildMeta(layoutPage.metas) + Environment.NewLine;
            Html += Buildlink(layoutPage.links) + Environment.NewLine;
            Html += "@RenderSection(\"styles\", required: false)" + Environment.NewLine;
            Html += "</head>" + Environment.NewLine;
            Html += "<body> " + Environment.NewLine;

            var liste = buildStructure(layoutPage, document);

            Html += buildbodyContenu(layoutPage,liste, 0, 0) + Environment.NewLine;
            Html += "@Html.Partial(\"scripts\")" + Environment.NewLine;
            Html += "@RenderSection(\"scripts\", required: false)" + Environment.NewLine;
            Html += "</body>" + Environment.NewLine;
            Html += "</html>" + Environment.NewLine;
            return Html;
        }

        private static string buildbodyContenu(LayoutPage layoutPage, List<Balise>[] balises, int index, int ParentId)
        {
            String Html = "";

            foreach (var balise in balises[index].Where(x => x.ParentId == ParentId))
            {
                var next = index + 1;
                if (balise.type == "div")
                {
                    Html += "<div class='" + balise.ClassName + "'>" + Environment.NewLine;
                    if (balises[next].Where(x => x.ParentId == balise.BaliseId).Any())
                    {
                        Html += buildbodyContenu(layoutPage,balises, next, balise.BaliseId) + Environment.NewLine;
                    }
                    Html += "</div>" + Environment.NewLine;
                }
                if (balise.type == "Container")
                {
                    Html += "@RenderBody()" + Environment.NewLine;
                }
                if (balise.type == "nav")
                {
                    Html += "@Html.Partial(\"nav\")" + Environment.NewLine;
                }
                if (balise.type == "footer")
                {
                    Html += "@Html.Partial(\"footer\")" + Environment.NewLine;
                }
                if (balise.type == "header")
                {
                    Html += "@Html.Partial(\"header\")" + Environment.NewLine;
                }
            }

            return Html;
        }

        private static List<Balise>[] buildStructure(LayoutPage layoutPage, HtmlDocument document)
        {
            var liste = new List<Balise>[5];
            buildstructurechild(ref liste, layoutPage.header.xPath, document);
            buildstructurechild(ref liste, layoutPage.nav.xPath, document);
            buildstructurechild(ref liste, layoutPage.Container.xPath, document);
            buildstructurechild(ref liste, layoutPage.footer.xPath, document);
            return liste;
        }

        private static void buildstructurechild(ref List<Balise>[] liste, string xPath, HtmlDocument document)
        {
            var stringpath = "/html[1]/body[1]";
            var list = xPath.Split('/');
            foreach (var item in list.Select((value, i) => new { i, value }))
            {
                stringpath += "/" + item.value;
                var type = item.value.Split('[').First();
                var prevpath = list.ElementAtOrDefault(item.i - 1);
                string prevcode = prevpath != null ? prevpath.Substring(prevpath.IndexOf('[') + 1).Replace("]", "") : "0";
                var path = list.ElementAtOrDefault(item.i);
                string code = path != null ? path.Substring(path.IndexOf('[') + 1).Replace("]", "") : "0";
                if (liste.ElementAtOrDefault(item.i) == null)
                {
                    liste[item.i] = new List<Balise>();
                    if (type == "Container")
                    {
                        liste[item.i].Add(new Balise()
                        {
                            BaliseId = int.Parse(code),
                            ClassName = "",
                            type = type,
                            ParentId = int.Parse(prevcode)
                        });
                    }
                    else
                    {
                        var node = document.DocumentNode.SelectNodes(stringpath).ToList().Select(x => getClass(x)).ToList();
                        liste[item.i].Add(new Balise()
                        {
                            BaliseId = int.Parse(code),
                            ClassName = String.Join(" ", node.ToList()),
                            type = type,
                            ParentId = int.Parse(prevcode)
                        });
                    }
                }
                else
                {
                    if (!liste[item.i].Any(x => x.BaliseId == int.Parse(code) && x.type ==type))
                    {
                        if(type== "Container") {
                            liste[item.i].Add(new Balise()
                            {
                                BaliseId = int.Parse(code),
                                ClassName = "",
                                type = type,
                                ParentId = int.Parse(prevcode)
                            });
                        }
                        else
                        {
                            var node = document.DocumentNode.SelectNodes(stringpath).ToList().Select(x => getClass(x)).ToList();
                            liste[item.i].Add(new Balise()
                            {
                                BaliseId = int.Parse(code),
                                ClassName = String.Join(" ", node.ToList()),
                                type = type,
                                ParentId = int.Parse(prevcode)
                            });
                        }
                       
                    }
                    else
                    {
                        var child = liste[item.i].FirstOrDefault(x => x.BaliseId == int.Parse(prevcode) && x.type == type);
                        if (child != null)
                        {
                            child.ParentId = child.BaliseId;
                        }
                    }
                }
            }
        }

        private static string Buildlink(List<Link> links)
        {
            String Html = "";
            foreach (var item in links)
            {
                var href = !string.IsNullOrEmpty(item.href) ? "href=\"" + item.href + "\"" : "";
                var rel = !string.IsNullOrEmpty(item.rel) ? "rel=\"" + item.rel + "\"" : "";
                var type = !string.IsNullOrEmpty(item.type) ? "type=\"" + item.type + "\"" : "";
                Html += "<link " + rel + " " + href + " " + type + " >" + Environment.NewLine;
            }
            return Html;
        }
        public static string BuildScript(List<Script> scripts)
        {
            String Html = "";
            foreach (var item in scripts)
            {
                var src = !string.IsNullOrEmpty(item.src) ? "src=\"" + item.src + "\"" : "";
                Html += "<script " + src + "  ></script>" + Environment.NewLine;
            }
            return Html;
        }

        private static string BuildMeta(List<Meta> metas)
        {
            String Html = "";
            foreach(var item in metas)
            {
                var charset = !string.IsNullOrEmpty(item.charset) ? "charset=\"" +item.charset+ "\"" : "";
                var httpequiv = !string.IsNullOrEmpty(item.httpequiv) ? "http-equiv=\"" + item.httpequiv+ "\"" : "";
                var content = !string.IsNullOrEmpty(item.content) ? "content=\"" + item.content + "\"" : "";
                var name = !string.IsNullOrEmpty(item.name) ? "charset=\"" + item.name+ "\"" : "";
                Html += "<meta "+ charset + "  "+ httpequiv + " "+ name + " "+ content + " >" + Environment.NewLine;
            }
            return Html;
        }

        public static string getClass(HtmlNode node)
        {
            return string.Join(" ", node.GetClasses());
        }

     
        public static string BuildHeader(Header Header)
        {
            String Html = "";
            Html +=Header.innerHTml;
            return Html;
        }
        public static string BuildNav(Nav nav)
        {
            String Html = "";

            Html += nav.innerHTml;
            return Html;
        }

        public static string BuildFooter(Footer footer)
        {
            String Html = "";

            Html += footer.innerHTml;
            return Html;
        }

  

        #endregion level 2
    }
}