using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "flattened.pdf";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Record original file size
        long originalSize = new FileInfo(inputPath).Length;

        // Flatten annotations using PdfAnnotationEditor (Facades API)
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPath);          // Load the PDF
            editor.FlatteningAnnotations();     // Correct method to flatten annotations
            editor.Save(outputPath);            // Save the flattened PDF
        }

        // Record new file size
        long flattenedSize = new FileInfo(outputPath).Length;

        // Calculate reduction
        long sizeReduction = originalSize - flattenedSize;
        double percentReduction = originalSize > 0
            ? (sizeReduction / (double)originalSize) * 100
            : 0;

        // Log the results
        Console.WriteLine($"Original size : {originalSize:N0} bytes");
        Console.WriteLine($"Flattened size: {flattenedSize:N0} bytes");
        Console.WriteLine($"Size reduction: {sizeReduction:N0} bytes ({percentReduction:F2}%)");
    }
}