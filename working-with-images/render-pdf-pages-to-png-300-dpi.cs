using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "output_images";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // NOTE: The Document class no longer exposes a DefaultFontName property.
            // If a default font is required for missing fonts, register the font with the
            // FontRepository (e.g., pdfDocument.FontRepository.AddFont("Arial.ttf")).
            // For the purpose of rendering pages to images, this step can be omitted.

            // Create a Resolution object for 300 DPI
            Resolution resolution = new Resolution(300);

            // Initialize the PNG device with the specified resolution
            PngDevice pngDevice = new PngDevice(resolution);

            // Iterate pages using 1‑based indexing (rule: page-indexing-one-based)
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                string outputPath = Path.Combine(outputFolder, $"page_{pageNumber}.png");

                // Save each page as a PNG image
                using (FileStream pngStream = new FileStream(outputPath, FileMode.Create))
                {
                    pngDevice.Process(pdfDocument.Pages[pageNumber], pngStream);
                }
            }
        }

        Console.WriteLine("All pages have been rendered to PNG at 300 DPI.");
    }
}
