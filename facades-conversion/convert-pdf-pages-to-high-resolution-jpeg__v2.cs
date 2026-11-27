using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Input PDF file
        const string inputPdf = "input.pdf";

        // Output directory for JPEG images
        const string outputDir = "JpegPages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output folder exists
        Directory.CreateDirectory(outputDir);

        // Configure font substitutions (example: replace missing "MyFont" with "Arial")
        FontRepository.Substitutions.Add(new SimpleFontSubstitution("MyFont", "Arial"));

        // Load the PDF document
        Document pdfDocument = new Document(inputPdf);

        // Create a JPEG device with 300 DPI resolution and 100% quality (quality set via constructor)
        JpegDevice jpegDevice = new JpegDevice(new Resolution(300), 100);

        // Convert pages 1 and 2 to JPEG images with high‑resolution settings
        for (int pageNumber = 1; pageNumber <= 2 && pageNumber <= pdfDocument.Pages.Count; pageNumber++)
        {
            string outputPath = Path.Combine(outputDir, $"Page_{pageNumber}.jpg");
            jpegDevice.Process(pdfDocument.Pages[pageNumber], outputPath);
            Console.WriteLine($"Page {pageNumber} saved as JPEG: {outputPath}");
        }

        Console.WriteLine("Conversion completed.");
    }
}
