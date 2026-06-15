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

        // Flatten all annotations using PdfAnnotationEditor
        PdfAnnotationEditor editor = new PdfAnnotationEditor();
        editor.BindPdf(inputPath);                 // Load the PDF
        editor.FlatteningAnnotations();            // Flatten annotations
        editor.Save(outputPath);                   // Save the flattened PDF
        editor.Close();                            // Release resources

        // Log new file size after flattening
        long newSize = new FileInfo(outputPath).Length;
        Console.WriteLine($"New file size after flattening: {newSize} bytes");
    }
}