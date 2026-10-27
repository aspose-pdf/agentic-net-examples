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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Record original file size
        long originalSize = new FileInfo(inputPath).Length;

        // Flatten all annotations using PdfAnnotationEditor
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Bind the source PDF (can be a file path or a Document instance)
            editor.BindPdf(inputPath);

            // Flatten the remaining annotations (correct method name)
            editor.FlatteningAnnotations();

            // Save the flattened PDF
            editor.Save(outputPath);
        }

        // Record new file size after flattening
        long newSize = new FileInfo(outputPath).Length;

        // Compute size reduction
        long reduction = originalSize - newSize;
        double reductionPercent = originalSize > 0 ? (double)reduction / originalSize * 100 : 0;

        // Log the audit information
        Console.WriteLine($"Original size: {originalSize} bytes");
        Console.WriteLine($"Flattened size: {newSize} bytes");
        Console.WriteLine($"Size reduction: {reduction} bytes ({reductionPercent:F2}%)");
    }
}