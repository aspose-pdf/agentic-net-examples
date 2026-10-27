using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "flattened.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // PdfAnnotationEditor does not implement IDisposable, so we instantiate it directly.
        PdfAnnotationEditor editor = new PdfAnnotationEditor();

        // Load the PDF document.
        editor.BindPdf(inputPath);

        // Flatten all annotations into non‑editable visual elements.
        editor.FlatteningAnnotations();

        // Save the resulting PDF.
        editor.Save(outputPath);

        // Release internal resources.
        editor.Close();

        Console.WriteLine($"Flattened PDF saved to '{outputPath}'.");
    }
}
