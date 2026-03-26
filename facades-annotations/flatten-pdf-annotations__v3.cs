using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "flattened.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Record original file size
        FileInfo originalInfo = new FileInfo(inputPath);
        long originalSize = originalInfo.Length;

        using (Document doc = new Document(inputPath))
        {
            // Flatten all annotations (and form fields) into the page content
            doc.Flatten();

            // Save the flattened document
            doc.Save(outputPath);
        }

        // Record new file size after flattening
        FileInfo newInfo = new FileInfo(outputPath);
        long newSize = newInfo.Length;

        long reduction = originalSize - newSize;
        double reductionPercent = originalSize > 0 ? (double)reduction / originalSize * 100 : 0;

        Console.WriteLine($"Original size: {originalSize} bytes");
        Console.WriteLine($"Flattened size: {newSize} bytes");
        Console.WriteLine($"Size reduction: {reduction} bytes ({reductionPercent:F2}%)");
    }
}
