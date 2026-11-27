using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "handbook_v2.pdf";
        const string outputPath = "UnderlineRemoved.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // PdfAnnotationEditor implements IDisposable, so wrap it in a using block.
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Bind the source PDF.
            editor.BindPdf(inputPath);

            // Delete all underline annotations. The DeleteAnnotations method expects the
            // annotation type name (or an array of names) as a string, not an enum array.
            editor.DeleteAnnotations("Underline");

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Underline annotations removed. Saved to '{outputPath}'.");
    }
}
