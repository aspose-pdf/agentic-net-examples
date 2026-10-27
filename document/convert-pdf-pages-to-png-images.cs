using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "Images";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPdf))
            {
                // Convert every page to an image using core Aspose.Pdf APIs
                ConvertAllPagesToImages(doc, outputDir);
            }

            Console.WriteLine($"All pages have been saved as images in '{outputDir}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Renders each page of the supplied PDF document to a separate PNG image file.
    /// </summary>
    /// <param name="doc">The loaded PDF document.</param>
    /// <param name="outputDir">Directory where image files will be written.</param>
    private static void ConvertAllPagesToImages(Document doc, string outputDir)
    {
        // Define the resolution for the output images (e.g., 300 DPI)
        var resolution = new Resolution(300);
        // Choose the image format – here we use PNG. You can switch to JpegDevice, etc.
        var device = new PngDevice(resolution);

        for (int pageNumber = 1; pageNumber <= doc.Pages.Count; pageNumber++)
        {
            var page = doc.Pages[pageNumber];
            string outPath = Path.Combine(outputDir, $"page_{pageNumber}.png");
            using (var stream = new FileStream(outPath, FileMode.Create))
            {
                device.Process(page, stream);
            }
        }
    }
}
