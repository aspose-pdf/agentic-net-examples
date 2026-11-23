using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "Invoice.pdf";
        const string outputPath = "HighlightedInvoice.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Work with the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle area to be highlighted (llx, lly, urx, ury)
            // Adjust coordinates as needed for the target content
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 520);

            // Create a HighlightAnnotation with the rectangle
            HighlightAnnotation highlight = new HighlightAnnotation(page, rect)
            {
                // Set a title that appears in the annotation popup
                Title = "Important",

                // Set a custom color (e.g., light orange)
                // Use Aspose.Pdf.Color to stay cross‑platform
                Color = Aspose.Pdf.Color.FromRgb(1.0, 0.6, 0.0)
                // Note: HighlightAnnotation does not expose an Open property; it is always closed by default.
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(highlight);

            // Save the modified PDF; the using block ensures the document stays alive until Save completes
            doc.Save(outputPath);
        }

        Console.WriteLine($"Highlighted PDF saved to '{outputPath}'.");
    }
}
