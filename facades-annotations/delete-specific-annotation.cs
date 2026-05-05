using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string annotationName = "Comment1";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF, delete the annotation by name, and save the result
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPath);
            editor.DeleteAnnotation(annotationName);
            editor.Save(outputPath);
        }

        Console.WriteLine($"Deleted annotation \"{annotationName}\" and saved to \"{outputPath}\".");
    }
}