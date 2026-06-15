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

        // Get original file size
        long originalSize = new FileInfo(inputPath).Length;

        // Load the PDF document inside a using block (ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Create an OptimizationOptions instance with all optimizations enabled.
            // This includes object stream compression (high compression).
            OptimizationOptions opt = OptimizationOptions.All();

            // Apply the optimization to the document.
            doc.OptimizeResources(opt);

            // Prepare PDF save options (required to use SaveOptions as per task).
            PdfSaveOptions saveOptions = new PdfSaveOptions();

            // Save the optimized document with the specified save options.
            doc.Save(outputPath, saveOptions);
        }

        // Get compressed file size
        long compressedSize = new FileInfo(outputPath).Length;

        // Output the size comparison
        Console.WriteLine($"Original size   : {originalSize} bytes");
        Console.WriteLine($"Compressed size : {compressedSize} bytes");
        Console.WriteLine($"Size reduction  : {originalSize - compressedSize} bytes");
    }
}