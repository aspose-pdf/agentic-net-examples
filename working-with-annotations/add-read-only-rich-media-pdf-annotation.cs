using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string targetPdfPath = "target.pdf";   // PDF to receive the annotation
        const string embedPdfPath = "embed.pdf";    // PDF to embed as rich media
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(targetPdfPath))
        {
            Console.Error.WriteLine($"Target PDF not found: {targetPdfPath}");
            return;
        }
        if (!File.Exists(embedPdfPath))
        {
            Console.Error.WriteLine($"Embedded PDF not found: {embedPdfPath}");
            return;
        }

        // Load the target document (the one that will contain the annotation)
        using (Document doc = new Document(targetPdfPath))
        {
            // Choose the page where the annotation will be placed (first page)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (coordinates are in points)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);

            // Create the RichMediaAnnotation
            RichMediaAnnotation richMedia = new RichMediaAnnotation(page, rect);

            // Embed the PDF content as a stream
            using (FileStream embedStream = File.OpenRead(embedPdfPath))
            {
                // Set the content type to PDF and provide the stream
                richMedia.SetContent("application/pdf", embedStream);
            }

            // Do NOT set an activation event – leaving it unset disables automatic launch.
            // richMedia.ActivateOn = RichMediaAnnotation.ActivationEvent.None; // Removed – enum has no 'None'

            // Make the annotation read‑only (no user interaction)
            richMedia.Flags = AnnotationFlags.ReadOnly;

            // Add the annotation to the page
            page.Annotations.Add(richMedia);

            // Save the modified document
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Rich media annotation added. Output saved to '{outputPdfPath}'.");
    }
}
