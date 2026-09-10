using System;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // -----------------------------------------------------------------
        // Prepare sample PDFs so the example is self‑contained.
        // -----------------------------------------------------------------
        const string sourcePdfPath = "source.pdf";
        const string targetPdfPath = "target.pdf";
        const string xfdfPath       = "annotations.xfdf";

        // Create a source PDF with a simple text annotation.
        using (Document sourceDoc = new Document())
        {
            Page page = sourceDoc.Pages.Add();
            // Define the rectangle for the annotation (llx, lly, urx, ury).
            var rect = new Rectangle(100, 600, 300, 650);
            var textAnn = new TextAnnotation(page, rect)
            {
                Title = "Sample",
                Subject = "Demo",
                Contents = "This is a sample annotation",
                Color = Color.Yellow
            };
            page.Annotations.Add(textAnn);
            sourceDoc.Save(sourcePdfPath);
        }

        // Create a target PDF (initially without annotations).
        using (Document targetDoc = new Document())
        {
            targetDoc.Pages.Add();
            targetDoc.Save(targetPdfPath);
        }

        // -----------------------------------------------------------------
        // Export all annotations from the source PDF to an XFDF file.
        // -----------------------------------------------------------------
        using (Document sourceDoc = new Document(sourcePdfPath))
        {
            sourceDoc.ExportAnnotationsToXfdf(xfdfPath);
        }

        // -----------------------------------------------------------------
        // Import the previously exported XFDF annotations into the target PDF.
        // -----------------------------------------------------------------
        using (Document targetDoc = new Document(targetPdfPath))
        {
            targetDoc.ImportAnnotationsFromXfdf(xfdfPath);
            // Save the updated PDF with the imported annotations.
            targetDoc.Save("target_with_annotations.pdf");
        }

        Console.WriteLine("Annotations exported to XFDF and imported into target PDF successfully.");
    }
}
