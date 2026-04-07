using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths for source PDF, XFDF file, and destination PDF
        const string sourcePdfPath = "source.pdf";
        const string xfdfPath      = "annotations.xfdf";
        const string targetPdfPath = "target.pdf";
        const string outputPdfPath = "target_with_annotations.pdf";

        // ------------------------------------------------------------
        // Ensure the source PDF exists and contains at least one annotation.
        // If the file is missing we create a minimal PDF with a TextAnnotation.
        // ------------------------------------------------------------
        if (!File.Exists(sourcePdfPath))
        {
            using (Document srcDoc = new Document())
            {
                Page page = srcDoc.Pages.Add();
                // Create a simple text annotation.
                var textAnnotation = new TextAnnotation(
                    srcDoc.Pages[1],
                    new Rectangle(100, 600, 200, 650) // left, bottom, right, top
                )
                {
                    Title    = "Sample",
                    Subject  = "Demo",
                    Contents = "This is a sample annotation created programmatically."
                };
                page.Annotations.Add(textAnnotation);
                srcDoc.Save(sourcePdfPath);
            }
        }

        // ------------------------------------------------------------
        // Ensure the target PDF exists. If it does not, create a blank PDF.
        // ------------------------------------------------------------
        if (!File.Exists(targetPdfPath))
        {
            using (Document tgtDoc = new Document())
            {
                tgtDoc.Pages.Add();
                tgtDoc.Save(targetPdfPath);
            }
        }

        // ------------------------------------------------------------
        // Export annotations from the source PDF to an XFDF file.
        // ------------------------------------------------------------
        using (Document sourceDoc = new Document(sourcePdfPath))
        {
            sourceDoc.ExportAnnotationsToXfdf(xfdfPath);
        }

        // ------------------------------------------------------------
        // Import the XFDF annotations into the target PDF and save the result.
        // ------------------------------------------------------------
        using (Document targetDoc = new Document(targetPdfPath))
        {
            targetDoc.ImportAnnotationsFromXfdf(xfdfPath);
            targetDoc.Save(outputPdfPath);
        }

        Console.WriteLine("Annotations exported from source PDF and imported into target PDF successfully.");
    }
}
