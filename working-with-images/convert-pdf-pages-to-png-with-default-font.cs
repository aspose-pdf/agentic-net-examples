using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Aspose.Pdf.Drawing;

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

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Convert each page to an image (PNG format) using PngDevice
            for (int pageNumber = 1; pageNumber <= doc.Pages.Count; pageNumber++)
            {
                string outPath = System.IO.Path.Combine(outputDir, $"Page_{pageNumber}.png");

                // Create a PNG device with desired resolution
                var pngDevice = new PngDevice(new Resolution(300))
                {
                    // Set the default font for missing fonts
                    RenderingOptions = new RenderingOptions { DefaultFontName = "Arial" }
                };

                // Process the specific page and save as PNG
                pngDevice.Process(doc.Pages[pageNumber], outPath);
                Console.WriteLine($"Saved page {pageNumber} as image → {outPath}");
            }
        }
    }
}
