using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_no_annotations.pdf";

        // Verify that the source PDF exists
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create the annotation editor facade and bind the PDF
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPath);          // Load the PDF into the editor
            editor.DeleteAnnotations();         // Remove every annotation
            editor.Save(outputPath);            // Persist the cleaned PDF
        }

        Console.WriteLine($"All annotations deleted. Saved to '{outputPath}'.");
    }
}