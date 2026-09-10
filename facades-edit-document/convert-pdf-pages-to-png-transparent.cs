using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Input PDF file
        const string inputPdf = "input.pdf";

        // Folder where PNG images will be saved
        const string outputFolder = "OutputImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document
        Document pdfDocument = new Document(inputPdf);

        // Define the resolution (200 DPI)
        var resolution = new Resolution(200);

        // Create a PNG device with the required resolution and transparent background
        var pngDevice = new PngDevice(resolution)
        {
            // Enable transparent background for PNG output
            TransparentBackground = true
        };

        // Convert pages 2 through 4 (1‑based indexing) to PNG images
        for (int pageNumber = 2; pageNumber <= 4 && pageNumber <= pdfDocument.Pages.Count; pageNumber++)
        {
            string outputPath = Path.Combine(outputFolder, $"page_{pageNumber}.png");
            // Use the device's Process method to render the page to a PNG file
            pngDevice.Process(pdfDocument.Pages[pageNumber], outputPath);
        }

        Console.WriteLine("Selected pages have been converted to PNG images.");
    }
}
