using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string annotName = "Comment1";

        // Ensure the source file exists
        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Use PdfAnnotationEditor to delete the annotation by name
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Load the PDF document
            editor.BindPdf(inputPdf);

            // Delete the annotation with the specified name
            editor.DeleteAnnotation(annotName);

            // Save the modified PDF
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Annotation \"{annotName}\" deleted. Result saved to '{outputPdf}'.");
    }
}