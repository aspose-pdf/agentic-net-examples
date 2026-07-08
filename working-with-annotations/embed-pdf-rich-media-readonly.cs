using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string sourcePdfPath   = "source.pdf";      // PDF to which the annotation will be added
        const string embedPdfPath    = "embedded.pdf";    // PDF to embed as rich media
        const string outputPdfPath   = "output.pdf";

        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }

        if (!File.Exists(embedPdfPath))
        {
            Console.Error.WriteLine($"Embedded PDF not found: {embedPdfPath}");
            return;
        }

        // Load the source document
        using (Document doc = new Document(sourcePdfPath))
        {
            // Create a RichMediaAnnotation on the first page
            Page page = doc.Pages[1];
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);
            RichMediaAnnotation richMedia = new RichMediaAnnotation(page, rect);

            // Embed the PDF file as rich media content (MIME type: application/pdf)
            using (FileStream embedStream = File.OpenRead(embedPdfPath))
            {
                richMedia.SetContent("application/pdf", embedStream);
            }

            // Disable interactive features: make the annotation read‑only and locked
            richMedia.Flags = AnnotationFlags.ReadOnly | AnnotationFlags.Locked;

            // Optional: prevent automatic activation (user must click to open)
            // richMedia.ActivateOn = RichMediaAnnotation.ActivationEvent.None; // uncomment if enum supports None

            // Add the annotation to the page
            page.Annotations.Add(richMedia);

            // Save the modified document
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"RichMediaAnnotation added and saved to '{outputPdfPath}'.");
    }
}