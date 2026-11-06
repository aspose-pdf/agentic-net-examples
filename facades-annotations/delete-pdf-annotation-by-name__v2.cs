using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the source PDF and the output PDF
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        // Name of the annotation to delete (could be obtained at runtime)
        string annotationName = "MyAnnotation";

        // Verify the source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdf}");
            return;
        }

        // Create the annotation editor (no IDisposable, so no using block needed)
        PdfAnnotationEditor editor = new PdfAnnotationEditor();

        // Load the PDF into the editor
        editor.BindPdf(inputPdf);

        // Delete the annotation whose name matches the variable
        editor.DeleteAnnotation(annotationName);

        // Save the modified PDF
        editor.Save(outputPdf);

        Console.WriteLine($"Annotation \"{annotationName}\" deleted. Saved to '{outputPdf}'.");
    }
}