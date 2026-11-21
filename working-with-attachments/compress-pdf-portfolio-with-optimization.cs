using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Optimization;

class Program
{
    static void Main()
    {
        const string inputPath = "portfolio.pdf";          // existing PDF Portfolio
        const string outputPath = "portfolio_compressed.pdf"; // destination with compression

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF Portfolio inside a using block for deterministic disposal
        using (Document portfolio = new Document(inputPath))
        {
            // Configure optimization options to enable compression of objects, streams, and images
            OptimizationOptions opt = OptimizationOptions.All();
            opt.CompressObjects = true;
            opt.RemoveUnusedObjects = true;
            opt.RemoveUnusedStreams = true;
            opt.LinkDuplicateStreams = true;
            // Optional image compression settings (adjust quality as needed)
            opt.CompressImages = true;
            opt.ImageQuality = 50; // 0‑100, lower = higher compression

            // Apply the optimization to the document
            portfolio.OptimizeResources(opt);

            // Save the compressed portfolio; Document.Save writes PDF by default
            portfolio.Save(outputPath);
        }

        Console.WriteLine($"Compressed PDF Portfolio saved to '{outputPath}'.");
    }
}
