using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "BmpPages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document
        Document pdfDocument = new Document(inputPdfPath);

        // Define the resolution (dpi) for the output BMP images
        Resolution resolution = new Resolution(300);

        // Convert each page to a BMP image. The device uses the page's CropBox by default,
        // so margins are trimmed automatically – no explicit PageSize property is required.
        for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
        {
            // Create a BMP device; it works cross‑platform and does not depend on System.Drawing
            BmpDevice bmpDevice = new BmpDevice(resolution);

            string outputPath = Path.Combine(outputFolder, $"page_{pageNumber}.bmp");

            // Render the page to the BMP file
            bmpDevice.Process(pdfDocument.Pages[pageNumber], outputPath);

            Console.WriteLine($"Saved page {pageNumber} as BMP: {outputPath}");
        }

        Console.WriteLine("PDF to BMP conversion completed.");
    }
}
