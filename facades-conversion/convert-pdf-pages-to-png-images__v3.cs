using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string outputDir = "Images";

        // Verify the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document (required for page access)
        using var pdfDocument = new Document(pdfPath);

        // Use PdfConverter only to obtain the page count (optional, demonstrates binding)
        int pageCount;
        var converter = new PdfConverter();
        converter.BindPdf(pdfDocument);
        pageCount = converter.PageCount; // 1‑based page count
        // No using block for PdfConverter – it does not implement IDisposable

        // Convert each page to a PNG image using the PngDevice with default settings
        for (int i = 1; i <= pageCount; i++)
        {
            string outputPath = Path.Combine(outputDir, $"page_{i}.png");
            // PngDevice is a concrete image device and does NOT implement IDisposable → no using block
            var pngDevice = new PngDevice(); // defaults: 300 DPI, no compression options needed
            pngDevice.Process(pdfDocument.Pages[i], outputPath);
            Console.WriteLine($"Page {i} saved as {outputPath}");
        }
    }
}
