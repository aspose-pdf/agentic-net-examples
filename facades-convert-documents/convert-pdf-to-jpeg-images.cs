using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string outputDir = "OutputImages";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document
        Document pdfDocument = new Document(pdfPath);
        int pageCount = pdfDocument.Pages.Count;

        // Define the resolution – using 96 DPI keeps the original page size.
        // Adjust if a different DPI is required.
        Resolution resolution = new Resolution(96);
        int jpegQuality = 100; // 0‑100, where 100 is best quality

        for (int pageNumber = 1; pageNumber <= pageCount; pageNumber++)
        {
            // JpegDevice does NOT implement IDisposable, so do NOT wrap it in a using statement.
            JpegDevice jpegDevice = new JpegDevice(resolution, jpegQuality);
            string outPath = Path.Combine(outputDir, $"Page_{pageNumber}.jpg");
            // Convert the page to JPEG preserving original dimensions and color depth
            jpegDevice.Process(pdfDocument.Pages[pageNumber], outPath);
        }

        Console.WriteLine($"PDF converted to JPEG images in '{outputDir}'.");
    }
}
