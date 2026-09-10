using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_no_annotations.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the annotation editor, bind the PDF, delete all annotations, and save.
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPath);          // Load the PDF into the editor.
            editor.DeleteAnnotations();         // Remove every annotation in the document.
            editor.Save(outputPath);            // Persist the cleaned PDF.
        }

        Console.WriteLine($"All annotations removed. Saved to '{outputPath}'.");
    }
}