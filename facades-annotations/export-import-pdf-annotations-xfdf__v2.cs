using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths for source PDF (with annotations), target PDF (where annotations will be duplicated),
        // temporary XFDF file and the final output PDF.
        const string sourcePdf = "source.pdf";
        const string targetPdf = "target.pdf";
        const string xfdfPath  = "annotations.xfdf";
        const string outputPdf = "merged_annotations.pdf";

        // Verify that the input files exist.
        if (!File.Exists(sourcePdf))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdf}");
            return;
        }
        if (!File.Exists(targetPdf))
        {
            Console.Error.WriteLine($"Target PDF not found: {targetPdf}");
            return;
        }

        // -----------------------------------------------------------------
        // Export annotations from the source PDF to an XFDF file.
        // -----------------------------------------------------------------
        try
        {
            using (var srcDoc = new Document(sourcePdf))
            using (var xfdfStream = new FileStream(xfdfPath, FileMode.Create, FileAccess.Write))
            {
                // Export all annotations of the source document to XFDF.
                srcDoc.ExportAnnotationsToXfdf(xfdfStream);
            }
            Console.WriteLine($"Annotations exported to '{xfdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to export annotations: {ex.Message}");
            return;
        }

        // -----------------------------------------------------------------
        // Import the XFDF into the target PDF, then save the result.
        // -----------------------------------------------------------------
        try
        {
            // Load the target PDF where annotations will be duplicated.
            using (var targetDoc = new Document(targetPdf))
            {
                // Load XFDF content into a temporary Document.
                var xfdfDoc = new Document();
                using (var xfdfStream = File.OpenRead(xfdfPath))
                {
                    XfdfReader.ReadAnnotations(xfdfStream, xfdfDoc);
                }

                // Copy annotations page‑by‑page.
                for (int pageIndex = 1; pageIndex <= xfdfDoc.Pages.Count; pageIndex++)
                {
                    // Ensure the target document has a corresponding page.
                    if (pageIndex > targetDoc.Pages.Count)
                        continue; // or optionally add a blank page.

                    Page srcPage = xfdfDoc.Pages[pageIndex];
                    Page dstPage = targetDoc.Pages[pageIndex];

                    foreach (Annotation ann in srcPage.Annotations)
                    {
                        // Clone the annotation to avoid reference issues and add it to the destination page.
                        dstPage.Annotations.Add((Annotation)ann.Clone());
                    }
                }

                // Save the modified PDF with duplicated annotation layout.
                targetDoc.Save(outputPdf);
                Console.WriteLine($"Result saved to '{outputPdf}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to import annotations: {ex.Message}");
            return;
        }
    }
}
