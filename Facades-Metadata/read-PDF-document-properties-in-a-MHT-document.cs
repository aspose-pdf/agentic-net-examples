using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string mhtPath = "input.mht";

        // Verify the MHT file exists
        if (!File.Exists(mhtPath))
        {
            Console.Error.WriteLine($"File not found: {mhtPath}");
            return;
        }

        // Load the MHT file into a PDF Document using MhtLoadOptions
        MhtLoadOptions loadOptions = new MhtLoadOptions();
        using (Document pdfDoc = new Document(mhtPath, loadOptions))
        {
            // Initialize the PdfFileInfo facade with the loaded document
            using (PdfFileInfo info = new PdfFileInfo(pdfDoc))
            {
                // Read and display standard PDF properties
                Console.WriteLine($"Title: {info.Title}");
                Console.WriteLine($"Author: {info.Author}");
                Console.WriteLine($"Subject: {info.Subject}");
                Console.WriteLine($"Keywords: {info.Keywords}");
                Console.WriteLine($"Creator: {info.Creator}");
                Console.WriteLine($"Producer: {info.Producer}");
                Console.WriteLine($"CreationDate: {info.CreationDate}");
                Console.WriteLine($"ModDate: {info.ModDate}");
                Console.WriteLine($"Number of pages: {info.NumberOfPages}");
                Console.WriteLine($"Is encrypted: {info.IsEncrypted}");
                Console.WriteLine($"PDF version: {info.GetPdfVersion()}");

                // Example of retrieving a custom meta property (if present)
                string customMeta = info.GetMetaInfo("CustomProperty");
                if (!string.IsNullOrEmpty(customMeta))
                {
                    Console.WriteLine($"CustomProperty: {customMeta}");
                }
            }
        }
    }
}