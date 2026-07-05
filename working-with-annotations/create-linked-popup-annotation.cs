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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document (using rule: document-disposal-with-using)
        using (Document doc = new Document(inputPath))
        {
            // Access the first page (1‑based indexing per rule: page-indexing-one-based)
            Page page = doc.Pages[1];

            // ----- Create a Highlight annotation -----
            // Define the rectangle where the highlight will appear
            Aspose.Pdf.Rectangle highlightRect = new Aspose.Pdf.Rectangle(100, 500, 300, 520);
            HighlightAnnotation highlight = new HighlightAnnotation(page, highlightRect)
            {
                Contents = "Highlighted text"
            };
            // Add the highlight to the page's annotation collection
            page.Annotations.Add(highlight);

            // ----- Create a Popup annotation linked to the highlight -----
            // Define the rectangle for the popup window (position and size)
            Aspose.Pdf.Rectangle popupRect = new Aspose.Pdf.Rectangle(100, 520, 300, 620);
            PopupAnnotation popup = new PopupAnnotation(page, popupRect)
            {
                Contents = "This is a popup note.",
                Open = false // popup is closed by default
            };
            // Associate the popup with the highlight annotation
            popup.Parent = highlight;
            // Add the popup to the page's annotation collection
            page.Annotations.Add(popup);

            // Save the modified PDF (PDF format, no special SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with linked popup saved to '{outputPath}'.");
    }
}