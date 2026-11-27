using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string pdfPath = "policy.pdf";
        const string xfdfPath = "types.xfdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Page range to export (inclusive)
        int startPage = 3;
        int endPage   = 7;

        // Annotation types to export (e.g., Text and Highlight)
        AnnotationType[] types = new AnnotationType[]
        {
            AnnotationType.Text,
            AnnotationType.Highlight
        };

        // PdfAnnotationEditor implements IDisposable; ensure proper disposal
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Bind the source PDF document
            editor.BindPdf(pdfPath);

            // Export the specified annotation types from the selected page range to XFDF
            using (FileStream xfdfStream = File.Create(xfdfPath))
            {
                // Correct method name and overload
                editor.ExportAnnotationsXfdf(xfdfStream, startPage, endPage, types);
            }
        }

        Console.WriteLine($"Annotations exported to '{xfdfPath}'.");
    }
}
