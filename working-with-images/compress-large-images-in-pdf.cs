using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Optimization;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "compressed_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create an OptimizationOptions instance
            OptimizationOptions optOptions = new OptimizationOptions();

            // Configure the existing ImageCompressionOptions instance (the property is read‑only)
            optOptions.ImageCompressionOptions.CompressImages = true;
            optOptions.ImageCompressionOptions.ImageQuality   = 75; // JPEG quality (0‑100)
            // You can also set the encoding if needed, e.g.:
            // optOptions.ImageCompressionOptions.Encoding = ImageEncoding.Jpeg;

            // Apply the optimization to the document.
            // This will recompress images (including those larger than 2 MB) to JPEG
            // with the specified quality, reducing overall PDF size.
            doc.OptimizeResources(optOptions);

            // Save the optimized PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Optimized PDF saved to '{outputPath}'.");
    }
}
