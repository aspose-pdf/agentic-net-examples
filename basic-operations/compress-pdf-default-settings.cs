using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "compressed.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Get original file size
        long originalSize = new FileInfo(inputPath).Length;

        // Load the PDF, then save it again.
        // Saving without explicit SaveOptions uses the default compression settings.
        using (Document doc = new Document(inputPath))
        {
            // Optional: linearize the document for faster web access.
            // This does not affect compression but is a harmless optimization.
            doc.Optimize();

            // Save the document with default compression.
            doc.Save(outputPath);
        }

        // Get compressed file size
        long compressedSize = new FileInfo(outputPath).Length;

        Console.WriteLine($"Original size : {originalSize} bytes");
        Console.WriteLine($"Compressed size: {compressedSize} bytes");
        Console.WriteLine($"Size reduction : {originalSize - compressedSize} bytes");
    }
}