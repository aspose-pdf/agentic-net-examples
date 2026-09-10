using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Optimization;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_high_compression.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Get original file size
        long originalSize = new FileInfo(inputPath).Length;

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Create optimization options with high compression settings
            OptimizationOptions optOptions = OptimizationOptions.All();
            optOptions.CompressObjects = true;          // compress PDF objects
            optOptions.SubsetFonts      = true;          // embed only used glyphs
            optOptions.RemoveUnusedObjects = true;      // drop unused objects
            optOptions.RemoveUnusedStreams = true;      // drop unused streams

            // Apply the optimization to the document
            doc.OptimizeResources(optOptions);

            // Save the optimized PDF using explicit PdfSaveOptions
            PdfSaveOptions saveOptions = new PdfSaveOptions();
            doc.Save(outputPath, saveOptions);
        }

        // Get optimized file size
        long optimizedSize = new FileInfo(outputPath).Length;

        // Output size comparison
        Console.WriteLine($"Original size : {originalSize} bytes");
        Console.WriteLine($"Optimized size: {optimizedSize} bytes");
        Console.WriteLine($"Size reduction: {originalSize - optimizedSize} bytes");
    }
}