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

        // Load the existing PDF (Document implements IDisposable)
        using (Document doc = new Document(inputPath))
        {
            // Work with the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // -----------------------------------------------------------------
            // 1. Create a parent markup annotation (e.g., a TextAnnotation)
            // -----------------------------------------------------------------
            // Define the rectangle for the parent annotation
            Aspose.Pdf.Rectangle parentRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            TextAnnotation parentMarkup = new TextAnnotation(page, parentRect)
            {
                Title    = "Note",
                Contents = "This is the parent markup annotation.",
                Open     = false,                     // initially closed
                Icon     = TextIcon.Note,
                Color    = Aspose.Pdf.Color.Yellow   // appearance color
            };
            // Add the parent markup to the page
            page.Annotations.Add(parentMarkup);

            // -----------------------------------------------------------------
            // 2. Create a PopupAnnotation with custom dimensions
            // -----------------------------------------------------------------
            // Define a rectangle for the popup (custom size and position)
            Aspose.Pdf.Rectangle popupRect = new Aspose.Pdf.Rectangle(120, 560, 280, 660);
            PopupAnnotation popup = new PopupAnnotation(page, popupRect)
            {
                Contents = "Detailed information displayed in the popup window.",
                Open     = true,                      // show popup open by default
                Color    = Aspose.Pdf.Color.LightGray
            };

            // Associate the popup with its parent markup
            popup.Parent = parentMarkup;   // parentMarkup is the annotation this popup belongs to

            // Add the popup annotation to the page
            page.Annotations.Add(popup);

            // -----------------------------------------------------------------
            // 3. Save the modified PDF
            // -----------------------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with popup annotation: {outputPath}");
    }
}