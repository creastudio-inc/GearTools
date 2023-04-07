using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using WebsiteCopier.Entity;

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
                if(!resultat.Any(x=>x.className == input.className && x.typeName == input.typeName))
                    resultat.Add(input);
            }
            return resultat;
        }
    }
}
