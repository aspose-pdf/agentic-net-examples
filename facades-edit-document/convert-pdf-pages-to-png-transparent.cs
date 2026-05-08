using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputDir = "output_images";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Set resolution to 200 DPI (Resolution class resides in Aspose.Pdf.Devices)
            Resolution resolution = new Resolution(200);

            // Initialize PNG device with transparent background
            PngDevice pngDevice = new PngDevice(resolution)
            {
                TransparentBackground = true
            };

            // Convert pages 2 through 4 (or up to the last page if fewer exist)
            for (int pageNumber = 2; pageNumber <= 4 && pageNumber <= pdfDoc.Pages.Count; pageNumber++)
            {
                string outputPath = Path.Combine(outputDir, $"page{pageNumber}.png");
                using (FileStream pngStream = new FileStream(outputPath, FileMode.Create))
                {
                    pngDevice.Process(pdfDoc.Pages[pageNumber], pngStream);
                }

                Console.WriteLine($"Saved page {pageNumber} as PNG → {outputPath}");
            }
        }

        Console.WriteLine("Selected pages conversion completed.");
    }
}
