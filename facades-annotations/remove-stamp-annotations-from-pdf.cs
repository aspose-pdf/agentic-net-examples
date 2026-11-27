using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "agenda.pdf";
        const string outputPath = "StampsRemoved.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfAnnotationEditor does not implement IDisposable, so we instantiate it directly.
        PdfAnnotationEditor editor = new PdfAnnotationEditor();

        // Load the PDF document.
        editor.BindPdf(inputPath);

        // Remove all annotations of type "Stamp".
        editor.DeleteAnnotations("Stamp");

        // Save the resulting PDF.
        editor.Save(outputPath);

        Console.WriteLine($"Stamp annotations removed. Saved to '{outputPath}'.");
    }
}