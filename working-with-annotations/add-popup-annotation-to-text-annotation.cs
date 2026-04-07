using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "popup_annotation_example.pdf";

        // Create a new PDF document and add a single page.
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Define the rectangle for the parent markup annotation (a sticky note).
            Aspose.Pdf.Rectangle parentRect = new Aspose.Pdf.Rectangle(100, 700, 120, 720);

            // Create a TextAnnotation (sticky note) on the page.
            TextAnnotation parentAnnotation = new TextAnnotation(page, parentRect)
            {
                Title    = "Reviewer",
                Contents = "Click to see additional notes.",
                Color    = Aspose.Pdf.Color.Yellow,
                Icon     = TextIcon.Note,
                Open     = false // Do not display the popup initially.
            };

            // Define the rectangle for the popup annotation (where the note will appear).
            Aspose.Pdf.Rectangle popupRect = new Aspose.Pdf.Rectangle(130, 720, 300, 800);

            // Create the PopupAnnotation.
            PopupAnnotation popup = new PopupAnnotation(page, popupRect)
            {
                // The text that will be shown in the popup window.
                Contents = "These are the additional notes that appear when the parent annotation is selected.",
                // Initially closed; it will open when the parent is activated.
                Open = false,
                // Optional: set a background color for the popup window.
                Color = Aspose.Pdf.Color.LightGray
            };

            // Associate the popup with its parent markup annotation.
            parentAnnotation.Popup = popup;
            // Also set the reverse link (optional but explicit).
            popup.Parent = parentAnnotation;

            // Add both annotations to the page's annotation collection.
            page.Annotations.Add(parentAnnotation);
            page.Annotations.Add(popup);

            // Save the document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with popup annotation saved to '{outputPath}'.");
    }
}