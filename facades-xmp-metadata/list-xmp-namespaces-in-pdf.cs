using System;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class XmpNamespaceLister
{
    static void Main()
    {
        const string inputPdf = "input.pdf";

        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load XMP metadata facade for the PDF
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            xmp.BindPdf(inputPdf);

            // Collect unique namespace prefixes from metadata keys (e.g., "dc:creator")
            HashSet<string> prefixes = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (string key in xmp.Keys)
            {
                int colonIndex = key.IndexOf(':');
                if (colonIndex > 0)
                {
                    string prefix = key.Substring(0, colonIndex);
                    prefixes.Add(prefix);
                }
            }

            // List each prefix with its corresponding namespace URI
            Console.WriteLine("XMP Namespaces found in the PDF:");
            foreach (string prefix in prefixes)
            {
                try
                {
                    string uri = xmp.GetNamespaceURIByPrefix(prefix);
                    Console.WriteLine($"Prefix: {prefix}  URI: {uri}");
                }
                catch (Exception ex)
                {
                    // If the prefix is not registered, GetNamespaceURIByPrefix may throw
                    Console.WriteLine($"Prefix: {prefix}  URI: (not registered)  Error: {ex.Message}");
                }
            }
        }
    }
}