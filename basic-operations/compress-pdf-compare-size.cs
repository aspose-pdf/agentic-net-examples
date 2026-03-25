using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "compressed.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Size before compression
        long originalSize = new FileInfo(inputPath).Length;

        // Load, compress with default optimization, and save
        using (Document doc = new Document(inputPath))
        {
            // Apply default resource optimization (compression, unused object removal, etc.)
            doc.OptimizeResources();
            doc.Save(outputPath);
        }

        // Size after compression
        long compressedSize = new FileInfo(outputPath).Length;

        Console.WriteLine($"Original size: {originalSize} bytes");
        Console.WriteLine($"Compressed size: {compressedSize} bytes");
        Console.WriteLine($"Size reduction: {originalSize - compressedSize} bytes");
    }
}