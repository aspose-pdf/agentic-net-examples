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

        try
        {
            // Get original file size
            long originalSize = new FileInfo(inputPath).Length;

            // Load the PDF and save it with default compression settings
            using (Document doc = new Document(inputPath))
            {
                // Document.Save without explicit SaveOptions writes a PDF using default compression
                doc.Save(outputPath);
            }

            // Get compressed file size
            long compressedSize = new FileInfo(outputPath).Length;

            Console.WriteLine($"Original size : {originalSize} bytes");
            Console.WriteLine($"Compressed size: {compressedSize} bytes");
            Console.WriteLine($"Size reduction : {originalSize - compressedSize} bytes");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}