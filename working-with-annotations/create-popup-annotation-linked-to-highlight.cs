using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_popup.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // ----- Create a Highlight annotation -----
            // Define the rectangle that the highlight will cover
            Rectangle highlightRect = new Rectangle(100, 700, 300, 720);
            HighlightAnnotation highlight = new HighlightAnnotation(page, highlightRect)
            {
                // Set a semi‑transparent yellow color (cross‑platform Aspose.Pdf.Color)
                Color = Color.FromRgb(1.0, 1.0, 0.0),
                // Optional title and contents shown in the popup
                Title = "Important",
                Contents = "This text is highlighted."
            };

            // ----- Create a Popup annotation -----
            // Define the rectangle for the popup window (position and size)
            Rectangle popupRect = new Rectangle(320, 720, 520, 800);
            PopupAnnotation popup = new PopupAnnotation(page, popupRect)
            {
                // Title property is not available on PopupAnnotation in older versions; omit it.
                Contents = "Additional information about the highlighted text."
            };

            // Link the popup to the highlight annotation
            highlight.Popup = popup;

            // Add only the markup annotation (highlight) to the page.
            // The linked popup will be saved automatically.
            page.Annotations.Add(highlight);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with linked popup annotation to '{outputPath}'.");
    }
}
