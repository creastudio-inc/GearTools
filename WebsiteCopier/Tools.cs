using GearTools.Util;
using HtmlAgilityPack;
using Microsoft.AspNet.SignalR;
using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading;
using WebsiteCopier.Entity.Layout;
using static System.Net.WebRequestMethods;
using File = System.IO.File;

namespace WebsiteCopier
{
    public class Tools
    {
        public static string FolderTmp { get; set; }
        public static string FolderZip { get; set; }
        public static string baseUrl { get; set; }

        public static void RemplirLayout(string url, string wrapperClass, IHubContext hubContext)
        {
            var itemsCount = 15;
            var i = 1;
            Functions.hubContext = hubContext;
            Functions.SendProgress("Process in progress..." , i++, itemsCount);
            Functions.SendLogMessage("Begin started" + DateTime.Now);
            HtmlWeb web = new HtmlWeb();
            HtmlDocument document = web.Load(url);
            Functions.SendLogMessage("End started" + DateTime.Now);
            Functions.SendProgress("Process in progress...", i++, itemsCount);

            Functions.SendProgress("Process in progress...", i++, itemsCount);
            Functions.SendLogMessage("Begin layoutPage" + DateTime.Now);
            var layoutPage = Init(document.DocumentNode, wrapperClass);
            Functions.SendLogMessage("End layoutPage" + DateTime.Now);
            Functions.SendProgress("Process in progress...", i++, itemsCount);

            Functions.SendProgress("Process in progress...", i++, itemsCount);
            Functions.SendLogMessage("Begin Layout" + DateTime.Now);
            string Layout = BuildPage(layoutPage, document);
            FileFolder.Filecontents("Views/Shared/_Layout.cshtml", Layout);
            Functions.SendLogMessage("End Layout" + DateTime.Now);
            Functions.SendProgress("Process in progress...", i++, itemsCount);

            Functions.SendProgress("Process in progress...", i++, itemsCount);
            Functions.SendLogMessage("Begin Header" + DateTime.Now);
            string Header = Step2.BuildHeader(layoutPage.header);
            FileFolder.Filecontents("Views/Shared/Partial/_header.cshtml", Header);
            Functions.SendLogMessage("End Header" + DateTime.Now);
            Functions.SendProgress("Process in progress...", i++, itemsCount);


            Functions.SendProgress("Process in progress...", i++, itemsCount);
            Functions.SendLogMessage("Begin Nav" + DateTime.Now);
            string Nav = Step2.BuildNav(layoutPage.nav);
            FileFolder.Filecontents("Views/Shared/Partial/_nav.cshtml", Nav);
            Functions.SendLogMessage("End Nav" + DateTime.Now);
            Functions.SendProgress("Process in progress...", i++, itemsCount);

            Functions.SendProgress("Process in progress...", i++, itemsCount);
            Functions.SendLogMessage("Begin Script" + DateTime.Now);
            string Script = Step2.BuildScript(layoutPage.scripts);
            FileFolder.Filecontents("Views/Shared/Partial/_script.cshtml", Script);
            Functions.SendLogMessage("End Script" + DateTime.Now);
            Functions.SendProgress("Process in progress...", i++, itemsCount);

            Functions.SendProgress("Process in progress...", i++, itemsCount);
            Functions.SendLogMessage("Begin Footer" + DateTime.Now);
            string Footer = Step2.BuildFooter(layoutPage.footer);
            FileFolder.Filecontents("Views/Shared/Partial/_footer.cshtml", Footer);
            Functions.SendLogMessage("End Footer" + DateTime.Now);
            Functions.SendProgress("Process in progress...", i++, itemsCount);


            var result = AddDirToZip();
            Functions.SendLogMessage("url : " + "https://localhost:44322/zip" + result);

            Functions.SendProgress("url : " + "https://localhost:44322/zip" + result, i++, itemsCount);

        }

        public static LayoutPage Init(HtmlNode DocumentNode, string wrapperClass)
        {
            var layoutPage = new LayoutPage();

            layoutPage.links = DocumentNode.SelectNodes("//link").Select(x => Step1.RenderLink(x)).ToList(); 
            Functions.SendLogMessage("get all links" + DateTime.Now);
            
            layoutPage.metas = DocumentNode.SelectNodes("//meta").Select(x => Step1.RenderMeta(x)).ToList();
            Functions.SendLogMessage("get all metas" + DateTime.Now);

            layoutPage.scripts = DocumentNode.SelectNodes("//script").Select(x => Step1.RenderScript(x)).ToList();
            Functions.SendLogMessage("get all scripts" + DateTime.Now);

            layoutPage.header = DocumentNode.SelectNodes("//header").Select(x => Step1.RenderHeader(x)).FirstOrDefault();
            Functions.SendLogMessage("get header" + DateTime.Now);

            layoutPage.nav = DocumentNode.SelectNodes("//nav").Select(x => Step1.RendeNav(x)).FirstOrDefault();
            Functions.SendLogMessage("get nav" + DateTime.Now);
            
            layoutPage.Container = DocumentNode.SelectNodes("//div[@class='" + wrapperClass + "']").Select(x => Step1.RendeContainer(x)).FirstOrDefault();
            Functions.SendLogMessage("get container" + DateTime.Now);

            layoutPage.footer = DocumentNode.SelectNodes("//footer").Select(x => Step1.RenderFooter(x)).FirstOrDefault();
            Functions.SendLogMessage("get footer" + DateTime.Now);
            
            return layoutPage;
        }

        public static string BuildPage(LayoutPage layoutPage, HtmlDocument document)
        {
            Functions.SendLogMessage("BuildPage" + DateTime.Now.Second);

            String Html = "";
            Html += "<!DOCTYPE html>   " + Environment.NewLine;
            Html += "<html>" + Environment.NewLine;
            Html += "<head>" + Environment.NewLine;
            Html += "<title> Dashboard</title>" + Environment.NewLine;
            Html += Step2.BuildMeta(layoutPage.metas) + Environment.NewLine; 
            Functions.SendLogMessage("BuildMeta" + DateTime.Now.Second);

            Html += Step2.Buildlink(layoutPage.links) + Environment.NewLine;
            Functions.SendLogMessage("Buildlink" + DateTime.Now.Second);

            Step2.dowloadFileTff(layoutPage.links);
            Functions.SendLogMessage("dowloadFileTff" + DateTime.Now.Second);

            Html += "@RenderSection(\"styles\", required: false)" + Environment.NewLine;
            Html += "</head>" + Environment.NewLine;
            Html += "<body> " + Environment.NewLine;

            var liste = Step2.buildStructure(layoutPage, document);
            Functions.SendLogMessage("buildStructure" + DateTime.Now.Second);


            Html += Step2.buildbodyContenu(layoutPage, liste, 0, 0) + Environment.NewLine;
            Functions.SendLogMessage("buildbodyContenu" + DateTime.Now.Second);

            Html += "@Html.Partial(\"Partial/_script\")" + Environment.NewLine;
            Html += "@RenderSection(\"scripts\", required: false)" + Environment.NewLine;
            Html += "</body>" + Environment.NewLine;
            Html += "</html>" + Environment.NewLine;

            Functions.SendLogMessage("finir" + DateTime.Now.Second);

            return Html;
        }

        public static string AddDirToZip()
        {
            string zipName = "\\result-" + DateTime.Now.Year + "-" + DateTime.Now.Day + "-" + DateTime.Now.Second + ".zip";
            string InputDirectory = WebsiteCopier.Tools.FolderTmp;
            string OutputFilename = WebsiteCopier.Tools.FolderZip+ zipName;

            if (File.Exists(OutputFilename))
                File.Delete(OutputFilename);

            ZipFile.CreateFromDirectory(InputDirectory, OutputFilename, CompressionLevel.Fastest, false);

            System.IO.DirectoryInfo di = new DirectoryInfo(InputDirectory);

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                dir.Delete(true);
            }
            return zipName;
        }
    }
}