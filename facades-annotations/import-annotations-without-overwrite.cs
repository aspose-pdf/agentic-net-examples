using System;
using System.IO;
using Aspose.Pdf.Facades;          // PdfAnnotationEditor
using Aspose.Pdf.Annotations;      // AnnotationType enum

class Program
{
    static void Main()
    {
        // Paths for the target PDF (which already contains annotations)
        const string targetPdfPath = "target.pdf";

        // Paths for source PDFs that contain additional annotations to import
        string[] sourcePdfPaths = { "source1.pdf", "source2.pdf" };

        // Output PDF path – will contain both original and newly imported annotations
        const string outputPdfPath = "merged_annotations.pdf";

        // Verify that the target PDF exists
        if (!File.Exists(targetPdfPath))
        {
            Console.Error.WriteLine($"Target PDF not found: {targetPdfPath}");
            return;
        }

        // Verify that each source PDF exists
        foreach (string src in sourcePdfPaths)
        {
            if (!File.Exists(src))
            {
                Console.Error.WriteLine($"Source PDF not found: {src}");
                return;
            }
        }

        // Use PdfAnnotationEditor (facade) to work with annotations.
        // The class implements IDisposable via the base SaveableFacade, so wrap it in a using block.
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Bind the target PDF – this loads the document on which we will operate.
            editor.BindPdf(targetPdfPath);

            // Import annotations from the source PDFs.
            // ImportAnnotations adds annotations without removing existing ones,
            // effectively preserving the original annotations.
            editor.ImportAnnotations(sourcePdfPaths);

            // Save the modified document to a new file.
            editor.Save(outputPdfPath);

            // Close the facade to release resources (optional because of using).
            editor.Close();
        }

        Console.WriteLine($"Annotations merged successfully. Output saved to '{outputPdfPath}'.");
    }
}