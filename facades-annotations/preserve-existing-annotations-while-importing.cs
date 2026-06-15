using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the target PDF (which already contains annotations)
        // and the source PDFs that hold the new annotations to be imported.
        const string targetPdfPath   = "target.pdf";
        const string outputPdfPath   = "merged_preserve_annotations.pdf";
        string[] sourcePdfPaths = { "source1.pdf", "source2.pdf" };

        // Verify that all files exist before proceeding.
        if (!File.Exists(targetPdfPath))
        {
            Console.Error.WriteLine($"Target PDF not found: {targetPdfPath}");
            return;
        }
        foreach (var src in sourcePdfPaths)
        {
            if (!File.Exists(src))
            {
                Console.Error.WriteLine($"Source PDF not found: {src}");
                return;
            }
        }

        // PdfAnnotationEditor implements SaveableFacade, which is IDisposable.
        // Use a using block to ensure proper resource cleanup.
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Bind the target PDF that already contains annotations.
            editor.BindPdf(targetPdfPath);

            // Disable overwriting of existing annotations.
            // The property name may vary by version; if unavailable, the default
            // behavior preserves existing annotations.
            // Uncomment the line below if the property exists in your version.
            // editor.OverwriteExistingAnnotations = false;

            // Import annotations from the source PDFs.
            // This overload imports all annotation types.
            editor.ImportAnnotations(sourcePdfPaths);

            // Save the resulting PDF. Existing annotations are retained,
            // and new ones from the source PDFs are added.
            editor.Save(outputPdfPath);
        }

        Console.WriteLine($"Annotations merged successfully. Output saved to '{outputPdfPath}'.");
    }
}