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
        const string outputDir = "PngPages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdf))
        {
            // ------------------------------------------------------------
            // Configure image compression options for PNG output
            // ------------------------------------------------------------
            // Create optimization options and obtain the ImageCompressionOptions instance
            OptimizationOptions optOptions = new OptimizationOptions();
            ImageCompressionOptions imgComp = optOptions.ImageCompressionOptions;

            // Enable image compression and set a high quality (lossless PNG)
            imgComp.CompressImages = true;      // activate compression
            imgComp.ImageQuality   = 100;       // 0-100, 100 = best quality (lossless for PNG)

            // Apply the optimization to the document
            pdfDoc.OptimizeResources(optOptions);

            // ------------------------------------------------------------
            // Rasterize each page to a PNG image using PngDevice
            // ------------------------------------------------------------
            // Define desired resolution (e.g., 300 DPI)
            Resolution resolution = new Resolution(300);
            PngDevice pngDevice = new PngDevice(resolution);

            for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
            {
                string outPath = Path.Combine(outputDir, $"page_{pageNum}.png");
                using (FileStream outStream = new FileStream(outPath, FileMode.Create))
                {
                    // Convert the page to PNG and write to the file stream
                    pngDevice.Process(pdfDoc.Pages[pageNum], outStream);
                }

                Console.WriteLine($"Saved PNG: {outPath}");
            }
        }
    }
}