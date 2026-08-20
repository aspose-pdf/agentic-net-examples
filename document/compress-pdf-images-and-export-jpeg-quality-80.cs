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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Load PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPath))
        {
            // ------------------------------------------------------------
            // 1. Create OptimizationOptions and configure JPEG compression
            //    with quality 80 via the read‑only ImageCompressionOptions.
            // ------------------------------------------------------------
            var opt = new OptimizationOptions();
            // The ImageCompressionOptions property is read‑only; modify the
            // existing instance instead of assigning a new one.
            opt.ImageCompressionOptions.CompressImages = true;
            opt.ImageCompressionOptions.ImageQuality = 80;
            // (Optional) Set the encoding to JPEG if you want to be explicit.
            // opt.ImageCompressionOptions.Encoding = ImageEncoding.Jpeg;

            // Apply the optimization settings to the PDF.
            pdfDoc.OptimizeResources(opt);

            // ------------------------------------------------------------
            // 2. Convert each page to a JPEG image using the same quality.
            // ------------------------------------------------------------
            Resolution resolution = new Resolution(150); // 150 DPI – adjust as needed
            JpegDevice jpegDevice = new JpegDevice(resolution, 80);

            for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
            {
                string outPath = Path.Combine(outputDir, $"page_{pageNum}.jpeg");
                using (FileStream outStream = new FileStream(outPath, FileMode.Create))
                {
                    jpegDevice.Process(pdfDoc.Pages[pageNum], outStream);
                }
                Console.WriteLine($"Saved {outPath}");
            }
        }
    }
}
