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

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // ---------- Parent markup annotation ----------
            // Create a TextAnnotation (sticky note) that will act as the parent
            Aspose.Pdf.Rectangle parentRect = new Aspose.Pdf.Rectangle(100, 700, 120, 720);
            TextAnnotation parent = new TextAnnotation(page, parentRect)
            {
                Title    = "Note",
                Contents = "Parent annotation",
                Color    = Aspose.Pdf.Color.Yellow,
                Open     = false,
                Icon     = TextIcon.Note
            };
            page.Annotations.Add(parent);

            // ---------- Popup annotation ----------
            // Define custom dimensions for the popup window
            Aspose.Pdf.Rectangle popupRect = new Aspose.Pdf.Rectangle(130, 720, 300, 850);
            PopupAnnotation popup = new PopupAnnotation(page, popupRect)
            {
                Contents = "Detailed information displayed in the popup.",
                Open     = true, // show the popup open by default
                Color    = Aspose.Pdf.Color.LightGray
            };
            // Associate the popup with its parent markup annotation
            popup.Parent = parent;

            // Add the popup annotation to the page
            page.Annotations.Add(popup);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved PDF with popup annotation to '{outputPath}'.");
    }
}