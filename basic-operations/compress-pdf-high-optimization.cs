using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Optimization;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_high.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF, apply high compression, and save it
        using (Document doc = new Document(inputPath))
        {
            // Configure optimization options for high compression
            OptimizationOptions opt = OptimizationOptions.All();
            // Image compression settings (new API)
            opt.ImageCompressionOptions.CompressImages = true;   // compress images
            opt.ImageCompressionOptions.ImageQuality = 30;       // 0‑100, lower = higher compression
            // General optimization flags
            opt.RemoveUnusedObjects = true;      // strip unused objects
            opt.RemoveUnusedStreams = true;      // strip unused streams
            // Note: CompressStreams property has been removed; OptimizeResources handles stream compression.

            // Apply the optimization to the document
            doc.OptimizeResources(opt);

            // Save the optimized document
            doc.Save(outputPath);
        }

        // Compare file sizes
        FileInfo originalInfo = new FileInfo(inputPath);
        FileInfo compressedInfo = new FileInfo(outputPath);

        Console.WriteLine($"Original size   : {originalInfo.Length} bytes");
        Console.WriteLine($"Compressed size : {compressedInfo.Length} bytes");

        if (originalInfo.Length > 0)
        {
            double reduction = 100.0 * (originalInfo.Length - compressedInfo.Length) / originalInfo.Length;
            Console.WriteLine($"Size reduction  : {reduction:F2}%");
        }
        else
        {
            Console.WriteLine("Original file size is zero; cannot compute reduction.");
        }
    }
}
