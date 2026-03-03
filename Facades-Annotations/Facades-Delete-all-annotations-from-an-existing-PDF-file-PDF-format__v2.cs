using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_no_annotations.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the annotation editor, bind the PDF, delete all annotations, and save.
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Load the PDF file.
            editor.BindPdf(inputPath);

            // Remove every annotation from the document.
            editor.DeleteAnnotations();

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"All annotations removed. Saved to '{outputPath}'.");
    }
}