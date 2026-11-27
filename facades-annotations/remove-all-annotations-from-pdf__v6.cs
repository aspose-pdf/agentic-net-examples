using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "document.pdf";
        const string outputPath = "Cleaned.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfAnnotationEditor does not implement IDisposable, so we instantiate it directly.
        PdfAnnotationEditor editor = new PdfAnnotationEditor();

        // Bind the source PDF.
        editor.BindPdf(inputPath);

        // Remove all annotations from the document.
        editor.DeleteAnnotations();

        // Save the cleaned PDF.
        editor.Save(outputPath);

        Console.WriteLine($"Annotations removed. Saved to '{outputPath}'.");
    }
}