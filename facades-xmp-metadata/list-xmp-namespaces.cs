using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            xmp.BindPdf(inputPath);

            ICollection<string> keys = xmp.Keys;
            HashSet<string> prefixes = new HashSet<string>();

            foreach (string key in keys)
            {
                int colonIndex = key.IndexOf(':');
                if (colonIndex > 0)
                {
                    string prefix = key.Substring(0, colonIndex);
                    prefixes.Add(prefix);
                }
            }

            Console.WriteLine("XMP Namespaces present in the PDF:");
            foreach (string prefix in prefixes)
            {
                string uri = xmp.GetNamespaceURIByPrefix(prefix);
                Console.WriteLine($"{prefix}: {uri}");
            }
        }
    }
}