using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "sample.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF file with the PdfFileInfo facade to access its metadata
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPath))
        {
            // Standard document properties
            Console.WriteLine($"Title    : {pdfInfo.Title}");
            Console.WriteLine($"Author   : {pdfInfo.Author}");
            Console.WriteLine($"Subject  : {pdfInfo.Subject}");
            Console.WriteLine($"Keywords : {pdfInfo.Keywords}");
            Console.WriteLine($"Creator  : {pdfInfo.Creator}");
            Console.WriteLine($"Producer : {pdfInfo.Producer}");
            Console.WriteLine($"Created  : {pdfInfo.CreationDate}");
            Console.WriteLine($"Modified : {pdfInfo.ModDate}");
            Console.WriteLine($"Pages    : {pdfInfo.NumberOfPages}");
            Console.WriteLine($"Encrypted: {pdfInfo.IsEncrypted}");
            Console.WriteLine($"PDF version: {pdfInfo.GetPdfVersion()}");

            // Example of retrieving a custom metadata entry
            string customMeta = pdfInfo.GetMetaInfo("CustomProperty");
            if (!string.IsNullOrEmpty(customMeta))
            {
                Console.WriteLine($"CustomProperty: {customMeta}");
            }
        }
    }
}