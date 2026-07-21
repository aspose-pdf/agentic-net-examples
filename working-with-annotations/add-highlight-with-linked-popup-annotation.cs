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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Create a highlight annotation on the first page
            Aspose.Pdf.Rectangle highlightRect = new Aspose.Pdf.Rectangle(100, 500, 300, 520);
            HighlightAnnotation highlight = new HighlightAnnotation(page, highlightRect)
            {
                Color = Aspose.Pdf.Color.Yellow,
                Contents = "Highlighted text"
            };
            page.Annotations.Add(highlight);

            // Create a popup annotation and associate it with the highlight
            Aspose.Pdf.Rectangle popupRect = new Aspose.Pdf.Rectangle(320, 500, 340, 520);
            PopupAnnotation popup = new PopupAnnotation(page, popupRect)
            {
                Contents = "This is a popup note.",
                Open = false // initially closed
            };
            // Link the popup to the highlight annotation
            popup.Parent = highlight;
            page.Annotations.Add(popup);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved PDF with linked popup to '{outputPath}'.");
    }
}