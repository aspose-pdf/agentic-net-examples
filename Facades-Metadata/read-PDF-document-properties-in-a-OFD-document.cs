using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputOfd = "input.ofd";

        if (!File.Exists(inputOfd))
        {
            Console.Error.WriteLine($"File not found: {inputOfd}");
            return;
        }

        // Load the OFD file as a PDF document using OfdLoadOptions
        using (Document doc = new Document(inputOfd, new OfdLoadOptions()))
        {
            // Initialize the PdfFileInfo facade with the loaded document
            using (PdfFileInfo info = new PdfFileInfo(doc))
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
                Console.WriteLine($"IsEncrypted: {info.IsEncrypted}");
                Console.WriteLine($"PDF version: {info.GetPdfVersion()}");

                // Example of retrieving a custom metadata property
                string customMeta = info.GetMetaInfo("CustomProperty");
                if (!string.IsNullOrEmpty(customMeta))
                {
                    Console.WriteLine($"CustomProperty: {customMeta}");
                }
            }
        }
    }
}