using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";

        // Ensure the PDF file exists before processing
        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Use PdfFileInfo facade to read metadata; wrap in using for deterministic disposal
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPdf))
        {
            // List of metadata keys to retrieve (standard or custom)
            string[] metaKeys = { "Title", "Author", "Subject", "CustomKey1", "CustomKey2" };

            foreach (string key in metaKeys)
            {
                // GetMetaInfo returns an empty string if the key does not exist
                string value = pdfInfo.GetMetaInfo(key);

                // Gracefully handle null or empty values
                if (string.IsNullOrEmpty(value))
                {
                    Console.WriteLine($"{key}: <empty>");
                }
                else
                {
                    Console.WriteLine($"{key}: {value}");
                }
            }
        }
    }
}