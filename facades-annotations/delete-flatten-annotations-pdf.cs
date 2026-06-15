using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Delete all annotations, flatten any remaining ones, and save the result
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPath);          // Load the PDF
            editor.DeleteAnnotations();         // Remove all annotations
            editor.FlatteningAnnotations();    // Flatten any remaining annotations (if any)
            editor.Save(outputPath);            // Save the processed PDF
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}