using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "flattened_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Record original file size
        long originalSize = new FileInfo(inputPath).Length;

        // Flatten all annotations using the correct API
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Bind the source PDF to the editor
            editor.BindPdf(inputPath);

            // Flatten annotations (method name is FlatteningAnnotations)
            editor.FlatteningAnnotations();

            // Save the flattened PDF
            editor.Save(outputPath);
        }

        // Record new file size after flattening
        long newSize = new FileInfo(outputPath).Length;

        // Log the sizes for audit purposes
        Console.WriteLine($"Original file size: {originalSize} bytes");
        Console.WriteLine($"New file size after flattening: {newSize} bytes");
    }
}
