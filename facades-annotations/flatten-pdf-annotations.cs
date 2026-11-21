using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "flattened.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // PdfAnnotationEditor provides the FlatteningAnnotations method.
        // It implements IDisposable, so wrap it in a using block.
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Bind the PDF to the editor
            editor.BindPdf(inputPath);

            // Flatten all annotations into non‑editable visual elements
            editor.FlatteningAnnotations();

            // Save the flattened result
            editor.Save(outputPath);
        }

        Console.WriteLine($"Annotations flattened. Output saved to '{outputPath}'.");
    }
}