using System;
using System.Collections.Generic;
using System.Linq;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        // Ensure the PDF file exists
        if (!System.IO.File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Bind the PDF to PdfFileInfo for GetMetaInfo access
        using (PdfFileInfo fileInfo = new PdfFileInfo(pdfPath))
        // Bind the same PDF to PdfXmpMetadata to obtain the list of custom keys
        using (PdfXmpMetadata xmpMeta = new PdfXmpMetadata())
        {
            xmpMeta.BindPdf(pdfPath);

            // Retrieve all metadata keys from the XMP dictionary
            List<string> keys = xmpMeta.Keys.Cast<string>().ToList();

            // Sort keys alphabetically (case‑insensitive)
            keys.Sort(StringComparer.OrdinalIgnoreCase);

            // Display each key with its corresponding custom value using GetMetaInfo
            foreach (string key in keys)
            {
                string value = fileInfo.GetMetaInfo(key);
                Console.WriteLine($"{key}: {value}");
            }
        }
    }
}