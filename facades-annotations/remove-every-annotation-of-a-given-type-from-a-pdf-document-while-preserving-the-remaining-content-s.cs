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
        // Annotation type to remove (e.g., "Text", "Highlight", "FreeText", etc.)
        const string annotationType = "Text";

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create the annotation editor (lifecycle: create)
        PdfAnnotationEditor editor = new PdfAnnotationEditor();

        // Load the PDF document into the editor (lifecycle: load)
        editor.BindPdf(inputPath);

        // Delete all annotations of the specified type (lifecycle: operation)
        editor.DeleteAnnotations(annotationType);

        // Save the modified PDF (lifecycle: save)
        editor.Save(outputPath);

        // Release resources held by the editor
        editor.Close();

        Console.WriteLine($"All '{annotationType}' annotations have been removed and saved to '{outputPath}'.");
    }
}