using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices; // for Resolution and PngDevice

class Program
{
    static void Main()
    {
        // Path to the source PDF file
        const string inputPdfPath = "input.pdf";

        // Directory where the page images will be saved
        const string outputImageFolder = "PageImages";

        // Verify that the input PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{inputPdfPath}'.");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputImageFolder);

        // Load the PDF document
        Document pdfDocument = new Document(inputPdfPath);

        // Define the resolution for the output images (e.g., 300 DPI)
        var resolution = new Resolution(300);

        // Convert each page to an image using PngDevice (Aspose.Pdf.Devices)
        for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
        {
            var pngDevice = new PngDevice(resolution);
            string outPath = Path.Combine(outputImageFolder, $"page_{pageNumber}.png");
            pngDevice.Process(pdfDocument.Pages[pageNumber], outPath);
        }

        Console.WriteLine($"All pages have been converted to images in folder: {outputImageFolder}");
    }
}
