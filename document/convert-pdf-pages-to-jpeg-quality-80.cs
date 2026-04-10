using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Aspose.Pdf.Optimization;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputDir = "Images";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document
        using (Document pdfDocument = new Document(inputPdf))
        {
            // ------------------------------------------------------------
            // Configure image compression for the PDF (quality = 80)
            // ------------------------------------------------------------
            OptimizationOptions opt = new OptimizationOptions();

            // ImageCompressionOptions is read‑only; modify its properties directly
            opt.ImageCompressionOptions.CompressImages = true;
            opt.ImageCompressionOptions.ImageQuality   = 80;   // JPEG quality 0‑100

            // Apply the optimization to the document
            pdfDocument.OptimizeResources(opt);

            // ------------------------------------------------------------
            // Convert each page to a JPEG image using the same quality
            // ------------------------------------------------------------
            // Resolution (DPI) – you can adjust as needed
            Resolution resolution = new Resolution(150);

            // JpegDevice constructor that accepts resolution and quality
            JpegDevice jpegDevice = new JpegDevice(resolution, 80);

            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                string outputPath = Path.Combine(outputDir, $"page_{pageNumber}.jpg");

                using (FileStream imageStream = new FileStream(outputPath, FileMode.Create))
                {
                    // Process the specific page and write the JPEG image
                    jpegDevice.Process(pdfDocument.Pages[pageNumber], imageStream);
                }

                Console.WriteLine($"Saved page {pageNumber} as JPEG → {outputPath}");
            }
        }

        Console.WriteLine("All pages have been converted to JPEG images with quality 80.");
    }
}