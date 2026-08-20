using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";
        // Output PDF file path after deletion
        const string outputPdf = "output.pdf";
        // Annotation name to delete (could be obtained dynamically)
        string annotationName = "4cfa69cd-9bff-49e0-9005-e22a77cebf38";

        // Ensure the input file exists
        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Use PdfAnnotationEditor facade to manipulate annotations
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Load the PDF document into the editor
            editor.BindPdf(inputPdf);

            // Delete the annotation with the specified name
            editor.DeleteAnnotation(annotationName);

            // Save the modified PDF
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Annotation \"{annotationName}\" deleted. Result saved to \"{outputPdf}\".");
    }
}