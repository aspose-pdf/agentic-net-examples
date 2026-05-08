using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "flattened_output.pdf";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Log original file size
        long originalSize = new FileInfo(inputPath).Length;
        Console.WriteLine($"Original file size: {originalSize} bytes");

        try
        {
            // Use PdfAnnotationEditor to flatten annotations
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                editor.BindPdf(inputPath);               // Load the PDF
                editor.FlatteningAnnotations();          // Flatten all annotations
                editor.Save(outputPath);                  // Save the flattened PDF
            }

            // Log new file size after flattening
            long newSize = new FileInfo(outputPath).Length;
            Console.WriteLine($"Flattened file size: {newSize} bytes");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during annotation flattening: {ex.Message}");
        }
    }
}