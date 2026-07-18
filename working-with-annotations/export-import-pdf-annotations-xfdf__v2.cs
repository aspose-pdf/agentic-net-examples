using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths for source PDF, target PDF, temporary XFDF file, and final output PDF
        const string sourcePdfPath = "source.pdf";
        const string targetPdfPath = "target.pdf";
        const string xfdfFilePath   = "annotations.xfdf";
        const string outputPdfPath  = "target_with_annotations.pdf";

        // ------------------------------------------------------------
        // Create a minimal source PDF with an annotation (so there is something to export)
        // ------------------------------------------------------------
        using (Document sourceDoc = new Document())
        {
            // Add a blank page
            sourceDoc.Pages.Add();

            // Create a simple text annotation
            TextAnnotation txtAnn = new TextAnnotation(sourceDoc.Pages[1], new Rectangle(100, 500, 300, 550))
            {
                Title = "Author",
                Subject = "Sample Comment",
                Contents = "This is a sample annotation."
            };
            sourceDoc.Pages[1].Annotations.Add(txtAnn);

            // Save the source PDF so it exists on disk for the next step
            sourceDoc.Save(sourcePdfPath);
        }

        // ------------------------------------------------------------
        // Create a minimal target PDF (blank) that will receive the annotations
        // ------------------------------------------------------------
        using (Document targetDoc = new Document())
        {
            targetDoc.Pages.Add();
            targetDoc.Save(targetPdfPath);
        }

        // ------------------------------------------------------------
        // Export all annotations from the source PDF to an XFDF file.
        // ------------------------------------------------------------
        using (Document sourceDoc = new Document(sourcePdfPath))
        {
            // ExportAnnotationsToXfdf writes the XFDF representation of all
            // annotations in the document to the specified file.
            sourceDoc.ExportAnnotationsToXfdf(xfdfFilePath);
        }

        // ------------------------------------------------------------
        // Import the previously exported XFDF into the target PDF.
        // ------------------------------------------------------------
        using (Document targetDoc = new Document(targetPdfPath))
        {
            // ImportAnnotationsFromXfdf reads the XFDF file and adds the
            // annotations to the target document.
            targetDoc.ImportAnnotationsFromXfdf(xfdfFilePath);

            // Save the modified target PDF with the imported annotations.
            targetDoc.Save(outputPdfPath);
        }

        Console.WriteLine("Annotations exported from source PDF and imported into target PDF successfully.");
    }
}
