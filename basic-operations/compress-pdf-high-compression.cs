using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Optimization;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "compressed_high.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the source PDF inside a using block (lifecycle rule)
        using (Document doc = new Document(inputPath))
        {
            // Create an OptimizationOptions instance with high compression settings
            OptimizationOptions opt = new OptimizationOptions
            {
                // Pack PDF objects into streams and compress them
                CompressObjects = true,
                // Subset fonts to keep only used glyphs
                SubsetFonts = true,
                // Remove unused objects and streams
                RemoveUnusedObjects = true,
                RemoveUnusedStreams = true
            };

            // Configure image compression on the existing ImageCompressionOptions instance
            opt.ImageCompressionOptions.CompressImages = true;
            opt.ImageCompressionOptions.ImageQuality = 50; // lower value = higher compression
            // Optional: choose a compression encoding (e.g., Flate)
            // opt.ImageCompressionOptions.Encoding = ImageEncoding.Flate;

            // Apply the optimization to the document
            doc.OptimizeResources(opt);

            // Prepare PdfSaveOptions (required when saving with explicit options)
            PdfSaveOptions saveOptions = new PdfSaveOptions();

            // Save the optimized PDF
            doc.Save(outputPath, saveOptions);
        }

        // Compare file sizes
        long originalSize = new FileInfo(inputPath).Length;
        long compressedSize = new FileInfo(outputPath).Length;

        Console.WriteLine($"Original size   : {originalSize:N0} bytes");
        Console.WriteLine($"Compressed size : {compressedSize:N0} bytes");
        Console.WriteLine($"Size reduction  : {originalSize - compressedSize:N0} bytes");
    }
}