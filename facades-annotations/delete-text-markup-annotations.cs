using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "report2021.pdf";
        const string outputPath = "CleanMarkup.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfAnnotationEditor does not implement IDisposable; instantiate directly.
        PdfAnnotationEditor editor = new PdfAnnotationEditor();

        // Load the PDF to be processed.
        editor.BindPdf(inputPath);

        // Remove all text markup annotations (e.g., highlights, underlines, strikeouts).
        editor.DeleteAnnotations("TextMarkup");

        // Save the resulting PDF without the markup annotations.
        editor.Save(outputPath);

        Console.WriteLine($"Clean PDF saved to '{outputPath}'.");
    }
}