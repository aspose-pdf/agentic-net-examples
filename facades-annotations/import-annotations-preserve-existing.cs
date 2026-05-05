using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths to the target PDF (which already contains annotations)
        // and the source PDFs from which new annotations will be imported.
        const string targetPdfPath   = "target.pdf";
        const string outputPdfPath   = "merged_annotations.pdf";
        string[] sourcePdfPaths = { "source1.pdf", "source2.pdf" };

        // Ensure the target file exists.
        if (!File.Exists(targetPdfPath))
        {
            Console.Error.WriteLine($"Target PDF not found: {targetPdfPath}");
            return;
        }

        // Verify source files exist.
        foreach (var src in sourcePdfPaths)
        {
            if (!File.Exists(src))
            {
                Console.Error.WriteLine($"Source PDF not found: {src}");
                return;
            }
        }

        // Load the target document and bind it to PdfAnnotationEditor.
        using (Document targetDoc = new Document(targetPdfPath))
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor(targetDoc))
        {
            // Bind the document to the editor (required before any operation).
            editor.BindPdf(targetDoc);

            // Define which annotation types should be imported.
            // Adjust the list as needed; here we import common types.
            AnnotationType[] importTypes = new AnnotationType[]
            {
                AnnotationType.Highlight,
                AnnotationType.Text,
                AnnotationType.Line,
                AnnotationType.Square,
                AnnotationType.FreeText,
                AnnotationType.Ink,
                AnnotationType.Stamp,
                AnnotationType.Link
            };

            // Import annotations from the source PDFs.
            // Existing annotations in the target document are preserved
            // because we do not delete them before importing.
            editor.ImportAnnotations(sourcePdfPaths, importTypes);

            // Save the resulting PDF with both original and newly imported annotations.
            editor.Save(outputPdfPath);
        }

        Console.WriteLine($"Annotations merged successfully. Output saved to '{outputPdfPath}'.");
    }
}