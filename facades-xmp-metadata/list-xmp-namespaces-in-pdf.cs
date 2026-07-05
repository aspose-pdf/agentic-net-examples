using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

class XmpNamespaceLister
{
    static void Main()
    {
        const string inputPdf = "input.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Use PdfXmpMetadata facade to access XMP metadata
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            // Bind the PDF file
            xmp.BindPdf(inputPdf);

            // Collect distinct namespace prefixes from metadata keys (e.g., "dc:creator")
            HashSet<string> prefixes = new HashSet<string>(StringComparer.Ordinal);
            foreach (string key in xmp.Keys)
            {
                int colonPos = key.IndexOf(':');
                if (colonPos > 0)
                {
                    string prefix = key.Substring(0, colonPos);
                    prefixes.Add(prefix);
                }
            }

            // List each prefix with its corresponding namespace URI
            Console.WriteLine("XMP Namespaces found in the PDF:");
            foreach (string prefix in prefixes)
            {
                string uri = xmp.GetNamespaceURIByPrefix(prefix);
                Console.WriteLine($"{prefix} => {uri}");
            }

            // If no prefixes were found, indicate that the PDF has no XMP namespaces
            if (prefixes.Count == 0)
            {
                Console.WriteLine("No XMP namespaces detected.");
            }
        }
    }
}