using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Annotations; // RichMediaAnnotation resides here
using Aspose.Pdf; // for AnnotationFlags

class Program
{
    static void Main()
    {
        const string sourcePdfPath = "source.pdf";          // PDF to embed
        const string targetPdfPath = "target.pdf";          // PDF to annotate
        const string outputPdfPath = "output.pdf";          // Resulting PDF

        // Ensure source files exist
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }
        if (!File.Exists(targetPdfPath))
        {
            Console.Error.WriteLine($"Target PDF not found: {targetPdfPath}");
            return;
        }

        // Load the PDF that will receive the annotation
        using (Document targetDoc = new Document(targetPdfPath))
        {
            // Choose the page where the annotation will be placed (first page, 1‑based index)
            Page page = targetDoc.Pages[1];

            // Define the rectangle for the annotation (coordinates in points)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);

            // Create the RichMediaAnnotation
            RichMediaAnnotation richMedia = new RichMediaAnnotation(page, rect)
            {
                // Make the annotation read‑only and prevent user interaction
                Flags = AnnotationFlags.ReadOnly | AnnotationFlags.LockedContents,
                // Optional: prevent activation by any event (if the enum provides a None value)
                // ActivateOn = RichMediaAnnotation.ActivationEvent.None
            };

            // Embed the source PDF as a stream inside the annotation
            using (FileStream embedStream = File.OpenRead(sourcePdfPath))
            {
                // The first parameter is a name for the embedded content (can be any string)
                richMedia.SetContent("EmbeddedPdf", embedStream);
            }

            // Add the annotation to the page
            page.Annotations.Add(richMedia);

            // Save the modified document
            targetDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"RichMediaAnnotation added and saved to '{outputPdfPath}'.");
    }
}