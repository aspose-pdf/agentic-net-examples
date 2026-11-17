using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least one page
            if (doc.Pages.Count < 1)
            {
                Console.Error.WriteLine("Document has no pages.");
                return;
            }

            // Get the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Create a highlight annotation on the first page
            // HighlightAnnotation constructor requires the page and a rectangle
            HighlightAnnotation highlight = new HighlightAnnotation(
                page,
                new Aspose.Pdf.Rectangle(100, 700, 300, 720)) // llx, lly, urx, ury
            {
                // Use Aspose.Pdf.Color (cross‑platform) for the highlight color
                Color = Aspose.Pdf.Color.Yellow,
                // Optional: set the contents that appear in the popup
                Contents = "Highlighted text"
            };

            // Create a popup annotation that will be linked to the highlight
            // PopupAnnotation also requires the page and a rectangle
            PopupAnnotation popup = new PopupAnnotation(
                page,
                new Aspose.Pdf.Rectangle(320, 720, 520, 800))
            {
                // Title property is not available in older Aspose.Pdf versions; omit it.
                // Title = "Note",
                // Text shown inside the popup window
                Contents = "This is a popup linked to the highlight annotation."
            };

            // Link the popup to the highlight annotation
            // MarkupAnnotation (base of HighlightAnnotation) has a Popup property
            highlight.Popup = popup;

            // Add the highlight annotation to the page.
            // The linked popup is automatically added when the parent annotation is saved.
            page.Annotations.Add(highlight);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Popup annotation linked to highlight saved to '{outputPath}'.");
    }
}
