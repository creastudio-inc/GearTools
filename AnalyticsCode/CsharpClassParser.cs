using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.Text;
using System.IO;

namespace AnalyticsCode
{
    public static class CsharpClassParser
    {
        public static CsharpClass Parse(string content)
        {
            var cls = new CsharpClass();
            string text = File.ReadAllText(content);
            var tree = CSharpSyntaxTree.ParseText(text);
            var members = tree.GetRoot().DescendantNodes().OfType<MemberDeclarationSyntax>();

            foreach (var member in members)
            {
                if (member is PropertyDeclarationSyntax property)
                {
                    cls.Properties.Add(new CsharpClass.CsharpProperty(
                         property.Identifier.ValueText,
                         property.Type.ToString())
                     );
                }

                if (member is NamespaceDeclarationSyntax namespaceDeclaration)
                {
                    cls.Namespace = namespaceDeclaration.Name.ToString();
                }

                if (member is ClassDeclarationSyntax classDeclaration)
                {
                    cls.Name = classDeclaration.Identifier.ValueText;

                    cls.PrimaryKeyType = FindPrimaryKeyType(classDeclaration);
                }

                //if (member is MethodDeclarationSyntax method)
                //{
                //    Console.WriteLine("Method: " + method.Identifier.ValueText);
                //}
            }


            return cls;
        }

        private static string FindPrimaryKeyType(ClassDeclarationSyntax classDeclaration)
        {
            if (classDeclaration == null)
            {
                return null;
            }

            if (classDeclaration.BaseList == null)
            {
                return null;
            }

            foreach (var baseClass in classDeclaration.BaseList.Types)
            {
                var match = Regex.Match(baseClass.Type.ToString(), @"<(.*?)>");
                if (match.Success)
                {
                    var primaryKey = match.Groups[1].Value;

                    //if (AppConsts.PrimaryKeyTypes.Any(x => x.Value == primaryKey))
                    //{
                    //    return primaryKey;
                    //}
                }
            }

            return null;
        }
    }
}
