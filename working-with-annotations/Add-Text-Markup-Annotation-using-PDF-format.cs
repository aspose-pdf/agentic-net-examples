using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based page indexing
            Page page = doc.Pages[1];

            // Define the annotation rectangle (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 520);

            // Create a Highlight annotation (TextMarkupAnnotation subclass)
            HighlightAnnotation highlight = new HighlightAnnotation(page, rect)
            {
                // Set visual properties
                Color = Aspose.Pdf.Color.Yellow,
                // Optional descriptive text shown in the popup
                Contents = "Important text",
                Title = "Reviewer"
                // Note: The 'Open' property is not available on HighlightAnnotation in recent Aspose.Pdf versions.
            };

            // Attach the annotation to the page
            page.Annotations.Add(highlight);

            // Save the modified PDF (PDF format, no SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text markup annotation added. Saved to '{outputPath}'.");
    }
}
