using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Input PDF containing graphs
        const string inputPdfPath = "graph_input.pdf";
        // Output folder for rendered images
        const string outputFolder = "RenderedImages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Configure rendering options:
            // - High resolution (300 DPI) for better detail and inherent anti‑aliasing
            Resolution renderResolution = new Resolution(300);
            PngDevice pngDevice = new PngDevice(renderResolution);

            // Render each page to a separate PNG file
            for (int pageNumber = 1; pageNumber <= pdfDoc.Pages.Count; pageNumber++)
            {
                string outputImagePath = Path.Combine(outputFolder, $"Page_{pageNumber}.png");
                using (FileStream imageStream = new FileStream(outputImagePath, FileMode.Create))
                {
                    // Render the current page using the configured device
                    pngDevice.Process(pdfDoc.Pages[pageNumber], imageStream);
                }

                Console.WriteLine($"Rendered page {pageNumber} to '{outputImagePath}'.");
            }
        }

        Console.WriteLine("Rendering completed with high‑resolution output (anti‑aliasing applied by the renderer).");
    }
}
