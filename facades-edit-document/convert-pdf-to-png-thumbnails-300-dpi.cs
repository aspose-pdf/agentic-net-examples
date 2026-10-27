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
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Iterate through each page (Aspose.Pdf uses 1‑based indexing)
            for (int pageNumber = 1; pageNumber <= pdfDoc.Pages.Count; pageNumber++)
            {
                string outputPath = Path.Combine(outputFolder, $"Page_{pageNumber}.png");

                // Create a PNG device with the required DPI (300) and transparent background
                var resolution = new Resolution(300);
                var pngDevice = new PngDevice(resolution)
                {
                    TransparentBackground = true // enable transparent background
                };

                // Render the current page to the PNG stream
                using (var outStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    pngDevice.Process(pdfDoc.Pages[pageNumber], outStream);
                }

                Console.WriteLine($"Saved thumbnail: {outputPath}");
            }
        }
    }
}
