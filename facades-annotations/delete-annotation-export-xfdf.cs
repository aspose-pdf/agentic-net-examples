using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "invoice.pdf";
        const string outputXfdf = "single.xfdf";
        const string annotationName = "Signature1";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        try
        {
            // PdfAnnotationEditor implements IDisposable, so wrap it in a using block
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                // Bind the PDF document to the editor
                editor.BindPdf(pdfPath);

                // Delete the annotation named "Signature1" (document‑wide). If it does not exist, ignore.
                try
                {
                    editor.DeleteAnnotation(annotationName);
                }
                catch (ArgumentException)
                {
                    // Annotation with the given name was not found – continue silently.
                }

                // Export the remaining annotations from page 1 to an XFDF file.
                // ExportAnnotationsXfdf(Stream, startPage, endPage, string[] annotationTypes)
                // Passing null for annotationTypes exports all annotation types.
                using (FileStream xfdfStream = File.Create(outputXfdf))
                {
                    editor.ExportAnnotationsXfdf(xfdfStream, 1, 1, (string[])null);
                }
            }

            Console.WriteLine($"Remaining annotations exported to '{outputXfdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
