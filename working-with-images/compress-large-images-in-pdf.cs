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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document doc = new Document(inputPath);

        // Configure optimization to compress images (JPEG, quality 75) and remove unused objects/streams.
        // This effectively replaces large images with compressed JPEG versions, reducing overall size.
        OptimizationOptions opt = OptimizationOptions.All();
        opt.CompressImages = true;          // enable image compression
        opt.ImageQuality = 75;               // JPEG quality (0‑100)
        opt.RemoveUnusedObjects = true;     // clean up unused PDF objects
        opt.RemoveUnusedStreams = true;     // clean up unused streams

        // Apply the optimization to the document.
        doc.OptimizeResources(opt);

        // Save the optimized PDF.
        doc.Save(outputPath);

        Console.WriteLine($"Compressed PDF saved to '{outputPath}'.");
    }
}
