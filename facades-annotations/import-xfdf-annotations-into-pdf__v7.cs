using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath    = "input.pdf";
        const string xfdfPath   = "annotations.xfdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(xfdfPath))
        {
            Console.Error.WriteLine($"XFDF not found: {xfdfPath}");
            return;
        }

        try
        {
            // PdfAnnotationEditor does not implement IDisposable; no using block needed.
            PdfAnnotationEditor editor = new PdfAnnotationEditor();

            // Load the PDF to which annotations will be added.
            editor.BindPdf(pdfPath);

            // Import annotations from the XFDF file. ImportAnnotations expects a string[]
            // of XFDF file paths, so we wrap the single path in an array.
            editor.ImportAnnotations(new[] { xfdfPath });

            // Save the resulting PDF with the imported annotations.
            editor.Save(outputPath);

            Console.WriteLine($"Annotations imported and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
