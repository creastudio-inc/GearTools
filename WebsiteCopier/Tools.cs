using HtmlAgilityPack;
using System;
using System.Linq;
using WebsiteCopier.Entity.Layout;

namespace WebsiteCopier
{
    public class Tools
    {
        public static string FolderTmp { get; set; }
        public static string baseUrl { get; set; }

        public static (string Layout, string Header, string Nav, string Footer, string Script) RemplirLayout(string url, string wrapperClass)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument document = web.Load(url);
            var layoutPage = Init(document.DocumentNode, wrapperClass);

            string Layout = BuildPage(layoutPage, document);
            FileFolder.Filecontents("Views/Shared/_Layout.cshtml", Layout);

            string Header = Step2.BuildHeader(layoutPage.header);
            FileFolder.Filecontents("Views/Shared/Partial/_header.cshtml", Header);

            string Nav = Step2.BuildNav(layoutPage.nav);
            FileFolder.Filecontents("Views/Shared/Partial/_nav.cshtml", Nav);

            string Script = Step2.BuildScript(layoutPage.scripts);
            FileFolder.Filecontents("Views/Shared/Partial/_script.cshtml", Script);

            string Footer = Step2.BuildFooter(layoutPage.footer);
            FileFolder.Filecontents("Views/Shared/Partial/_footer.cshtml", Footer);

            return (Layout, Header, Nav, Footer, Script);
        }

        public static LayoutPage Init(HtmlNode DocumentNode, string wrapperClass)
        {
            var layoutPage = new LayoutPage();
            layoutPage.links = DocumentNode.SelectNodes("//link").Select(x => Step1.RenderLink(x)).ToList();
            layoutPage.metas = DocumentNode.SelectNodes("//meta").Select(x => Step1.RenderMeta(x)).ToList();
            layoutPage.scripts = DocumentNode.SelectNodes("//script").Select(x => Step1.RenderScript(x)).ToList();
            layoutPage.header = DocumentNode.SelectNodes("//header").Select(x => Step1.RenderHeader(x)).FirstOrDefault();
            layoutPage.nav = DocumentNode.SelectNodes("//nav").Select(x => Step1.RendeNav(x)).FirstOrDefault();
            layoutPage.Container = DocumentNode.SelectNodes("//div[@class='" + wrapperClass + "']").Select(x => Step1.RendeContainer(x)).FirstOrDefault();
            layoutPage.footer = DocumentNode.SelectNodes("//footer").Select(x => Step1.RenderFooter(x)).FirstOrDefault();
            return layoutPage;
        }

        public static string BuildPage(LayoutPage layoutPage, HtmlDocument document)
        {
            String Html = "";
            Html += "<!DOCTYPE html>   " + Environment.NewLine;
            Html += "<html>" + Environment.NewLine;
            Html += "<head>" + Environment.NewLine;
            Html += "<title> Dashboard</title>" + Environment.NewLine;
            Html += Step2.BuildMeta(layoutPage.metas) + Environment.NewLine;
            Html += Step2.Buildlink(layoutPage.links) + Environment.NewLine;
            Html += "@RenderSection(\"styles\", required: false)" + Environment.NewLine;
            Html += "</head>" + Environment.NewLine;
            Html += "<body> " + Environment.NewLine;

            var liste = Step2.buildStructure(layoutPage, document);

            Html += Step2.buildbodyContenu(layoutPage, liste, 0, 0) + Environment.NewLine;
            Html += "@Html.Partial(\"_script\")" + Environment.NewLine;
            Html += "@RenderSection(\"scripts\", required: false)" + Environment.NewLine;
            Html += "</body>" + Environment.NewLine;
            Html += "</html>" + Environment.NewLine;
            return Html;
        }
    }
}