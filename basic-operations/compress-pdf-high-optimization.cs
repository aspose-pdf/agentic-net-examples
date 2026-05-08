using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Optimization;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "compressed_high.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the source PDF inside a using block (document-disposal-with-using rule)
        using (Document doc = new Document(inputPath))
        {
            // Apply aggressive optimization before saving.
            // This uses OptimizationOptions to enable object compression, font subsetting, etc.
            OptimizationOptions opt = new OptimizationOptions
            {
                CompressObjects = true,      // pack objects into streams and compress them
                SubsetFonts    = true,      // embed only used glyphs
                RemoveUnusedObjects = true, // drop objects not referenced by any page
                RemoveUnusedStreams = true,
                LinkDuplicateStreams = true
            };
            doc.OptimizeResources(opt);

            // Create PDF save options (all-save-options-in-aspose-pdf-namespace rule)
            PdfSaveOptions saveOptions = new PdfSaveOptions();

            // Save the optimized PDF using Save(string, SaveOptions) (save-to-non-pdf-always-use-save-options rule)
            doc.Save(outputPath, saveOptions);
        }

        // Compare file sizes
        long originalSize = new FileInfo(inputPath).Length;
        long compressedSize = new FileInfo(outputPath).Length;

        Console.WriteLine($"Original size : {originalSize:N0} bytes");
        Console.WriteLine($"Compressed size: {compressedSize:N0} bytes");
        Console.WriteLine($"Size reduction : {originalSize - compressedSize:N0} bytes ({(originalSize - compressedSize) * 100.0 / originalSize:0.##}% )");
    }
}