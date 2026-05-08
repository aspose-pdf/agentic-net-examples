using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Optimization;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";      // source PDF containing large images
        const string outputPdf = "output_optimized.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Create an optimization strategy with all options enabled
            OptimizationOptions opt = OptimizationOptions.All();

            // Enable image compression and set desired quality (0‑100)
            opt.ImageCompressionOptions.CompressImages = true;
            opt.ImageCompressionOptions.ImageQuality = 50; // adjust as needed

            // Down‑sample images that exceed the specified resolution (dpi)
            // This reduces pixel dimensions while preserving visual quality
            opt.ImageCompressionOptions.MaxResolution = 150; // max DPI for images

            // Apply the optimization to the document
            pdfDoc.OptimizeResources(opt);

            // Save the optimized PDF
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"Optimized PDF saved to '{outputPdf}'.");
    }
}