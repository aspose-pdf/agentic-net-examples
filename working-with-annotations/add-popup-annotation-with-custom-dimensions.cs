using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_popup.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure we work with the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // ----- Parent markup annotation (e.g., a sticky note) -----
            // Define the rectangle for the parent annotation
            Aspose.Pdf.Rectangle parentRect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);
            TextAnnotation parent = new TextAnnotation(page, parentRect)
            {
                Title    = "Note",
                Contents = "This is the parent annotation.",
                Open     = false,                     // start closed
                Color    = Aspose.Pdf.Color.Yellow   // visual cue
            };
            // Optional: add a border to the parent annotation
            parent.Border = new Border(parent) { Width = 1 };
            page.Annotations.Add(parent);

            // ----- Popup annotation linked to the parent -----
            // Define custom dimensions for the popup (e.g., larger than parent)
            Aspose.Pdf.Rectangle popupRect = new Aspose.Pdf.Rectangle(120, 520, 320, 720);
            PopupAnnotation popup = new PopupAnnotation(page, popupRect);
            popup.Contents = "Detailed information displayed in the popup window.";
            popup.Parent   = parent;   // associate with the parent markup annotation
            popup.Open     = true;    // open by default (optional)
            popup.Color    = Aspose.Pdf.Color.LightGray;
            // Border must be set after the annotation instance exists
            popup.Border   = new Border(popup) { Width = 1 };
            page.Annotations.Add(popup);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with popup annotation saved to '{outputPath}'.");
    }
}
