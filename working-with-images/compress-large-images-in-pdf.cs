using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Optimization;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_compressed.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Configure optimization options
            OptimizationOptions optOptions = new OptimizationOptions();

            // Enable image compression
            optOptions.ImageCompressionOptions.CompressImages = true;
            // Set JPEG quality (0‑100). Lower value = higher compression.
            optOptions.ImageCompressionOptions.ImageQuality = 70;
            // Optionally limit the maximum resolution of images (in DPI)
            optOptions.ImageCompressionOptions.MaxResolution = 1500;

            // Apply the optimization to the document
            pdfDoc.OptimizeResources(optOptions);

            // Save the optimized PDF
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"Optimized PDF saved to '{outputPdf}'.");
    }
}