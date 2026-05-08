using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class PdfToPngConverter
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";

        // Output directory for PNG images
        const string outputDir = "OutputImages";

        // Validate input file
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document
        Document pdfDocument = new Document(inputPdf);

        // Define the resolution for the output images (e.g., 300 DPI)
        Resolution resolution = new Resolution(300);

        // Iterate through all pages and save each as a PNG file
        for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
        {
            string outputPath = Path.Combine(outputDir, $"page_{pageNumber}.png");
            using (FileStream imageStream = new FileStream(outputPath, FileMode.Create))
            {
                // PngDevice renders a page to a PNG image using the specified resolution
                PngDevice pngDevice = new PngDevice(resolution);
                pngDevice.Process(pdfDocument.Pages[pageNumber], imageStream);
            }
        }

        Console.WriteLine($"Conversion completed. PNG files are saved in '{outputDir}'.");
    }
}
