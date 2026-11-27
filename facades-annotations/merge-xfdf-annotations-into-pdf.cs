using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "handbook.pdf";
        const string outputPdf = "MergedHandbook.pdf";
        string[] xfdfFiles = { "annotations1.xfdf", "annotations2.xfdf", "annotations3.xfdf" };

        // Verify that the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Verify that all XFDF files exist
        foreach (var xfdf in xfdfFiles)
        {
            if (!File.Exists(xfdf))
            {
                Console.Error.WriteLine($"XFDF file not found: {xfdf}");
                return;
            }
        }

        // PdfAnnotationEditor does not implement IDisposable, so no using block is needed
        PdfAnnotationEditor editor = new PdfAnnotationEditor();

        // Bind the PDF to the editor
        editor.BindPdf(inputPdf);

        // Import annotations from all XFDF files in a single call (method expects a string[])
        editor.ImportAnnotations(xfdfFiles);

        // Save the resulting PDF with merged annotations
        editor.Save(outputPdf);

        Console.WriteLine($"Merged PDF saved to '{outputPdf}'.");
    }
}
