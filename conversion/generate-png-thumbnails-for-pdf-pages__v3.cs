using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "Thumbnails";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Aspose.Pdf uses 1‑based page indexing
            for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
            {
                string outputPngPath = Path.Combine(outputFolder, $"page_{pageIndex}.png");

                // Create a PNG device with the required dimensions (90 × 130 pixels)
                // Use the constructor that accepts width, height and resolution.
                using (FileStream pngStream = new FileStream(outputPngPath, FileMode.Create))
                {
                    var pngDevice = new PngDevice(90, 130, new Resolution(72));
                    // Process the specific page and write the image to the stream
                    pngDevice.Process(pdfDoc.Pages[pageIndex], pngStream);
                }

                Console.WriteLine($"Thumbnail saved: {outputPngPath}");
            }
        }
    }
}
