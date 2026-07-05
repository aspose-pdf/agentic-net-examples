using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string annotationName = "Comment1";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Delete the annotation named "Comment1" using PdfAnnotationEditor
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPath);                 // Load the PDF
            editor.DeleteAnnotation(annotationName);    // Remove the specific annotation
            editor.Save(outputPath);                    // Save the modified PDF
            editor.Close();                            // Release resources (optional, handled by using)
        }

        Console.WriteLine($"Annotation '{annotationName}' deleted. Saved to '{outputPath}'.");
    }
}