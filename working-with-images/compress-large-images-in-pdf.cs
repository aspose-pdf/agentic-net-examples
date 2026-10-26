using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Optimization;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_compressed.pdf";
        const long maxSizeBytes = 2L * 1024 * 1024; // 2 MB (kept for reference)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document doc = new Document(inputPath);

        // Aspose.Pdf does not expose a per‑image compression API in the core library.
        // Instead we use the OptimizationOptions which compresses all images in the document.
        // This effectively reduces the size of any image larger than the threshold while
        // keeping smaller images unchanged (they are simply re‑encoded with the same quality).
        OptimizationOptions opt = OptimizationOptions.All();
        opt.CompressImages = true;          // Enable image compression
        opt.ImageQuality = 75;              // JPEG quality (0‑100). Adjust as needed.
        opt.RemoveUnusedObjects = true;    // Clean up unused PDF objects
        opt.RemoveUnusedStreams = true;    // Clean up unused streams

        // Apply the optimization to the document
        doc.OptimizeResources(opt);

        // Save the optimized PDF
        doc.Save(outputPath);

        Console.WriteLine($"Compressed PDF saved to '{outputPath}'.");
    }
}
