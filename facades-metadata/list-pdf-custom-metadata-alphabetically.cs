using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize PdfFileInfo and bind the PDF document
        using (PdfFileInfo pdfInfo = new PdfFileInfo())
        {
            pdfInfo.BindPdf(inputPath);

            // Use PdfXmpMetadata to obtain all metadata keys
            using (PdfXmpMetadata xmp = new PdfXmpMetadata())
            {
                xmp.BindPdf(inputPath);
                List<string> keys = new List<string>(xmp.Keys);
                keys.Sort(StringComparer.OrdinalIgnoreCase); // alphabetical order

                // Loop through keys, retrieve each custom value via GetMetaInfo, and display
                foreach (string key in keys)
                {
                    string value = pdfInfo.GetMetaInfo(key);
                    Console.WriteLine($"{key}: {value}");
                }
            }
        }
    }
}