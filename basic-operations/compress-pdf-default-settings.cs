using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Optimization;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "compressed.pdf";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Get original file size
        long originalSize = new FileInfo(inputPath).Length;

        // Load the PDF, apply default compression, and save
        using (Document doc = new Document(inputPath))
        {
            // Create default optimization options (enable object compression)
            OptimizationOptions opt = new OptimizationOptions
            {
                CompressObjects = true
            };

            // Optimize resources using the options
            doc.OptimizeResources(opt);

            // Save the compressed PDF
            doc.Save(outputPath);
        }

        // Get compressed file size
        long compressedSize = new FileInfo(outputPath).Length;

        // Output size comparison
        Console.WriteLine($"Original size:   {originalSize} bytes");
        Console.WriteLine($"Compressed size: {compressedSize} bytes");
        Console.WriteLine($"Size reduction:  {originalSize - compressedSize} bytes");
    }
}