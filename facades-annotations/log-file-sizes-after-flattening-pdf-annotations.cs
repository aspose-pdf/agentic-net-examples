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

        // Record original file size (bytes)
        long originalSize = new FileInfo(inputPath).Length;

        // Flatten all annotations using the correct Facades API method
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPath);               // Load the PDF
            editor.FlatteningAnnotations();          // Convert annotations to static page content (correct method name)
            editor.Save(outputPath);                 // Save the flattened PDF
        }

        // Record new file size after flattening
        long newSize = new FileInfo(outputPath).Length;

        // Output audit information
        Console.WriteLine($"Original file size: {originalSize} bytes");
        Console.WriteLine($"Flattened file size: {newSize} bytes");
    }
}
