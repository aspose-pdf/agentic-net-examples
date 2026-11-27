using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "flattened.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfAnnotationEditor provides the FlatteningAnnotations method.
        // It implements IDisposable, so we use a using block.
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Load the source PDF.
            editor.BindPdf(inputPath);

            // Flatten all annotations (including form fields) into the page content.
            editor.FlatteningAnnotations();

            // Save the result.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Annotations have been flattened and saved to '{outputPath}'.");
    }
}
