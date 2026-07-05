using System;
using System.Collections.Generic;
using System.Linq;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        // Ensure the PDF file exists before processing
        if (!System.IO.File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // PdfFileInfo provides GetMetaInfo(string) to retrieve custom metadata values
        using (PdfFileInfo fileInfo = new PdfFileInfo(pdfPath))
        // PdfXmpMetadata gives access to the collection of custom metadata keys
        using (PdfXmpMetadata xmpMeta = new PdfXmpMetadata())
        {
            // Bind the XMP metadata facade to the same PDF document
            xmpMeta.BindPdf(pdfPath);

            // Retrieve all custom metadata keys, sort them alphabetically
            List<string> sortedKeys = xmpMeta.Keys
                                            .Cast<string>()
                                            .OrderBy(k => k, StringComparer.OrdinalIgnoreCase)
                                            .ToList();

            // Loop through each key, obtain its value via GetMetaInfo, and display
            foreach (string key in sortedKeys)
            {
                string value = fileInfo.GetMetaInfo(key);
                Console.WriteLine($"{key}: {value}");
            }
        }
    }
}