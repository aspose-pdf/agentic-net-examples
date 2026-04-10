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
        const string outputFolder = "PngPages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // ------------------------------------------------------------
            // Configure image compression options for PNG output
            // ------------------------------------------------------------
            // OptimizationOptions exposes a read‑only ImageCompressionOptions instance.
            // Retrieve that instance and set the desired properties.
            OptimizationOptions optOptions = new OptimizationOptions();

            // Configure the existing ImageCompressionOptions object
            optOptions.ImageCompressionOptions.CompressImages = true;
            // 100 means lossless PNG compression (no quality loss)
            optOptions.ImageCompressionOptions.ImageQuality = 100;
            // The Version property is optional; the default works for PNG.
            // If a specific version is required, use the correct enum value, e.g. ImageCompressionVersion.V1_0.
            // optOptions.ImageCompressionOptions.Version = ImageCompressionVersion.V1_0;

            // Apply the optimization to the document. This prepares the PDF resources
            // according to the compression settings.
            pdfDoc.OptimizeResources(optOptions);

            // ------------------------------------------------------------
            // Rasterize each page to a PNG image using PngDevice
            // ------------------------------------------------------------
            // Create a PngDevice with default resolution (150 DPI). Adjust if needed.
            PngDevice pngDevice = new PngDevice();

            for (int pageNumber = 1; pageNumber <= pdfDoc.Pages.Count; pageNumber++)
            {
                string outputPath = Path.Combine(outputFolder, $"page_{pageNumber}.png");

                // Process the page and write the PNG to a file stream
                using (FileStream pngStream = new FileStream(outputPath, FileMode.Create))
                {
                    pngDevice.Process(pdfDoc.Pages[pageNumber], pngStream);
                }

                Console.WriteLine($"Saved PNG: {outputPath}");
            }
        }

        Console.WriteLine("Rasterization completed.");
    }
}
