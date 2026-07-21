using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";   // PDF to which the annotation will be added
        const string embedPdfPath   = "embed.pdf";   // PDF that will be embedded as rich media
        const string outputPdfPath  = "output.pdf";  // Resulting PDF

        // Ensure source files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(embedPdfPath))
        {
            Console.Error.WriteLine($"Embedded PDF not found: {embedPdfPath}");
            return;
        }

        // Load the target document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPdfPath))
        {
            // Use 1‑based page indexing (global rule)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the RichMediaAnnotation (constructor rule)
            RichMediaAnnotation richMedia = new RichMediaAnnotation(page, rect);

            // Embed the PDF as rich‑media content.
            // SetContent(string mimeType, Stream contentStream) is the correct API.
            using (FileStream embedStream = File.OpenRead(embedPdfPath))
            {
                richMedia.SetContent("application/pdf", embedStream);
            }

            // Disable all interactive features:
            //   - ReadOnly: prevents user interaction.
            //   - Locked: prevents moving/resizing/deleting the annotation.
            // Combine flags using bitwise OR.
            richMedia.Flags = AnnotationFlags.ReadOnly | AnnotationFlags.Locked;

            // Optional: prevent automatic activation (if the enum provides a None value).
            // richMedia.ActivateOn = RichMediaAnnotation.ActivationEvent.None;

            // Add the annotation to the page.
            page.Annotations.Add(richMedia);

            // Save the modified document (lifecycle rule: save inside using block).
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"RichMediaAnnotation added and saved to '{outputPdfPath}'.");
    }
}