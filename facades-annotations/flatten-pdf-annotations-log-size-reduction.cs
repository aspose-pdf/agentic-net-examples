using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "flattened.pdf";

        // Ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Record original file size
        long originalSize = new FileInfo(inputPath).Length;

        // Initialize the annotation editor and bind the PDF
        PdfAnnotationEditor editor = new PdfAnnotationEditor();
        editor.BindPdf(inputPath);

        // Flatten all annotations in the document
        editor.FlatteningAnnotations();

        // Save the flattened PDF
        editor.Save(outputPath);
        editor.Close(); // Release resources held by the facade

        // Record new file size
        long newSize = new FileInfo(outputPath).Length;

        // Calculate and log size reduction
        long reductionBytes = originalSize - newSize;
        double reductionPercent = originalSize > 0
            ? (double)reductionBytes * 100.0 / originalSize
            : 0.0;

        Console.WriteLine($"Original size: {originalSize} bytes");
        Console.WriteLine($"Flattened size: {newSize} bytes");
        Console.WriteLine($"Size reduced by: {reductionBytes} bytes ({reductionPercent:0.##}%)");
    }
}