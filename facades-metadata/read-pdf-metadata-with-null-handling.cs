using System;
using System.IO;
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

        // PdfFileInfo provides GetMetaInfo for reading PDF metadata.
        PdfFileInfo pdfInfo = new PdfFileInfo(pdfPath);

        // List of standard metadata keys we want to read.
        string[] metaKeys = new[]
        {
            "Title",
            "Author",
            "Subject",
            "Keywords",
            "Creator",
            "Producer",
            "CreationDate",
            "ModDate"
        };

        Console.WriteLine("PDF Metadata:");
        foreach (string key in metaKeys)
        {
            // GetMetaInfo returns null if the key is absent.
            string value = pdfInfo.GetMetaInfo(key);

            // Gracefully handle null or empty values.
            if (string.IsNullOrEmpty(value))
                value = "(not set)";

            Console.WriteLine($"{key}: {value}");
        }
    }
}