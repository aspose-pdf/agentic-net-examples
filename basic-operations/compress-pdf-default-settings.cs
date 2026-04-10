using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "compressed_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Get original file size
        long originalSize = new FileInfo(inputPath).Length;
        Console.WriteLine($"Original size: {originalSize} bytes");

        // Load, compress, and save the PDF
        using (Document doc = new Document(inputPath))
        {
            // Apply default resource optimization (compression)
            doc.OptimizeResources();

            // Save the compressed PDF
            doc.Save(outputPath);
        }

        // Get compressed file size
        long compressedSize = new FileInfo(outputPath).Length;
        Console.WriteLine($"Compressed size: {compressedSize} bytes");

        // Show size reduction
        long reduction = originalSize - compressedSize;
        double percent = originalSize > 0 ? (double)reduction / originalSize * 100 : 0;
        Console.WriteLine($"Size reduced by {reduction} bytes ({percent:F2}%).");
    }
}