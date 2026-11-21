using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices; // For PngDevice

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "OddPages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPdf))
            {
                // Iterate over odd‑numbered pages (Aspose.Pdf uses 1‑based indexing)
                for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum += 2)
                {
                    // Build the output file name for the current page
                    string outPath = Path.Combine(outputDir, $"Page_{pageNum}.png");

                    // Create a PNG device – this defines the image format and options
                    PngDevice pngDevice = new PngDevice();

                    // Convert the specific page to PNG and save it
                    using (FileStream outStream = new FileStream(outPath, FileMode.Create))
                    {
                        pngDevice.Process(doc.Pages[pageNum], outStream);
                    }
                }
            }

            Console.WriteLine("Odd pages have been saved as PNG images.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
