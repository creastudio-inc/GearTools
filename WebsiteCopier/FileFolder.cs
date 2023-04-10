using System;
using System.IO;
using System.Net;
using System.Text;
using WebsiteCopier.Entity.Layout;

namespace WebsiteCopier
{
    public static class FileFolder
    {
        public static void Folder(String subPath)
        {
            bool exists = System.IO.Directory.Exists(subPath);

            if (!exists)
                System.IO.Directory.CreateDirectory(subPath);
        }

        public static void File(String subPath, String contents)
        {
            bool exists = System.IO.File.Exists(subPath);

            if (!exists)
            {
                using (FileStream fs = System.IO.File.Create(subPath))
                {
                    Byte[] info = new UTF8Encoding(true).GetBytes(contents);
                    // Add some information to the file.
                    fs.Write(info, 0, info.Length);
                }
            }
        }

        public static void Filecontents(String path, String contents)
        {
            var subpath = path.Split('/');
            String sub = "";
            for (int i = 0; i < subpath.Length - 1; i++)
            {
                sub += subpath[i] + "/";
                Folder(WebsiteCopier.Tools.FolderTmp + "/" + sub);
            }
            using (var tw = new StreamWriter(WebsiteCopier.Tools.FolderTmp + "/" + path, false))
            {
                tw.WriteLine(contents);
            }
        }
        public static string ReadContents(String path)
        {
          return  System.IO.File.ReadAllText(WebsiteCopier.Tools.FolderTmp + "/" + path);

        }


        public static void downloadfile(String link, String path)
        {
            try
            {
                
                var subpath = path.Split('/');
                String sub = "";
                for (int i = 0; i < subpath.Length - 1; i++)
                {
                    sub += subpath[i] + "/";
                    Folder(WebsiteCopier.Tools.FolderTmp + "/" + sub);
                }
                var pathlocal = WebsiteCopier.Tools.FolderTmp + "\\" + path;
                using (WebClient wc = new WebClient())
                {
                    wc.DownloadFile(
                        // Param1 = Link of file
                        new System.Uri(link),
                       // Param2 = Path to save
                       pathlocal
                    );
                }
            }
            catch (Exception ex)
            {
                var aa = path;
                //throw ex;
            }
        }
    }
}