using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string targetPdf = "target.pdf";
        const string outputPdf = "merged_annotations.pdf";

        // Source PDFs that contain the annotations you want to add
        string[] sourcePdfs = { "source1.pdf", "source2.pdf" };

        // Choose which annotation types to import (optional)
        AnnotationType[] types = { AnnotationType.Highlight, AnnotationType.Text };

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

        // Bind the target PDF and import annotations.
        // ImportAnnotations adds new annotations without overwriting existing ones,
        // thereby preserving any annotations already present in the target document.
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(targetPdf);
            editor.ImportAnnotations(sourcePdfs, types);
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Annotations imported successfully. Output saved to '{outputPdf}'.");
    }
}