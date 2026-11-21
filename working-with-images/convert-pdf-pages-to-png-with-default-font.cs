using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputFolder = "Images";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPath))
        {
            // Iterate pages using 1‑based indexing (Aspose.Pdf requirement)
            for (int pageNumber = 1; pageNumber <= pdfDoc.Pages.Count; pageNumber++)
            {
                string imagePath = Path.Combine(outputFolder, $"page_{pageNumber}.png");

                // Create a file stream for the output image
                using (FileStream imageStream = new FileStream(imagePath, FileMode.Create))
                {
                    // Define the resolution (e.g., 300 DPI)
                    Resolution resolution = new Resolution(300);

                    // Create a PNG device and set the default rendering font to Arial
                    PngDevice pngDevice = new PngDevice(resolution);
                    pngDevice.RenderingOptions.DefaultFontName = "Arial";

                    // Render the page to a PNG image
                    pngDevice.Process(pdfDoc.Pages[pageNumber], imageStream);
                }

                Console.WriteLine($"Saved page {pageNumber} as PNG to '{imagePath}'.");
            }
        }
    }
}
