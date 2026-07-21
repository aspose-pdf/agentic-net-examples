using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations; // for AnnotationType enum

class Program
{
    static void Main()
    {
        // Path to the PDF that already contains annotations
        const string targetPdf = "target.pdf";

        // Path where the resulting PDF will be saved
        const string outputPdf = "merged_annotations.pdf";

        // PDFs that contain the new annotations to be imported
        string[] sourcePdfs = { "source1.pdf", "source2.pdf" };

        // Optional: limit import to specific annotation types
        AnnotationType[] types = {
            AnnotationType.Highlight,
            AnnotationType.Text,
            AnnotationType.Line,
            AnnotationType.Square
        };

        // Verify that all files exist before proceeding
        if (!File.Exists(targetPdf))
        {
            Console.Error.WriteLine($"Target file not found: {targetPdf}");
            return;
        }

        foreach (var src in sourcePdfs)
        {
            if (!File.Exists(src))
            {
                Console.Error.WriteLine($"Source file not found: {src}");
                return;
            }
        }

        // Use PdfAnnotationEditor to bind the target PDF, import annotations,
        // and save the result. Existing annotations are preserved because
        // ImportAnnotations adds to the document without overwriting.
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(targetPdf);                     // Load target PDF
            editor.ImportAnnotations(sourcePdfs, types);   // Import new annotations
            editor.Save(outputPdf);                         // Save merged PDF
        }

        Console.WriteLine($"Annotations imported and saved to '{outputPdf}'.");
    }
}