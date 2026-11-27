using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "catalog.pdf";
        const string xfdfPath = "widgets.xfdf";
        const int startPage = 10;
        const int endPage = 15;

        // Verify input PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {pdfPath}");
            return;
        }

        try
        {
            // PdfAnnotationEditor does NOT implement IDisposable, so no using block for it
            PdfAnnotationEditor editor = new PdfAnnotationEditor();

            // Bind the PDF document to the editor
            editor.BindPdf(pdfPath);

            // Export widget annotations (form fields) from the specified page range to XFDF
            // The ExportAnnotationsXfdf overload requires a destination stream, page range, and an array of annotation type names.
            // "Widget" is the type name for form field annotations.
            using (FileStream xfdfStream = File.Create(xfdfPath))
            {
                string[] annotationTypes = new[] { "Widget" };
                editor.ExportAnnotationsXfdf(xfdfStream, startPage, endPage, annotationTypes);
            }

            Console.WriteLine($"Widget annotations from pages {startPage}-{endPage} exported to '{xfdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
