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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Get original file size
        long originalSize = new FileInfo(inputPath).Length;
        Console.WriteLine($"Original size: {originalSize} bytes");

        // Load, save with default compression, and dispose properly
        using (Document doc = new Document(inputPath))
        {
            // Save using default compression settings
            doc.Save(outputPath);
        }

        // Get compressed file size
        long compressedSize = new FileInfo(outputPath).Length;
        Console.WriteLine($"Compressed size: {compressedSize} bytes");

        // Show reduction
        long reduction = originalSize - compressedSize;
        Console.WriteLine($"Size reduction: {reduction} bytes ({(originalSize == 0 ? 0 : (reduction * 100.0 / originalSize)):F2}%)");
    }
}