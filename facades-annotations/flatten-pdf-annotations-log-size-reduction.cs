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

        // Initialize the annotation editor and bind the PDF
        PdfAnnotationEditor editor = new PdfAnnotationEditor();
        editor.BindPdf(inputPath);

        // Flatten all annotations in the document
        editor.FlatteningAnnotations();

        // Save the flattened document
        editor.Save(outputPath);

        // Release resources held by the editor
        editor.Close();
        editor.Dispose();

        // Record new file size
        long newSize = new FileInfo(outputPath).Length;

        // Calculate reduction
        long reductionBytes = originalSize - newSize;
        double reductionPercent = originalSize > 0
            ? (double)reductionBytes / originalSize * 100
            : 0;

        // Log the results
        Console.WriteLine($"Original size: {originalSize:N0} bytes");
        Console.WriteLine($"Flattened size: {newSize:N0} bytes");
        Console.WriteLine($"Size reduction: {reductionBytes:N0} bytes ({reductionPercent:F2}%)");
    }
}