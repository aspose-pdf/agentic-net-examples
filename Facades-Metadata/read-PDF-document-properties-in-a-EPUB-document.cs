using System;
using System.IO;
using Aspose.Pdf;                     // Core PDF API
using Aspose.Pdf.Facades;            // Facades API (PdfFileInfo, etc.)

class Program
{
    static void Main()
    {
        // Paths to the input EPUB file and optional output (not needed for reading)
        const string epubPath = "input.epub";

        if (!File.Exists(epubPath))
        {
            Console.Error.WriteLine($"File not found: {epubPath}");
            return;
        }

        // Load the EPUB file as a PDF document using EpubLoadOptions
        using (Document pdfDoc = new Document(epubPath, new EpubLoadOptions()))
        {
            // Initialize PdfFileInfo on the loaded document to access PDF metadata
            using (PdfFileInfo info = new PdfFileInfo(pdfDoc))
            {
                // Standard PDF properties
                Console.WriteLine($"Title          : {info.Title}");
                Console.WriteLine($"Author         : {info.Author}");
                Console.WriteLine($"Creator        : {info.Creator}");
                Console.WriteLine($"Subject        : {info.Subject}");
                Console.WriteLine($"Keywords       : {info.Keywords}");
                Console.WriteLine($"CreationDate   : {info.CreationDate}");
                Console.WriteLine($"ModDate        : {info.ModDate}");
                Console.WriteLine($"Producer       : {info.Producer}");
                Console.WriteLine($"Number of Pages: {info.NumberOfPages}");
                Console.WriteLine($"PDF Version    : {info.GetPdfVersion()}");
                Console.WriteLine($"Is Encrypted   : {info.IsEncrypted}");
                Console.WriteLine($"Has Open Password: {info.HasOpenPassword}");
                Console.WriteLine($"Has Edit Password: {info.HasEditPassword}");

                // Example of retrieving a custom metadata entry (if any)
                string customMetaKey = "CustomProperty";
                string customMetaValue = info.GetMetaInfo(customMetaKey);
                if (!string.IsNullOrEmpty(customMetaValue))
                {
                    Console.WriteLine($"{customMetaKey}: {customMetaValue}");
                }
            }
        }
    }
}