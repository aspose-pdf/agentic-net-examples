using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Optimization;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "compressed.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF generated from XML
        using (Document doc = new Document(inputPath))
        {
            // Configure optimization to compress streams and images
            OptimizationOptions opt = OptimizationOptions.All();
            opt.CompressImages = true;          // compress image streams
            opt.ImageQuality = 50;               // adjust quality as needed (0‑100)
            opt.RemoveUnusedObjects = true;     // drop objects not referenced
            opt.RemoveUnusedStreams = true;     // drop empty/unused streams

            // Apply the optimization settings
            doc.OptimizeResources(opt);

            // Save the optimized (compressed) PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Compressed PDF saved to '{outputPath}'.");
    }
}
