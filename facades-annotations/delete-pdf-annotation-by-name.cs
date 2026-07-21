using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Annotation name to delete (could be obtained dynamically)
        string annotationName = "4cfa69cd-9bff-49e0-9005-e22a77cebf38";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfAnnotationEditor works with the Facades API
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Load the PDF document
            editor.BindPdf(inputPath);

            // Delete the annotation by its name
            editor.DeleteAnnotation(annotationName);

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Deleted annotation '{annotationName}'. Output saved to '{outputPath}'.");
    }
}