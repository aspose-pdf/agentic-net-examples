using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "PngPages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Register a fallback font for any missing fonts in the PDF.
        // The asterisk ("*") matches any missing font name.
        FontRepository.Substitutions.Add(new SimpleFontSubstitution("*", "Arial"));

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Aspose.Pdf uses 1‑based page indexing
            for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
            {
                // Create a PNG device with 300 DPI resolution
                PngDevice pngDevice = new PngDevice(new Resolution(300));

                // Prepare output file path for the current page
                string outputPath = Path.Combine(outputFolder, $"page_{pageNum}.png");

                // Render the page to PNG using a FileStream
                using (FileStream outStream = new FileStream(outputPath, FileMode.Create))
                {
                    pngDevice.Process(pdfDoc.Pages[pageNum], outStream);
                }

                Console.WriteLine($"Rendered page {pageNum} to {outputPath}");
            }
        }

        Console.WriteLine("All pages have been rendered to PNG.");
    }
}
