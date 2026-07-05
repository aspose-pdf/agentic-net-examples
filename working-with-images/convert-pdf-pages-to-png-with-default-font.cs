using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "PageImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        using (Document pdfDoc = new Document(inputPdf))
        {
            // Define the resolution for the output images (e.g., 300 DPI)
            var resolution = new Resolution(300);

            // Loop through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageNumber = 1; pageNumber <= pdfDoc.Pages.Count; pageNumber++)
            {
                string outputPath = Path.Combine(outputDir, $"Page_{pageNumber}.png");

                // Create a PNG device for the current page
                var pngDevice = new PngDevice(resolution);

                // Set the default font name to be used when a font is missing
                pngDevice.RenderingOptions = new RenderingOptions
                {
                    DefaultFontName = "Arial"
                };

                // Render the page to the PNG file
                pngDevice.Process(pdfDoc.Pages[pageNumber], outputPath);
                Console.WriteLine($"Saved page {pageNumber} as image → {outputPath}");
            }
        }
    }
}
