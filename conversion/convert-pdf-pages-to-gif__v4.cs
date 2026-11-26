using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "GifPages";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPath))
        {
            // Define the resolution for the output GIF images (default 96 DPI)
            Resolution resolution = new Resolution(96);

            // GifDevice does NOT implement IDisposable, so do NOT wrap it in a using block
            GifDevice gifDevice = new GifDevice(resolution);

            // Aspose.Pdf uses 1‑based page indexing
            for (int i = 1; i <= pdfDoc.Pages.Count; i++)
            {
                string outputPath = Path.Combine(outputDir, $"page_{i}.gif");

                // Create a FileStream for the output GIF and dispose it after processing
                using (FileStream fs = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    // Render the current page to a GIF file via the device
                    gifDevice.Process(pdfDoc.Pages[i], fs);
                }

                Console.WriteLine($"Saved page {i} as GIF: {outputPath}");
            }
        }

        Console.WriteLine("All pages have been converted to GIF images.");
    }
}
