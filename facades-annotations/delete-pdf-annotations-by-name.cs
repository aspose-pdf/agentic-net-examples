using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create the annotation editor and bind it to the PDF file
        PdfAnnotationEditor editor = new PdfAnnotationEditor();
        editor.BindPdf(inputPath);

        // Delete an annotation using a string literal for its name
        editor.DeleteAnnotation("MyAnnotation");

        // Delete an annotation using a variable that holds the name
        string annotationName = "AnotherAnnotation";
        editor.DeleteAnnotation(annotationName);

        // Save the modified PDF to a new file
        editor.Save(outputPath);

        Console.WriteLine($"Annotations removed. Output saved to '{outputPath}'.");
    }
}