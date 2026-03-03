using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string annotationName = "MyAnnotation";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the annotation editor facade and bind the PDF document
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Load the PDF file into the facade
            editor.BindPdf(inputPath);

            // Delete the annotation identified by its name
            editor.DeleteAnnotation(annotationName);

            // Save the modified PDF to a new file
            editor.Save(outputPath);
        }

        Console.WriteLine($"Annotation \"{annotationName}\" deleted. Saved to \"{outputPath}\".");
    }
}