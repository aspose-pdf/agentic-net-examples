using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfAnnotationEditor (facade) to manipulate annotations.
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Load the PDF document.
            editor.BindPdf(inputPath);

            // Delete all existing annotations.
            editor.DeleteAnnotations();

            // Flatten any remaining annotations (if any were added later).
            editor.FlatteningAnnotations();

            // Save the processed PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}