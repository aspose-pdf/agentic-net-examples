using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";   // existing PDF or create a new one
        const string outputPath = "output_with_popup.pdf";

        // Ensure the input file exists; if not, create a blank PDF with one page.
        if (!File.Exists(inputPath))
        {
            using (Document blank = new Document())
            {
                blank.Pages.Add();
                blank.Save(inputPath);
            }
        }

        // Open the document inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (1‑based indexing).
            Page page = doc.Pages[1];

            // Define the rectangle for the parent markup annotation (a sticky note).
            Aspose.Pdf.Rectangle parentRect = new Aspose.Pdf.Rectangle(100, 700, 120, 720);

            // Create a TextAnnotation (sticky note) on the page.
            TextAnnotation textAnn = new TextAnnotation(page, parentRect)
            {
                Title    = "Note",
                Contents = "Click to see more details.",
                Color    = Aspose.Pdf.Color.Yellow,
                Icon     = TextIcon.Note,
                Open     = false   // Do not open the popup automatically.
            };

            // Define the rectangle for the popup annotation (size of the popup window).
            Aspose.Pdf.Rectangle popupRect = new Aspose.Pdf.Rectangle(150, 750, 350, 850);

            // Create the PopupAnnotation and associate it with the parent annotation.
            PopupAnnotation popup = new PopupAnnotation(page, popupRect)
            {
                Contents = "Additional notes displayed in the popup window.",
                Open     = false   // Popup is hidden until the parent annotation is selected.
            };

            // Link the popup to its parent markup annotation.
            textAnn.Popup = popup;          // Alternatively: popup.Parent = textAnn;

            // Add the parent annotation (which now carries the popup) to the page.
            page.Annotations.Add(textAnn);

            // Save the modified document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with popup annotation saved to '{outputPath}'.");
    }
}