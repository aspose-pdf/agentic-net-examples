using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Input PDF file
        const string inputPath = "input.pdf";
        // Output folder for JPEG images
        const string outputDir = "output";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Load PDF inside a using block for deterministic disposal
        using (Document pdf = new Document(inputPath))
        {
            // JpegDevice with default settings (150 DPI, max quality)
            JpegDevice jpegDevice = new JpegDevice();

            // Aspose.Pdf uses 1‑based page indexing
            for (int pageNumber = 1; pageNumber <= pdf.Pages.Count; pageNumber++)
            {
                string outPath = Path.Combine(outputDir, $"page_{pageNumber}.jpg");

                // Write each page to a JPEG file
                using (FileStream fs = new FileStream(outPath, FileMode.Create))
                {
                    jpegDevice.Process(pdf.Pages[pageNumber], fs);
                }
            }
        }

        Console.WriteLine("PDF successfully converted to JPEG images.");
    }
}