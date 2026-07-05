using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Ensure the source file exists
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfAnnotationEditor implements IDisposable via SaveableFacade,
        // so wrap it in a using block for deterministic cleanup.
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Load the PDF document into the facade.
            editor.BindPdf(inputPath);

            // Remove all annotations from the document.
            editor.DeleteAnnotations();

            // Flatten any remaining annotations (if any were added later).
            editor.FlatteningAnnotations();

            // Save the processed PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}