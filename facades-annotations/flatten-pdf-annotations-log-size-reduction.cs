using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "flattened_output.pdf";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Record original file size
        long originalSize = new FileInfo(inputPath).Length;

        // Flatten all annotations using PdfAnnotationEditor (Facades API)
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPath);               // Load the PDF
            editor.FlatteningAnnotations();          // Flatten all annotations
            editor.Save(outputPath);                  // Save the flattened PDF
        }

        // Record size after flattening
        long flattenedSize = new FileInfo(outputPath).Length;

        // Calculate reduction
        long sizeReduction = originalSize - flattenedSize;
        double reductionPercent = originalSize > 0
            ? (double)sizeReduction / originalSize * 100
            : 0;

        // Log results
        Console.WriteLine($"Original size:   {originalSize} bytes");
        Console.WriteLine($"Flattened size:  {flattenedSize} bytes");
        Console.WriteLine($"Size reduction:  {sizeReduction} bytes ({reductionPercent:F2}%)");
    }
}