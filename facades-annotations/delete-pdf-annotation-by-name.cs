using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the source and destination PDF files
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        // The annotation name to be deleted (could be obtained at runtime)
        string annotationName = "my-annotation-id";

        // Verify that the source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Use PdfAnnotationEditor (Facades API) to bind, delete, and save the PDF
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Load the PDF document into the editor
            editor.BindPdf(inputPdf);

            // Delete the annotation whose name is stored in the variable
            editor.DeleteAnnotation(annotationName);

            // Save the modified PDF to a new file
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Annotation \"{annotationName}\" deleted. Output saved to \"{outputPdf}\".");
    }
}