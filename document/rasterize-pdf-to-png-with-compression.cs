using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Aspose.Pdf.Optimization;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFolder = "output_images";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Configure image compression options for PNG output
            OptimizationOptions opt = new OptimizationOptions();
            ImageCompressionOptions imgComp = opt.ImageCompressionOptions;
            imgComp.CompressImages = true;      // enable compression
            imgComp.ImageQuality = 90;          // PNG compression level (0-100)

            // Apply the optimization settings to the document
            doc.OptimizeResources(opt);

            // Rasterize each page to a PNG image
            for (int pageNumber = 1; pageNumber <= doc.Pages.Count; pageNumber++)
            {
                // Create a PNG device with desired resolution
                PngDevice pngDevice = new PngDevice(new Resolution(300));

                string outputPath = Path.Combine(outputFolder, $"page_{pageNumber}.png");
                using (FileStream outStream = new FileStream(outputPath, FileMode.Create))
                {
                    pngDevice.Process(doc.Pages[pageNumber], outStream);
                }

                Console.WriteLine($"Saved page {pageNumber} as PNG to '{outputPath}'.");
            }
        }
    }
}