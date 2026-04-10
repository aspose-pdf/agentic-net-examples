using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "BmpImages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Set the desired resolution (300 DPI)
            Resolution resolution = new Resolution(300);

            // Create a BmpDevice with the specified resolution
            BmpDevice bmpDevice = new BmpDevice(resolution);

            // Optional: configure high‑quality rendering options
            bmpDevice.RenderingOptions = new RenderingOptions
            {
                // Uncomment and adjust as needed for higher quality
                // TextRenderingMode = TextRenderingMode.Vector,
                // ImageRenderingMode = ImageRenderingMode.Vector,
                // AntiAliasing = true
            };

            // Iterate through all pages (1‑based indexing)
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                string outputPath = Path.Combine(outputFolder, $"page_{pageNumber}.bmp");

                // Save each page as a BMP image
                using (FileStream bmpStream = new FileStream(outputPath, FileMode.Create))
                {
                    bmpDevice.Process(pdfDocument.Pages[pageNumber], bmpStream);
                }
            }
        }

        Console.WriteLine("PDF pages have been exported as BMP images at 300 DPI.");
    }
}