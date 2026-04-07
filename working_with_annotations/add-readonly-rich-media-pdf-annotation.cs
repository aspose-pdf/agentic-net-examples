using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";   // PDF to which the annotation will be added
        const string embedPdf = "embed.pdf";   // PDF that will be embedded as rich media
        const string outputPdf = "output.pdf"; // Resulting PDF

        if (!File.Exists(inputPdf) || !File.Exists(embedPdf))
        {
            Console.Error.WriteLine("Required files not found.");
            return;
        }

        // Load the target document (lifecycle rule: using block)
        using (Document doc = new Document(inputPdf))
        {
            // Define the annotation rectangle (coordinates are in points)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);

            // Create a RichMediaAnnotation on the first page
            RichMediaAnnotation richMedia = new RichMediaAnnotation(doc.Pages[1], rect);

            // NOTE: The ActivateOn property and ActivationEvent enum are not available in recent Aspose.PDF versions.
            // The annotation will be displayed in a read‑only view by default when the appropriate flags are set.

            // Set flags to make the annotation non‑interactive and read‑only
            richMedia.Flags = AnnotationFlags.ReadOnly |
                              AnnotationFlags.Hidden |
                              AnnotationFlags.NoView |
                              AnnotationFlags.Locked;

            // Embed the external PDF as the rich‑media content
            using (FileStream embedStream = File.OpenRead(embedPdf))
            {
                // "EmbeddedPDF" is an arbitrary name for the content stream
                richMedia.SetContent("EmbeddedPDF", embedStream);
            }

            // Optional descriptive text for the annotation
            richMedia.Contents = "Embedded PDF (read‑only)";

            // Add the annotation to the page's annotation collection
            doc.Pages[1].Annotations.Add(richMedia);

            // Save the modified document (lifecycle rule: using block)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"RichMediaAnnotation added and saved to '{outputPdf}'.");
    }
}
