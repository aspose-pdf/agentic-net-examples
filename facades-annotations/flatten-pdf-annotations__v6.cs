using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "flattened_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfAnnotationEditor implements IDisposable, so use a using block.
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Bind the editor to the source PDF.
            editor.BindPdf(inputPath);

            // Flatten all annotations into the page content.
            // The correct method name is FlatteningAnnotations().
            // PdfAnnotationEditor does not expose a FlattenSettings property, and the flattening operation does not trigger JavaScript events.
            editor.FlatteningAnnotations();

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Annotations flattened and saved to '{outputPath}'.");
    }
}
