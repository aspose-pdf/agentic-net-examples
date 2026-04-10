using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // PdfAnnotationEditor implements IDisposable, so use a using block for deterministic cleanup
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Bind the source PDF file to the editor
            editor.BindPdf(inputPdf);

            // Delete all annotations in the document
            editor.DeleteAnnotations();

            // Save the modified PDF to a new file
            editor.Save(outputPdf);
        }

        Console.WriteLine($"All annotations removed. Saved to '{outputPdf}'.");
    }
}