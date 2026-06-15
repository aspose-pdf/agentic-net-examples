using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Bind the PDF file using PdfFileInfo (facade API)
        using (PdfFileInfo pdfInfo = new PdfFileInfo(pdfPath))
        {
            // Header is a Dictionary<string,string> that holds custom metadata (key=value)
            IDictionary<string, string> headerDict = pdfInfo.Header ?? new Dictionary<string, string>();

            // Extract all keys, sort them alphabetically (case‑insensitive)
            List<string> keys = headerDict.Keys.ToList();
            keys.Sort(StringComparer.OrdinalIgnoreCase);

            // Retrieve and display each custom metadata value using GetMetaInfo
            foreach (string key in keys)
            {
                string value = pdfInfo.GetMetaInfo(key);
                Console.WriteLine($"{key}: {value}");
            }
        }
    }
}
