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

        long originalSize = new FileInfo(inputPath).Length;

        using (Document doc = new Document(inputPath))
        {
            // Apply high compression using optimization options
            OptimizationOptions opt = OptimizationOptions.All();
            opt.CompressObjects = true;          // compress object streams
            opt.SubsetFonts = true;              // embed only used glyphs
            opt.RemoveUnusedObjects = true;      // drop unused objects
            opt.RemoveUnusedStreams = true;      // drop unused streams

            doc.OptimizeResources(opt);

            // Save with PDF save options
            PdfSaveOptions saveOpts = new PdfSaveOptions();
            doc.Save(outputPath, saveOpts);
        }

        long compressedSize = new FileInfo(outputPath).Length;

        Console.WriteLine($"Original size: {originalSize} bytes");
        Console.WriteLine($"Compressed size: {compressedSize} bytes");
        Console.WriteLine($"Size reduction: {originalSize - compressedSize} bytes");
    }
}