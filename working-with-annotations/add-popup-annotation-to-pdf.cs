using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";   // existing PDF or will be created if missing
        const string outputPath = "output_with_popup.pdf";

        // Ensure the input file exists; if not, create a simple one-page PDF
        if (!File.Exists(inputPath))
        {
            using (Document doc = new Document())
            {
                // Add a blank page
                doc.Pages.Add();
                doc.Save(inputPath);
            }
        }

        // Open the PDF and add a markup annotation with an associated popup annotation
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (pages are 1‑based)
            Page page = doc.Pages[1];

            // ----- Parent markup annotation (e.g., a TextAnnotation) -----
            // Define the rectangle for the markup annotation
            Aspose.Pdf.Rectangle markupRect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);
            TextAnnotation markup = new TextAnnotation(page, markupRect)
            {
                Title    = "Summary",
                Contents = "Click the icon to view more details.",
                Open     = false,                         // keep closed initially
                Color    = Aspose.Pdf.Color.LightGray      // background color (cross‑platform)
            };
            page.Annotations.Add(markup);

            // ----- Popup annotation linked to the markup annotation -----
            // Define the rectangle for the popup window
            Aspose.Pdf.Rectangle popupRect = new Aspose.Pdf.Rectangle(210, 500, 400, 650);
            PopupAnnotation popup = new PopupAnnotation(page, popupRect)
            {
                Contents = "Here are the additional notes that appear when the parent annotation is selected.",
                Open     = false                          // popup starts closed
            };
            // Link the popup to its parent markup annotation
            popup.Parent = markup;

            page.Annotations.Add(popup);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with popup annotation saved to '{outputPath}'.");
    }
}