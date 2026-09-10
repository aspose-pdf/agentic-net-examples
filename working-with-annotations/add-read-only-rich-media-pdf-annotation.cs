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
        const string outputPdfPath = "output.pdf"; // Resulting PDF

        // Ensure source files exist
        if (!File.Exists(targetPdfPath))
        {
            Console.Error.WriteLine($"Target PDF not found: {targetPdfPath}");
            return;
        }
        if (!File.Exists(embedPdfPath))
        {
            Console.Error.WriteLine($"PDF to embed not found: {embedPdfPath}");
            return;
        }

        // Open the target document
        using (Document doc = new Document(targetPdfPath))
        {
            // Choose the page where the annotation will be placed (first page, 1‑based index)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (coordinates in points)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);

            // Create the RichMediaAnnotation
            RichMediaAnnotation richMedia = new RichMediaAnnotation(page, rect)
            {
                // Disable user interaction – read‑only view
                Flags = AnnotationFlags.ReadOnly
                // No activation event is set, leaving the annotation inert until explicitly activated.
            };

            // Embed the PDF file as rich‑media content
            using (FileStream embedStream = File.OpenRead(embedPdfPath))
            {
                // The first argument is a MIME type string; "application/pdf" denotes an embedded PDF
                richMedia.SetContent("application/pdf", embedStream);
            }

            // Add the annotation to the page
            page.Annotations.Add(richMedia);

            // Save the modified document
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"RichMediaAnnotation added and saved to '{outputPdfPath}'.");
    }
}
