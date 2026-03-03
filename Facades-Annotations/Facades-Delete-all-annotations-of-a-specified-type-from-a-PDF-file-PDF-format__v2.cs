using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPath = "input.pdf";
        // Output PDF file path
        const string outputPath = "output.pdf";
        // Annotation type to delete (e.g., "Text", "Link", "FreeText", etc.)
        const string annotationType = "Text";

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Initialize the annotation editor facade
        PdfAnnotationEditor editor = new PdfAnnotationEditor();

        // Bind the PDF document to the editor
        editor.BindPdf(inputPath);

        // Delete all annotations of the specified type
        editor.DeleteAnnotations(annotationType);

        // Save the modified PDF to the output path
        editor.Save(outputPath);

        Console.WriteLine($"Deleted all '{annotationType}' annotations and saved to '{outputPath}'.");
    }
}