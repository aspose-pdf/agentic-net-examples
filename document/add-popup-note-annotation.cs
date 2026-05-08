using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "popup_note.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Work with the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle where the sticky‑note icon will appear
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 120, 720);

            // Create a visible TextAnnotation (the sticky note)
            TextAnnotation textAnn = new TextAnnotation(page, rect)
            {
                Title    = "Note",                                   // Title shown in the popup window
                Contents = "Brief description",                      // Short text shown when the note is opened
                Icon     = TextIcon.Note,                            // Standard note icon
                Color    = Aspose.Pdf.Color.Yellow,                  // Icon background color
                Open     = false                                     // Do not display the popup on load
            };

            // Create a PopupAnnotation that holds the detailed information
            PopupAnnotation popup = new PopupAnnotation(page, rect)
            {
                Contents = "Detailed information displayed when the user hovers over the note.",
                Open     = false                                     // Show only on hover/click
            };

            // Link the popup to the text annotation
            textAnn.Popup = popup;   // Alternatively: popup.Parent = textAnn;

            // Add the annotation (which now references the popup) to the page
            page.Annotations.Add(textAnn);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with pop‑up note saved to '{outputPath}'.");
    }
}