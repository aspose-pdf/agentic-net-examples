using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations; // needed for AnnotationType enum (if you want to reference it elsewhere)

class Program
{
    static void Main()
    {
        const string pdfPath = "presentation.pdf";
        const string xfdfPath = "highlights.xfdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // PdfAnnotationEditor implements IDisposable in recent Aspose.Pdf versions.
        // Use a using‑statement to ensure resources are released automatically.
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            try
            {
                // Bind the source PDF.
                editor.BindPdf(pdfPath);

                // Export only Highlight and Underline annotations from pages 5 through 10.
                // The ExportAnnotationsXfdf overload in the current Aspose.Pdf version expects a Stream
                // for the output and a string[] for the annotation type names.
                using (FileStream xfdfStream = File.Create(xfdfPath))
                {
                    editor.ExportAnnotationsXfdf(
                        xfdfStream,               // output stream
                        5,                        // start page (1‑based)
                        10,                       // end page (inclusive)
                        new[] { "Highlight", "Underline" } // annotation type names
                    );
                }

                Console.WriteLine($"Exported highlights and underlines to '{xfdfPath}'.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error: {ex.Message}");
            }
            // No explicit UnbindPdf call is required – the using block disposes the editor.
        }
    }
}
