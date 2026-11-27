using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "draft_v2.pdf";
        const string outputPath = "NoStamps.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // PdfAnnotationEditor does NOT implement IDisposable, so no using block.
            PdfAnnotationEditor editor = new PdfAnnotationEditor();

            // Load the PDF document.
            editor.BindPdf(inputPath);

            // Delete all annotations of type "Stamp".
            editor.DeleteAnnotations("Stamp");

            // Save the modified PDF.
            editor.Save(outputPath);

            Console.WriteLine($"Stamps removed. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}