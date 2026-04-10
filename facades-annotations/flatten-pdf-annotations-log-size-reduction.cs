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

        // Record original file size
        long originalSize = new FileInfo(inputPath).Length;

        // Flatten all annotations using PdfAnnotationEditor
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPath);                 // Load the PDF
            editor.FlatteningAnnotations();            // Flatten all annotations
            editor.Save(outputPath);                    // Save the result
        }

        // Record new file size
        long newSize = new FileInfo(outputPath).Length;

        // Calculate and log size reduction
        long reduction = originalSize - newSize;
        double percent = originalSize > 0 ? (reduction * 100.0 / originalSize) : 0;

        Console.WriteLine($"Original size: {originalSize:N0} bytes");
        Console.WriteLine($"Flattened size: {newSize:N0} bytes");
        Console.WriteLine($"Size reduction: {reduction:N0} bytes ({percent:F2}%)");
    }
}