using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace AppManifestFixeroo
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] files = Directory.GetFiles(Directory.GetCurrentDirectory(), "appmanifest_*.acf");
            if (files.Length == 0)
                Console.WriteLine("No .acf files found in current dir.");
            foreach (var file in files)
            {
                String rawKv = File.ReadAllText(file);
                Match toDownload = Regex.Match(rawKv, @"""BytesToDownload""\s+""(\d+)""", RegexOptions.IgnoreCase);
                Match downloaded = Regex.Match(rawKv, @"""BytesDownloaded""\s+""(\d+)""", RegexOptions.IgnoreCase);
                if (toDownload.Groups[1].Value != downloaded.Groups[1].Value && Int32.Parse(downloaded.Groups[1].Value) > 0)
                {
                    Console.WriteLine(Path.GetFileName(file));
                }
            }
        }
    }
}
