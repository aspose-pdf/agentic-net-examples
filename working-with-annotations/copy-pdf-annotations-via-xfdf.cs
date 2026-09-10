using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Define file names used in the demo
        const string sourcePdfPath = "source.pdf";
        const string targetPdfPath = "target.pdf";
        const string xfdfFilePath   = "annotations.xfdf";
        const string outputPdfPath  = "target_with_annotations.pdf";

        // ---------------------------------------------------------------------
        // 1. Create a source PDF that contains at least one annotation.
        // ---------------------------------------------------------------------
        using (Document sourceDoc = new Document())
        {
            // Add a blank page.
            Page page = sourceDoc.Pages.Add();

            // Create a simple text annotation (a comment).
            var rect = new Rectangle(100, 600, 300, 650); // left, bottom, right, top
            var textAnn = new TextAnnotation(page, rect)
            {
                Title = "Reviewer",
                Subject = "Comment",
                Contents = "This is a sample comment transferred via XFDF."
            };
            page.Annotations.Add(textAnn);

            // Save the source PDF so it exists on disk for the later export step.
            sourceDoc.Save(sourcePdfPath);
        }

        // ---------------------------------------------------------------------
        // 2. Create a target PDF that will receive the annotations.
        // ---------------------------------------------------------------------
        using (Document targetDoc = new Document())
        {
            // Add a blank page – the same size as the source for visual consistency.
            targetDoc.Pages.Add();
            targetDoc.Save(targetPdfPath);
        }

        // ---------------------------------------------------------------------
        // 3. Export all annotations from the source PDF to an XFDF file.
        // ---------------------------------------------------------------------
        using (Document sourceDoc = new Document(sourcePdfPath))
        {
            sourceDoc.ExportAnnotationsToXfdf(xfdfFilePath);
        }

        // ---------------------------------------------------------------------
        // 4. Import the XFDF annotations into the target PDF and save the result.
        // ---------------------------------------------------------------------
        using (Document targetDoc = new Document(targetPdfPath))
        {
            targetDoc.ImportAnnotationsFromXfdf(xfdfFilePath);
            targetDoc.Save(outputPdfPath);
        }

        Console.WriteLine("Annotations have been copied from the source PDF to the target PDF.");
    }
}
