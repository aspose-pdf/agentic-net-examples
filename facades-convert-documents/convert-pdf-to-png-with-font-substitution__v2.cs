using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputDir = "PngImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Register font substitution: replace missing Helvetica with Times New Roman
        FontRepository.Substitutions.Add(new SimpleFontSubstitution("Helvetica", "Times New Roman"));

        // Load the PDF document (wrapped in using for proper disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Set up rendering options (optional: enable font hinting for better quality)
            RenderingOptions renderOpts = new RenderingOptions
            {
                UseFontHinting = true
            };

            // Define resolution for PNG output (e.g., 300 DPI)
            Resolution resolution = new Resolution(300);

            // Create a PNG device with the specified resolution and rendering options
            PngDevice pngDevice = new PngDevice(resolution)
            {
                RenderingOptions = renderOpts
            };

            // Iterate over all pages (1‑based indexing)
            for (int pageNumber = 1; pageNumber <= doc.Pages.Count; pageNumber++)
            {
                string outputPath = Path.Combine(outputDir, $"page_{pageNumber}.png");

                // Save each page as a PNG image
                using (FileStream imageStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    pngDevice.Process(doc.Pages[pageNumber], imageStream);
                }

                Console.WriteLine($"Saved page {pageNumber} as PNG → '{outputPath}'");
            }
        }
    }
}