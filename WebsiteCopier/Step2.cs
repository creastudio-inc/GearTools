using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using WebsiteCopier.Entity.Layout;

namespace WebsiteCopier
{
    public class Step2
    {
        public static string buildbodyContenu(LayoutPage layoutPage, List<Balise>[] balises, int index, int ParentId)
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
                        Html += buildbodyContenu(layoutPage, balises, next, balise.BaliseId) + Environment.NewLine;
                    }
                    Html += "</div>" + Environment.NewLine;
                }
                if (balise.type == "Container")
                {
                    Html += "@RenderBody()" + Environment.NewLine;
                }
                if (balise.type == "nav")
                {
                    Html += "@Html.Partial(\"_nav\")" + Environment.NewLine;
                }
                if (balise.type == "footer")
                {
                    Html += "@Html.Partial(\"_footer\")" + Environment.NewLine;
                }
                if (balise.type == "header")
                {
                    Html += "@Html.Partial(\"_header\")" + Environment.NewLine;
                }
            }

            return Html;
        }

        public static List<Balise>[] buildStructure(LayoutPage layoutPage, HtmlDocument document)
        {
            var liste = new List<Balise>[5];
            buildstructurechild(ref liste, layoutPage.header.xPath, document);
            buildstructurechild(ref liste, layoutPage.nav.xPath, document);
            buildstructurechild(ref liste, layoutPage.Container.xPath, document);
            buildstructurechild(ref liste, layoutPage.footer.xPath, document);
            return liste;
        }

        public static void buildstructurechild(ref List<Balise>[] liste, string xPath, HtmlDocument document)
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
                    if (!liste[item.i].Any(x => x.BaliseId == int.Parse(code) && x.type == type))
                    {
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
                        var child = liste[item.i].FirstOrDefault(x => x.BaliseId == int.Parse(prevcode) && x.type == type);
                        if (child != null)
                        {
                            child.ParentId = child.BaliseId;
                        }
                    }
                }
            }
        }

        static string GetAbsoluteUrlString(string url)
        {
            var uri = new Uri(url, UriKind.RelativeOrAbsolute);
            if (!uri.IsAbsoluteUri)
                uri = new Uri(new Uri(Tools.baseUrl), uri);
            return uri.ToString();
        }
        public static string Buildlink(List<Link> links)
        {

            String Html = "";
            foreach (var item in links)
            {
                if (!item.href.Contains("fonts.googleapis."))
                    FileFolder.downloadfile(GetAbsoluteUrlString(item.href), "Content/" + item.href);
                var href = !string.IsNullOrEmpty(item.href) ? "href=\"~/Content/" + item.href + "\"" : "";
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
                FileFolder.downloadfile(GetAbsoluteUrlString(item.src), "Content/" + item.src);
                var src = !string.IsNullOrEmpty(item.src) ? "src=\"~/Content/" + item.src + "\"" : "";
                Html += "<script " + src + "  ></script>" + Environment.NewLine;
            }
            return Html;
        }

        public static string BuildMeta(List<Meta> metas)
        {
            String Html = "";
            foreach (var item in metas)
            {
                var charset = !string.IsNullOrEmpty(item.charset) ? "charset=\"" + item.charset + "\"" : "";
                var httpequiv = !string.IsNullOrEmpty(item.httpequiv) ? "http-equiv=\"" + item.httpequiv + "\"" : "";
                var content = !string.IsNullOrEmpty(item.content) ? "content=\"" + item.content + "\"" : "";
                var name = !string.IsNullOrEmpty(item.name) ? "charset=\"" + item.name + "\"" : "";
                Html += "<meta " + charset + "  " + httpequiv + " " + name + " " + content + " >" + Environment.NewLine;
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
            Html += Header.innerHTml;
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
    }
}