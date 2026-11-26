using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "Images";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Load PDF with deterministic disposal
        using (Document pdfDoc = new Document(inputPath))
        {
            // Determine how many pages to process (max 5)
            int pagesToConvert = Math.Min(5, pdfDoc.Pages.Count);

            // Create a JPEG device with 250 DPI resolution
            JpegDevice jpegDevice = new JpegDevice(250);

            // Iterate using 1‑based page indexing
            for (int i = 1; i <= pagesToConvert; i++)
            {
                string outputPath = Path.Combine(outputDir, $"page_{i}.jpg");
                // Convert the page to JPEG and save
                jpegDevice.Process(pdfDoc.Pages[i], outputPath);
                Console.WriteLine($"Saved page {i} as {outputPath}");
            }
        }
    }
}