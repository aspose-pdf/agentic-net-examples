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

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Use the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Rectangle for the parent markup annotation (a sticky‑note icon)
            Aspose.Pdf.Rectangle parentRect = new Aspose.Pdf.Rectangle(100, 700, 120, 720);

            // Create a TextAnnotation that will act as the parent annotation
            TextAnnotation parentAnn = new TextAnnotation(page, parentRect)
            {
                Title    = "Note",
                Contents = "Click to view additional information",
                Color    = Aspose.Pdf.Color.Yellow,
                Icon     = TextIcon.Comment   // optional visual icon
            };

            // Rectangle for the popup window (size and position of the popup)
            Aspose.Pdf.Rectangle popupRect = new Aspose.Pdf.Rectangle(150, 650, 350, 800);

            // Create the PopupAnnotation that holds the extra notes
            PopupAnnotation popup = new PopupAnnotation(page, popupRect)
            {
                Contents = "Here are the additional notes displayed in the popup window.",
                // RichText can be used for formatted content if needed
                // RichText = "Additional <b>notes</b> with formatting.",
                Open = false   // start closed; opens when the parent annotation is selected
            };

            // Link the popup to its parent markup annotation
            parentAnn.Popup = popup;   // alternatively: popup.Parent = parentAnn;

            // Add the parent annotation to the page (the popup is automatically associated)
            page.Annotations.Add(parentAnn);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with popup annotation: {outputPath}");
    }
}