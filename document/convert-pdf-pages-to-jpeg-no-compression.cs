using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Aspose.Pdf.Optimization;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "Images";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // ----- Fixed: use the new OptimizationOptions API -----
            // Create an OptimizationOptions instance and disable image compression.
            // This affects PDF saving; raster conversion quality is controlled by the device.
            OptimizationOptions opt = new OptimizationOptions();
            opt.ImageCompressionOptions.CompressImages = false;
            pdfDoc.OptimizeResources(opt);
            // -------------------------------------------------------

            // Create a JPEG device with high quality (100 = no compression loss)
            Resolution resolution = new Resolution(300); // optional, adjust DPI as needed
            JpegDevice jpegDevice = new JpegDevice(resolution, quality: 100);

            // Iterate through all pages (1‑based indexing)
            for (int pageNumber = 1; pageNumber <= pdfDoc.Pages.Count; pageNumber++)
            {
                string outputPath = Path.Combine(outputFolder, $"page_{pageNumber}.jpg");

                // Convert the page to JPEG and save to file
                using (FileStream imageStream = new FileStream(outputPath, FileMode.Create))
                {
                    jpegDevice.Process(pdfDoc.Pages[pageNumber], imageStream);
                }

                Console.WriteLine($"Saved page {pageNumber} as JPEG → {outputPath}");
            }
        }
    }
}
