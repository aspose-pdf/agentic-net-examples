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

        // Load the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // First page (1‑based indexing)
            Page page = doc.Pages[1];

            // Rectangle for the highlight annotation
            Aspose.Pdf.Rectangle highlightRect = new Aspose.Pdf.Rectangle(100, 700, 300, 720);

            // Create and configure the highlight annotation
            HighlightAnnotation highlight = new HighlightAnnotation(page, highlightRect)
            {
                Color = Aspose.Pdf.Color.Yellow,
                Contents = "Highlighted text"
            };
            page.Annotations.Add(highlight);

            // Rectangle for the popup annotation (small icon area)
            Aspose.Pdf.Rectangle popupRect = new Aspose.Pdf.Rectangle(320, 700, 340, 720);

            // Create and configure the popup annotation
            PopupAnnotation popup = new PopupAnnotation(page, popupRect)
            {
                Color = Aspose.Pdf.Color.LightGray,
                Contents = "This is a popup note linked to the highlight.",
                Open = false // initially closed
            };
            page.Annotations.Add(popup);

            // Link the popup to the highlight annotation
            highlight.Popup = popup; // associate popup with the highlight

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved PDF with linked popup to '{outputPath}'.");
    }
}