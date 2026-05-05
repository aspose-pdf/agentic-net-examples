using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Aspose.Pdf.Optimization;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputFolder  = "RasterizedPages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document inside a using block for proper disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // ------------------------------------------------------------
            // Configure image compression options for lossless PNG output
            // ------------------------------------------------------------
            OptimizationOptions opt = new OptimizationOptions();

            // Enable image compression and set quality to 100 (lossless)
            opt.ImageCompressionOptions.CompressImages = true;
            opt.ImageCompressionOptions.ImageQuality   = 100; // 0‑100, 100 = no quality loss

            // Apply the optimization settings to the document
            pdfDoc.OptimizeResources(opt);

            // ------------------------------------------------------------
            // Rasterize each page to a PNG image using PngDevice
            // ------------------------------------------------------------
            // Define the desired resolution (e.g., 300 DPI)
            Resolution resolution = new Resolution(300);
            PngDevice pngDevice = new PngDevice(resolution);

            for (int pageNumber = 1; pageNumber <= pdfDoc.Pages.Count; pageNumber++)
            {
                string outputPath = Path.Combine(outputFolder, $"page_{pageNumber}.png");

                // Save the rasterized page to a file stream
                using (FileStream pngStream = new FileStream(outputPath, FileMode.Create))
                {
                    pngDevice.Process(pdfDoc.Pages[pageNumber], pngStream);
                }

                Console.WriteLine($"Page {pageNumber} saved as PNG: {outputPath}");
            }
        }

        Console.WriteLine("Rasterization completed.");
    }
}