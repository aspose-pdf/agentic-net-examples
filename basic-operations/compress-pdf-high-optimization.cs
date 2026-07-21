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

        // Load the PDF document (using the recommended lifecycle pattern)
        using (Document doc = new Document(inputPath))
        {
            // Create optimization options with high compression settings
            OptimizationOptions opt = OptimizationOptions.All();
            opt.CompressObjects = true;          // compress PDF objects
            opt.SubsetFonts = true;              // embed only used glyphs
            opt.RemoveUnusedObjects = true;      // drop unused objects
            opt.RemoveUnusedStreams = true;      // drop unused streams

            // Apply the optimization to the document
            doc.OptimizeResources(opt);

            // Save the document using explicit PdfSaveOptions (required for non‑PDF formats,
            // but also a good practice for PDF to keep the pattern consistent)
            PdfSaveOptions saveOpts = new PdfSaveOptions();
            doc.Save(outputPath, saveOpts);
        }

        // Compare file sizes of the original and the compressed PDF
        long originalSize   = new FileInfo(inputPath).Length;
        long compressedSize = new FileInfo(outputPath).Length;

        Console.WriteLine($"Original size:   {originalSize} bytes");
        Console.WriteLine($"Compressed size: {compressedSize} bytes");
        if (originalSize > 0)
        {
            double reduction = (originalSize - compressedSize) * 100.0 / originalSize;
            Console.WriteLine($"Size reduction: {reduction:0.00}%");
        }
    }
}