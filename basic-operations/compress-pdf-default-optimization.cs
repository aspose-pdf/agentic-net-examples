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

        // Load the PDF, optimize resources (default compression), and save
        using (Document doc = new Document(inputPath))
        {
            // Optimize resources with default settings
            doc.OptimizeResources();

            // Save the compressed PDF
            doc.Save(outputPath);
        }

        // Get compressed file size
        long compressedSize = new FileInfo(outputPath).Length;

        // Output size comparison
        Console.WriteLine($"Original size:   {originalSize} bytes");
        Console.WriteLine($"Compressed size: {compressedSize} bytes");
    }
}