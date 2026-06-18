using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;          // JpegDevice
using Aspose.Pdf.Optimization;    // OptimizationOptions, ImageCompressionOptions

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "Images";

        // Verify source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPath))
        {
            // ------------------------------------------------------------
            // Configure JPEG compression for images *inside* the PDF using
            // the modern OptimizationOptions API (Document.OptimizationOptions
            // is obsolete).
            // ------------------------------------------------------------
            OptimizationOptions opt = new OptimizationOptions();
            opt.ImageCompressionOptions.CompressImages = true;
            opt.ImageCompressionOptions.ImageQuality = 80; // 0‑100
            // Apply the optimization with the configured options.
            pdfDoc.OptimizeResources(opt);

            // Create a JpegDevice with 300 DPI resolution and quality 80 for
            // page‑to‑image conversion.
            Resolution resolution = new Resolution(300);
            JpegDevice jpegDevice = new JpegDevice(resolution, 80);

            // Convert each page to a JPEG file
            for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
            {
                string outPath = Path.Combine(outputDir, $"page_{pageNum}.jpeg");
                using (FileStream outStream = new FileStream(outPath, FileMode.Create))
                {
                    jpegDevice.Process(pdfDoc.Pages[pageNum], outStream);
                }
            }
        }

        Console.WriteLine("PDF pages have been converted to JPEG images with quality 80.");
    }
}
