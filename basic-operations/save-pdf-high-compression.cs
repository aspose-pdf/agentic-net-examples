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
        using (Document doc = new Document(inputPath))
        {
            // Configure optimization for high compression
            OptimizationOptions opt = new OptimizationOptions();
            opt.CompressObjects = true;            // Compress object streams
            opt.SubsetFonts = true;                // Subset embedded fonts
            opt.RemoveUnusedObjects = true;        // Remove objects not referenced
            opt.RemoveUnusedStreams = true;        // Remove unused resource streams
            opt.LinkDuplicateStreams = true;       // Merge duplicate streams

            // Apply the optimization to the document
            doc.OptimizeResources(opt);

            // Save the document with default PDF save options
            PdfSaveOptions saveOpts = new PdfSaveOptions();
            doc.Save(outputPath, saveOpts);
        }

        // Compare file sizes
        long originalSize = new FileInfo(inputPath).Length;
        long compressedSize = new FileInfo(outputPath).Length;
        Console.WriteLine($"Original size: {originalSize} bytes");
        Console.WriteLine($"Compressed size: {compressedSize} bytes");
        long reduction = originalSize - compressedSize;
        double percent = originalSize > 0 ? (double)reduction * 100 / originalSize : 0;
        Console.WriteLine($"Reduction: {reduction} bytes ({percent:0.##}%)");
    }
}