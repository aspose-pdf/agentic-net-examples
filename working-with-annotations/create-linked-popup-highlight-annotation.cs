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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using rule: document-disposal-with-using)
        using (Document doc = new Document(inputPath))
        {
            // First page (1‑based indexing per rule: page-indexing-one-based)
            Page page = doc.Pages[1];

            // Rectangle for the highlight annotation
            Aspose.Pdf.Rectangle highlightRect = new Aspose.Pdf.Rectangle(100, 500, 300, 520);
            HighlightAnnotation highlight = new HighlightAnnotation(page, highlightRect)
            {
                Color = Aspose.Pdf.Color.Yellow,   // optional visual styling
                Contents = "Highlighted text"
            };

            // Rectangle for the popup annotation (position of the popup icon)
            Aspose.Pdf.Rectangle popupRect = new Aspose.Pdf.Rectangle(320, 500, 340, 520);
            PopupAnnotation popup = new PopupAnnotation(page, popupRect)
            {
                Contents = "This is a popup note linked to the highlight.",
                Open = false,                     // initially closed
                Color = Aspose.Pdf.Color.LightGray
            };

            // Link the popup to the highlight (both directions are recommended)
            highlight.Popup = popup;
            popup.Parent = highlight;

            // Add annotations to the page
            page.Annotations.Add(highlight);
            page.Annotations.Add(popup);

            // Save the modified PDF (using rule: save-to-non-pdf-always-use-save-options not needed for PDF)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved PDF with linked annotations to '{outputPath}'.");
    }
}