using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices; // for PngDevice and Resolution

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "OutputImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document
        Document pdfDocument = new Document(inputPdf);

        // Set the desired resolution (72 DPI)
        var resolution = new Resolution(72);
        // Create a PNG device with the specified resolution
        var pngDevice = new PngDevice(resolution);

        // Iterate through all pages and convert each to a PNG image
        for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
        {
            string outputPath = Path.Combine(outputDir, $"image{pageNumber}.png");

            // Convert the current page to PNG and write directly to file
            using (FileStream imageStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                pngDevice.Process(pdfDocument.Pages[pageNumber], imageStream);
            }
        }

        Console.WriteLine($"PDF pages have been converted to PNG images in '{outputDir}'.");
    }
}
