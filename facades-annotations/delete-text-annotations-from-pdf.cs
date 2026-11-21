using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_no_text_annotations.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // PdfAnnotationEditor does NOT implement IDisposable, so no using block.
            PdfAnnotationEditor editor = new PdfAnnotationEditor();

            // Load the PDF document.
            editor.BindPdf(inputPath);

            // Delete only annotations of type "Text".
            editor.DeleteAnnotations("Text");

            // Save the modified PDF.
            editor.Save(outputPath);

            Console.WriteLine($"Text annotations removed. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}