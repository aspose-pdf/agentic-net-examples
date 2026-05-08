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

        // Load the PDF document (lifecycle rule: use using)
        using (Document doc = new Document(inputPath))
        {
            // Work with the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Rectangle for the parent markup annotation (a sticky note)
            Aspose.Pdf.Rectangle parentRect = new Aspose.Pdf.Rectangle(100, 700, 120, 720);

            // Create a TextAnnotation (sticky note) on the page
            TextAnnotation parentAnn = new TextAnnotation(page, parentRect)
            {
                Title    = "Note",
                Contents = "Click to see more details",
                Color    = Aspose.Pdf.Color.Yellow,
                Icon     = TextIcon.Comment,
                Open     = false // initially closed
            };

            // Rectangle for the popup window that will appear when the parent is selected
            Aspose.Pdf.Rectangle popupRect = new Aspose.Pdf.Rectangle(130, 720, 300, 850);

            // Create the PopupAnnotation
            PopupAnnotation popup = new PopupAnnotation(page, popupRect)
            {
                Contents = "Additional notes go here. This text appears in the popup window.",
                Open     = false // initially closed
            };

            // Associate the popup with its parent markup annotation
            parentAnn.Popup = popup;
            // Also set the Parent property of the popup (optional but clarifies the relationship)
            popup.Parent = parentAnn;

            // Add the parent annotation (which now carries the popup) to the page
            page.Annotations.Add(parentAnn);

            // Save the modified PDF (lifecycle rule: save inside using)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with popup annotation saved to '{outputPath}'.");
    }
}
