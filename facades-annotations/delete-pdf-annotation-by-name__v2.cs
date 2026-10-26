using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the source PDF and the output PDF
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Name of the annotation to delete (could come from elsewhere)
        string annotationName = "MyAnnotation";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create the Facade editor for annotations
        PdfAnnotationEditor editor = new PdfAnnotationEditor();

        // Bind the editor to the existing PDF file
        editor.BindPdf(inputPath);

        // Delete the annotation whose name matches the variable
        editor.DeleteAnnotation(annotationName);

        // Save the modified PDF to a new file
        editor.Save(outputPath);

        Console.WriteLine($"Annotation '{annotationName}' deleted. Saved to '{outputPath}'.");
    }
}