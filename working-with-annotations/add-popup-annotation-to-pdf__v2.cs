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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // ----- Parent markup annotation (TextAnnotation) -----
            // Define the rectangle for the parent annotation
            Aspose.Pdf.Rectangle parentRect = new Aspose.Pdf.Rectangle(100, 600, 200, 650);

            // Create a TextAnnotation on the page
            TextAnnotation parent = new TextAnnotation(page, parentRect)
            {
                Title = "Note",
                Contents = "Parent annotation",
                Color = Aspose.Pdf.Color.Yellow,
                Open = false,               // not opened by default
                Icon = TextIcon.Note
            };
            page.Annotations.Add(parent);

            // ----- Popup annotation -----
            // Custom dimensions for the popup
            double popupWidth = 150;
            double popupHeight = 100;

            // Position the popup relative to the parent (e.g., 20 pts right, 30 pts below)
            double popupX = parentRect.LLX + 20;
            double popupY = parentRect.LLY - 30;

            Aspose.Pdf.Rectangle popupRect = new Aspose.Pdf.Rectangle(
                popupX,
                popupY,
                popupX + popupWidth,
                popupY + popupHeight);

            // Create the PopupAnnotation on the same page
            PopupAnnotation popup = new PopupAnnotation(page, popupRect)
            {
                Contents = "Detailed information in popup",
                Color = Aspose.Pdf.Color.LightGray,
                Open = false // initially closed
            };

            // Associate the popup with its parent markup annotation
            popup.Parent = parent;
            parent.Popup = popup;

            // Add the popup to the page's annotation collection
            page.Annotations.Add(popup);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with popup annotation to '{outputPath}'.");
    }
}