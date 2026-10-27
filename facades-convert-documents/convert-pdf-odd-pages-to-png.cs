using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "OddPagesImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Load the PDF document
        Document pdfDocument = new Document(inputPdf);
        int pageCount = pdfDocument.Pages.Count;

        // Define the resolution for the PNG images
        var resolution = new Resolution(150);

        // Process only odd‑numbered pages
        for (int pageNumber = 1; pageNumber <= pageCount; pageNumber += 2)
        {
            using (var imageStream = new MemoryStream())
            {
                // Convert the current page to PNG using PngDevice (cross‑platform)
                var pngDevice = new PngDevice(resolution);
                pngDevice.Process(pdfDocument.Pages[pageNumber], imageStream);

                string outputPath = Path.Combine(outputDir, $"Page_{pageNumber}.png");
                File.WriteAllBytes(outputPath, imageStream.ToArray());
                Console.WriteLine($"Saved odd page {pageNumber} as {outputPath}");
            }
        }
    }
}
