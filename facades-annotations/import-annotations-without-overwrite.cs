using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the target PDF (which already contains annotations) and the output PDF.
        const string targetPdfPath = "target.pdf";
        const string outputPdfPath = "target_with_added_annotations.pdf";

        // Paths of source PDFs that contain the annotations you want to import.
        string[] sourcePdfPaths = { "source1.pdf", "source2.pdf" };

        // Verify that all files exist before proceeding.
        if (!File.Exists(targetPdfPath))
        {
            Console.Error.WriteLine($"Target PDF not found: {targetPdfPath}");
            return;
        }

        foreach (string src in sourcePdfPaths)
        {
            if (!File.Exists(src))
            {
                Console.Error.WriteLine($"Source PDF not found: {src}");
                return;
            }
        }

        // Use PdfAnnotationEditor (a facade) to work with annotations.
        // The facade implements IDisposable, so wrap it in a using block.
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Bind the existing PDF that already has annotations.
            editor.BindPdf(targetPdfPath);

            // Import annotations from the source PDFs.
            // The ImportAnnotations method adds annotations; it does NOT overwrite existing ones,
            // so the original annotations in the target PDF are preserved automatically.
            // If you need to import only specific types, use the overload that accepts
            // Aspose.Pdf.AnnotationType[] (e.g., Highlight, Text, etc.).
            editor.ImportAnnotations(sourcePdfPaths);

            // Save the resulting PDF. Existing annotations remain, and new ones are added.
            editor.Save(outputPdfPath);
        }

        Console.WriteLine($"Annotations imported successfully. Output saved to '{outputPdfPath}'.");
    }
}