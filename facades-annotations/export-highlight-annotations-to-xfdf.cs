using System;
using System.IO;
using Aspose.Pdf.Facades;          // PdfAnnotationEditor
using Aspose.Pdf.Annotations;    // for annotation type names (optional)

class ExportHighlightAnnotations
{
    static void Main()
    {
        // Input PDF and output XFDF file paths
        const string pdfPath = "input.pdf";
        const string xfdfPath = "highlights.xfdf";

        // Verify the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {pdfPath}");
            return;
        }

        try
        {
            // Create the annotation editor and bind the PDF document
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                editor.BindPdf(pdfPath);

                // Export only Highlight annotations to XFDF using the overload that
                // accepts a string[] of annotation type names.
                int startPage = 1;
                int endPage   = editor.Document.Pages.Count; // export all pages
                string[] annotTypes = new[] { "Highlight" };

                using (FileStream xfdfStream = File.Create(xfdfPath))
                {
                    editor.ExportAnnotationsXfdf(xfdfStream, startPage, endPage, annotTypes);
                }
            }

            Console.WriteLine($"Highlight annotations exported to '{xfdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during export: {ex.Message}");
        }
    }
}
