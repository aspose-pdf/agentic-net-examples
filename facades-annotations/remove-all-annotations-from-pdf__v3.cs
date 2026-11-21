using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output_clean.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // PdfAnnotationEditor supports IDisposable in recent Aspose.Pdf versions, so we use a using block.
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Bind the existing PDF document.
            editor.BindPdf(inputPdf);

            // Remove every annotation from the document.
            editor.DeleteAnnotations();

            // Save the cleaned PDF to a new file.
            editor.Save(outputPdf);
        }

        Console.WriteLine($"All annotations removed. Saved to '{outputPdf}'.");
    }
}
