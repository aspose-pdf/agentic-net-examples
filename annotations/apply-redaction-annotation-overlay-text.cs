using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "redacted_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least 9 pages (1‑based indexing)
            if (doc.Pages.Count < 9)
            {
                Console.Error.WriteLine("Document does not contain page 9.");
                return;
            }

            // Get page 9
            Page page = doc.Pages[9];

            // Define a rectangle covering the whole page (coordinates: llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(
                0,
                0,
                page.PageInfo.Width,
                page.PageInfo.Height);

            // Create the redaction annotation
            RedactionAnnotation redaction = new RedactionAnnotation(page, rect)
            {
                // Text that will appear over the redacted area
                OverlayText = "Top Secret",
                // Optional: set fill color for the redaction box
                FillColor = Aspose.Pdf.Color.Black
            };

            // Add the annotation to the page
            page.Annotations.Add(redaction);

            // Apply the redaction (Document.Redact() does not exist in recent versions)
            redaction.Redact();

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Redacted PDF saved to '{outputPath}'.");
    }
}
