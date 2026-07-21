using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "flattened_output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // PdfAnnotationEditor provides methods to work with annotations.
        // FlatteningAnnotations() converts all interactive annotations to static graphics
        // while preserving their visual appearance.
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPdf);                 // Load the source PDF
            editor.FlatteningAnnotations();          // Preserve appearance, remove interactivity
            editor.Save(outputPdf);                   // Save the result
        }

        Console.WriteLine($"Annotations flattened and saved to '{outputPdf}'.");
    }
}