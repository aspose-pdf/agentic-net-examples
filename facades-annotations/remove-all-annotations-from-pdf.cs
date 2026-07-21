using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_no_annotations.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // PdfAnnotationEditor implements IDisposable via SaveableFacade, so use a using block.
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Bind the existing PDF document.
            editor.BindPdf(inputPath);

            // Delete all annotations in the document.
            editor.DeleteAnnotations();

            // Save the modified PDF to a new file.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Annotations removed. Saved to '{outputPath}'.");
    }
}