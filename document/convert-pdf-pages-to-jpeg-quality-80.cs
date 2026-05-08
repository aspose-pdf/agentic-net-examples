using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Aspose.Pdf.Optimization;

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

        // Load PDF document inside a using block (lifecycle rule)
        using (Document doc = new Document(inputPath))
        {
            // -----------------------------------------------------------------
            // Apply JPEG compression (quality 80) to images *inside* the PDF.
            // Use the modern OptimizationOptions API (Document.OptimizationOptions is obsolete).
            // -----------------------------------------------------------------
            OptimizationOptions opt = new OptimizationOptions();
            opt.ImageCompressionOptions.CompressImages = true;
            opt.ImageCompressionOptions.ImageQuality = 80; // JPEG quality for embedded images
            doc.OptimizeResources(opt);

            // Create JpegDevice with JPEG quality 80 for the output images.
            JpegDevice jpegDevice = new JpegDevice(80);

            // Pages are 1‑based (global rule)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                string outputPath = Path.Combine(outputDir, $"page_{pageIndex}.jpeg");

                // Save each page as JPEG using the device
                using (FileStream outStream = new FileStream(outputPath, FileMode.Create))
                {
                    jpegDevice.Process(doc.Pages[pageIndex], outStream);
                }
            }
        }

        Console.WriteLine("PDF pages have been converted to JPEG images with quality 80.");
    }
}
