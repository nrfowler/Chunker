using System;
using System.Collections;
using System.IO;
using System.Linq;
using HtmlAgilityPack;
namespace Chunker
{
    class Program
    {
        static string directory = "C://Users//Nathan//Desktop//";
        static string output = "C://Users//Nathan//Desktop//desktop//CulturalEnrichment//test";

        static void Main(string[] args)
        {
            ChunkAndRead();

        }

        private static void ChunkAndRead()
        {
            ChunkAllHtmOnDesktop();
            OpenRandomFile();
        }

        private static void OpenRandomFile()
        {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
            {
                FileName = GetRandomFile(),
                UseShellExecute = true,
                Verb = "open"
            });
        }

        private static string GetRandomFile()
        {
            string[] files = Directory.GetFiles(output);
            files = files.Where(x => x.Contains("txt")).ToArray();
            Random r = new Random();

            if (files.Length > 0)
            {
                int a = r.Next() % files.Length;
                return files[a];
            }
            else
                return directory;
        }

        private static void ChunkMe(string name)
        {
            string file = GetFile(name);
            string output2 = string.Concat(output, name, ".html");
            //string[] lines = File.ReadAllLines(file);
            //Console.WriteLine(string.Concat(lines));
            HtmlWeb web = new HtmlWeb();
            HtmlDocument document = web.Load(file);
            HtmlNode[] nodes = document.DocumentNode.SelectNodes("//p")
                .Where(x => !x.HasClass("information")).Where(x => x.InnerLength > 30).ToArray();
            using (StreamWriter fw = new StreamWriter(output2))
            {
                foreach (HtmlNode item in nodes)
                {
                    fw.WriteLine("<p>{0}</p>", item.InnerHtml);


                }
            }

            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
            {
                FileName = output,
                UseShellExecute = true,
                Verb = "open"
            });
        }
        private static void ChunkMe2(string file)
        {
            string output2 = string.Concat(output, file.Substring(file.LastIndexOf("//")));

            HtmlWeb web = new HtmlWeb();
            HtmlDocument document = web.Load(file);
            HtmlNode[] nodes = document.DocumentNode.SelectNodes("//p")
                .Where(x => !x.HasClass("information")).Where(x => x.InnerLength > 30).ToArray();
            using (StreamWriter fw = new StreamWriter(output2))
            {
                foreach (HtmlNode item in nodes)
                {
                    fw.WriteLine("<p>{0}</p>", item.InnerHtml);


                }
            }

            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
            {
                FileName = output,
                UseShellExecute = true,
                Verb = "open"
            });
        }
        private static string GetFile(string name)
        {
            
            string[] files = Directory.GetFiles(directory);
            foreach(var file in files)
            {
                if (file.Contains(name))
                    return file;
            }
            return "C://Users//Nathan//Desktop//input.html";
        }
        private static void ChunkAllHtmOnDesktop()
        {

            string[] files = Directory.GetFiles(directory);
            var htmFiles = new ArrayList();
            foreach (var file in files)
            {
                if (file.Contains("htm"))
                {
                    ChunkMe2(file);
                    File.Delete(file);
                }
            }

        }
    }
}
