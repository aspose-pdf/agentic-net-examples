using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "PngPages";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Optional: set a default fallback font for missing glyphs
        // FontRepository.DefaultFont = FontRepository.FindFont("Arial");

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based page indexing
            for (int i = 1; i <= pdfDoc.Pages.Count; i++)
            {
                // Create a PNG device with 300 DPI resolution
                Resolution resolution = new Resolution(300);
                PngDevice pngDevice = new PngDevice(resolution);

                // Define the output file name for the current page
                string outPath = Path.Combine(outputDir, $"Page_{i}.png");

                // Render the page to a PNG file
                using (FileStream imageStream = new FileStream(outPath, FileMode.Create))
                {
                    pngDevice.Process(pdfDoc.Pages[i], imageStream);
                }

                Console.WriteLine($"Rendered page {i} to {outPath}");
            }
        }
    }
}