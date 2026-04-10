using System;
using System.IO;
using Aspose.Pdf.Facades; // PdfAnnotationEditor resides here

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf  = "input.pdf";
        // Output PDF file path after deletion
        const string outputPdf = "output.pdf";

        // Name (or ID) of the annotation to delete – could be obtained at runtime
        string annotationName = "4cfa69cd-9bff-49e0-9005-e22a77cebf38";

        // Verify the source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Use PdfAnnotationEditor facade to manipulate annotations
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Load the PDF document into the editor
            editor.BindPdf(inputPdf);

            // Delete the annotation whose name matches the variable
            editor.DeleteAnnotation(annotationName);

            // Save the modified PDF
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Annotation \"{annotationName}\" deleted. Result saved to '{outputPdf}'.");
    }
}