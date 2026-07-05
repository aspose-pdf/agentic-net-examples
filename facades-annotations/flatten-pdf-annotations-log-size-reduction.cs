using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "flattened_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Get original file size
        long originalSize = new FileInfo(inputPath).Length;

        // Flatten annotations using PdfAnnotationEditor
        PdfAnnotationEditor editor = new PdfAnnotationEditor();
        editor.BindPdf(inputPath);                 // Load the PDF
        editor.FlatteningAnnotations();            // Flatten all annotations
        editor.Save(outputPath);                    // Save the flattened PDF
        editor.Close();                            // Release resources

        // Get flattened file size
        long flattenedSize = new FileInfo(outputPath).Length;

        // Calculate reduction
        long sizeReduction = originalSize - flattenedSize;
        double percentReduction = originalSize > 0
            ? (sizeReduction / (double)originalSize) * 100.0
            : 0.0;

        // Log the results
        Console.WriteLine($"Original size : {originalSize} bytes");
        Console.WriteLine($"Flattened size: {flattenedSize} bytes");
        Console.WriteLine($"Size reduction : {sizeReduction} bytes ({percentReduction:F2}%)");
    }
}