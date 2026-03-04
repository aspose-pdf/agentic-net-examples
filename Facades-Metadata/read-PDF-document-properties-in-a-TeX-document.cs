using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "sample.pdf";

        // Verify that the PDF file exists before attempting to read it
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // PdfFileInfo is a facade that provides access to PDF meta‑information.
        // It implements IDisposable, so we wrap it in a using block for deterministic cleanup.
        using (PdfFileInfo info = new PdfFileInfo(pdfPath))
        {
            // Standard document properties
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

            // PDF version string (e.g., "1.7")
            Console.WriteLine($"PDF version: {info.GetPdfVersion()}");

            // Example of retrieving a custom metadata entry (returns empty string if not present)
            string customMeta = info.GetMetaInfo("CustomProperty");
            if (!string.IsNullOrEmpty(customMeta))
            {
                Console.WriteLine($"CustomProperty: {customMeta}");
            }
        }
    }
}