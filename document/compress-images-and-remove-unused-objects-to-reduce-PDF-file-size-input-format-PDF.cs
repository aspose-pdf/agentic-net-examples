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

        // Load the existing PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Configure optimization options:
            // - CompressObjects: pack PDF objects into streams and compress them
            // - RemoveUnusedObjects / RemoveUnusedStreams: delete objects that are not referenced
            // - SubsetFonts: embed only the glyphs that are actually used
            // - MaxResoultion: downscale images that exceed the given DPI
            // - ImageEncoding: re‑encode images (e.g., to JPEG) to reduce size
            OptimizationOptions opt = new OptimizationOptions
            {
                CompressObjects      = true,
                RemoveUnusedObjects  = true,
                RemoveUnusedStreams  = true,
                SubsetFonts          = true,
                MaxResoultion        = 150,                     // DPI limit for images
                ImageEncoding        = ImageEncoding.Jpeg      // Re‑encode images as JPEG
            };

            // Apply the optimization to the document
            doc.OptimizeResources(opt);

            // Optional: linearize the PDF for faster web access
            doc.Optimize();

            // Save the optimized PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Optimized PDF saved to '{outputPath}'.");
    }
}