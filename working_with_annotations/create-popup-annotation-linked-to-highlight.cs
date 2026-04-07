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

        // Document lifecycle handled with using (rule: document-disposal-with-using)
        using (Document doc = new Document(inputPath))
        {
            // First page (1‑based indexing per rule: page-indexing-one-based)
            Page page = doc.Pages[1];

            // Highlight annotation rectangle
            Aspose.Pdf.Rectangle highlightRect = new Aspose.Pdf.Rectangle(100, 500, 300, 520);
            HighlightAnnotation highlight = new HighlightAnnotation(page, highlightRect)
            {
                Color = Aspose.Pdf.Color.Yellow,
                Contents = "Highlighted text"
            };
            page.Annotations.Add(highlight);

            // Popup annotation rectangle (position of the popup icon)
            Aspose.Pdf.Rectangle popupRect = new Aspose.Pdf.Rectangle(300, 520, 350, 570);
            PopupAnnotation popup = new PopupAnnotation(page, popupRect)
            {
                Contents = "This is a popup note.",
                Open = false // initially closed
            };
            page.Annotations.Add(popup);

            // Link the popup to the highlight (both ways are valid)
            highlight.Popup = popup;
            // popup.Parent = highlight; // alternative linking

            // Save the document (inside using block as required)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotated PDF saved to '{outputPath}'.");
    }
}