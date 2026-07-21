using System;
using System.IO;
using Aspose.Pdf;               // Document, Page
using Aspose.Pdf.Devices;      // Resolution, PngDevice

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";          // source PDF file
        const string outputDir = "output_images";    // folder for PNG files

        // Verify source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document
        Document pdfDocument = new Document(inputPdf);

        // Desired resolution (72 DPI)
        Resolution resolution = new Resolution(72);

        // PngDevice does NOT implement IDisposable, so instantiate without using
        PngDevice pngDevice = new PngDevice(resolution);

        // Convert each page to a PNG image using the CropBox (default behaviour)
        for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
        {
            // Output stream for the image
            using (MemoryStream imageStream = new MemoryStream())
            {
                // Convert the current page to PNG
                pngDevice.Process(pdfDocument.Pages[pageNumber], imageStream);

                // Write the PNG file to disk
                string outputPath = Path.Combine(outputDir, $"page_{pageNumber}.png");
                File.WriteAllBytes(outputPath, imageStream.ToArray());
            }
        }

        Console.WriteLine("PDF to PNG conversion completed.");
    }
}
