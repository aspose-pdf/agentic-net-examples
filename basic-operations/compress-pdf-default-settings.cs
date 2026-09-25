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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Record original file size
        long originalSize = new FileInfo(inputPath).Length;

        // Load the PDF and save it using default compression settings
        using (Document doc = new Document(inputPath))
        {
            // No SaveOptions are specified; default compression is applied
            doc.Save(outputPath);
        }

        // Record compressed file size
        long compressedSize = new FileInfo(outputPath).Length;

        // Output size comparison
        Console.WriteLine($"Original size:   {originalSize} bytes");
        Console.WriteLine($"Compressed size: {compressedSize} bytes");
        Console.WriteLine($"Reduction:       {originalSize - compressedSize} bytes");
    }
}