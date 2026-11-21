using System;
using System.IO;
using Aspose.Pdf.Facades;

class DeleteAnnotationsExample
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the Facade that works with PDF annotations
        PdfAnnotationEditor editor = new PdfAnnotationEditor();

        // Load the PDF document into the editor
        editor.BindPdf(inputPath);

        // Delete an annotation by specifying its name directly as a string literal
        editor.DeleteAnnotation("MyAnnotation");

        // Delete an annotation by using a variable that holds the annotation name
        string annotationName = "AnotherAnnotation";
        editor.DeleteAnnotation(annotationName);

        // Persist the changes to a new file
        editor.Save(outputPath);

        Console.WriteLine($"Annotations removed. Output saved to '{outputPath}'.");
    }
}