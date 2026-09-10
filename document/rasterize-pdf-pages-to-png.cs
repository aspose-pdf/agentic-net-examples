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
        const string outputFolder  = "PngPages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document (using statement ensures proper disposal)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Configure image compression options for lossless PNG output
            OptimizationOptions opt = OptimizationOptions.All();
            opt.ImageCompressionOptions.CompressImages = true;   // enable compression
            opt.ImageCompressionOptions.ImageQuality   = 100;   // max quality (lossless for PNG)

            // Apply the optimization settings to the document
            pdfDoc.OptimizeResources(opt);

            // Define the resolution for the PNG images (e.g., 300 DPI)
            Resolution resolution = new Resolution(300);

            // Create a PNG device with the specified resolution
            PngDevice pngDevice = new PngDevice(resolution);

            // Iterate through all pages (1‑based indexing)
            for (int pageNumber = 1; pageNumber <= pdfDoc.Pages.Count; pageNumber++)
            {
                string pngPath = Path.Combine(outputFolder, $"page_{pageNumber}.png");

                // Save each page as a PNG file using the PNG device
                using (FileStream pngStream = new FileStream(pngPath, FileMode.Create))
                {
                    pngDevice.Process(pdfDoc.Pages[pageNumber], pngStream);
                }

                Console.WriteLine($"Saved page {pageNumber} as PNG: {pngPath}");
            }
        }

        Console.WriteLine("Rasterization completed.");
    }
}