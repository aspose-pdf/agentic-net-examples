using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "TiffPages";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPath))
        {
            // Create a TiffDevice with the required resolution (150 DPI)
            TiffDevice tiffDevice = new TiffDevice(new Resolution(150));

            // Aspose.Pdf uses 1‑based page indexing
            for (int i = 1; i <= pdfDoc.Pages.Count; i++)
            {
                string outPath = Path.Combine(outputDir, $"Page_{i}.tiff");

                // Create a file stream for the TIFF output
                using (FileStream outStream = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                {
                    // Process the current page and write the TIFF image to the stream
                    tiffDevice.Process(pdfDoc.Pages[i], outStream);
                }

                Console.WriteLine($"Saved page {i} as TIFF → {outPath}");
            }
        }
    }
}
