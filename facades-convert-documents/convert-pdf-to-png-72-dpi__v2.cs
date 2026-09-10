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
        const string outputDir = "PngImages";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Verify the input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        Document pdfDocument = new Document(inputPdf);

        // Set the desired resolution (72 DPI)
        Resolution resolution = new Resolution(72);

        // PngDevice does NOT implement IDisposable, so instantiate it directly
        PngDevice pngDevice = new PngDevice(resolution);

        // Iterate through each page and convert to PNG using CropBox (default behavior respects CropBox)
        for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
        {
            string outputFile = Path.Combine(outputDir, $"page_{pageNumber}.png");

            // Convert the page to an image and save directly to file
            pngDevice.Process(pdfDocument.Pages[pageNumber], outputFile);
        }

        Console.WriteLine("PDF to PNG conversion completed.");
    }
}