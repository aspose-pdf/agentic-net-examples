using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Optimization; // Core optimization namespace

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "compressed_output.pdf";

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Configure optimization options for image compression and cleanup.
            // ImageQuality: 0‑100 (higher = better quality, larger size). 75 is a good trade‑off.
            // MaxResolution: DPI limit for raster images after compression. 150 DPI keeps visual fidelity.
            OptimizationOptions opt = OptimizationOptions.All();
            opt.ImageCompressionOptions.CompressImages = true;
            opt.ImageCompressionOptions.ImageQuality   = 75;   // 0‑100
            opt.ImageCompressionOptions.MaxResolution  = 150;  // DPI
            opt.RemoveUnusedObjects = true;
            opt.RemoveUnusedStreams = true;

            // Apply the optimization to the document.
            doc.OptimizeResources(opt);

            // Save the compressed PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Compressed PDF saved to '{outputPath}'.");
    }
}
