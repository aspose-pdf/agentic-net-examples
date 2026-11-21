using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Optimization;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string defaultPath = "output_default.pdf";
        const string highCompPath = "output_high_compression.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Save with default settings (no explicit optimization)
        using (Document doc = new Document(inputPath))
        {
            doc.Save(defaultPath);
        }

        // Save with high compression using OptimizationOptions
        using (Document highDoc = new Document(inputPath))
        {
            // Configure optimization for high compression
            OptimizationOptions opt = OptimizationOptions.All();
            opt.CompressImages = true;          // compress images
            opt.ImageQuality = 50;              // lower quality to increase compression (adjust as needed)
            opt.RemoveUnusedObjects = true;    // clean up unused objects
            opt.RemoveUnusedStreams = true;    // clean up unused streams

            // Apply the optimization to the document
            highDoc.OptimizeResources(opt);

            // Save the optimized (high‑compression) PDF
            highDoc.Save(highCompPath);
        }

        // Compare file sizes of the two output PDFs
        FileInfo defaultInfo = new FileInfo(defaultPath);
        FileInfo highInfo = new FileInfo(highCompPath);

        Console.WriteLine($"Default PDF size: {defaultInfo.Length} bytes");
        Console.WriteLine($"High‑compression PDF size: {highInfo.Length} bytes");

        if (highInfo.Length < defaultInfo.Length)
            Console.WriteLine("High compression reduced the file size.");
        else if (highInfo.Length > defaultInfo.Length)
            Console.WriteLine("High compression increased the file size (unexpected).");
        else
            Console.WriteLine("File sizes are identical.");
    }
}
