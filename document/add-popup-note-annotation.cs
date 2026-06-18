using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_popupped.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least one page
            Page page = doc.Pages[1]; // 1‑based indexing

            // Define the rectangle where the sticky‑note icon will appear
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 120, 720);

            // Create the visible TextAnnotation (the sticky note)
            TextAnnotation textAnn = new TextAnnotation(page, rect)
            {
                Title    = "Note",                     // Title shown in the pop‑up window title bar
                Contents = "Hover to see details",    // Short tooltip text
                Icon     = TextIcon.Comment,           // Choose an icon style
                Open     = false                       // Start closed; will open on hover/click
            };

            // Create the hidden PopupAnnotation that holds the detailed information
            PopupAnnotation popup = new PopupAnnotation(page, rect)
            {
                Contents = "This is the detailed information displayed in the pop‑up window when the note is activated.",
                Open     = false // Keep closed initially
            };

            // Link the popup to its parent annotation
            popup.Parent = textAnn;
            textAnn.Popup = popup;

            // Add the parent annotation (which now carries the popup) to the page
            page.Annotations.Add(textAnn);

            // Save the modified PDF (PDF format by default)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Pop‑up note annotation added and saved to '{outputPath}'.");
    }
}