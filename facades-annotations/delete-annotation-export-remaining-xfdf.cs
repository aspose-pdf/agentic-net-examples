using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string pdfPath = "presentation_v1.pdf";
        const string xfdfPath = "remaining.xfdf";

        // Verify input PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {pdfPath}");
            return;
        }

        // PdfAnnotationEditor implements IDisposable, so we use a using block.
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Bind the PDF to the editor
            editor.BindPdf(pdfPath);

            // Delete the annotation with the specified name
            editor.DeleteAnnotation("Arrow1");

            // Export the remaining annotations from pages 1‑3 to XFDF.
            // Passing null for the annotation type array exports all types.
            using (FileStream xfdfStream = File.Create(xfdfPath))
            {
                editor.ExportAnnotationsXfdf(xfdfStream, 1, 3, (string[])null);
            }
        }

        Console.WriteLine($"Annotations exported to '{xfdfPath}'.");
    }
}
