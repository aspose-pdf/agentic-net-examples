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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (pages are 1‑based)
            Page page = doc.Pages[1];

            // Create a markup annotation (TextAnnotation) that the user can select
            // Fully qualify Rectangle to avoid ambiguity with System.Drawing
            Aspose.Pdf.Rectangle markupRect = new Aspose.Pdf.Rectangle(100, 700, 200, 750);
            TextAnnotation markup = new TextAnnotation(page, markupRect)
            {
                Title    = "Note",
                Contents = "Click to see more details.",
                Open     = false,                     // initially closed
                Icon     = TextIcon.Note
            };
            page.Annotations.Add(markup);

            // Create a PopupAnnotation that is linked to the markup annotation
            // Position the popup somewhere else on the page
            Aspose.Pdf.Rectangle popupRect = new Aspose.Pdf.Rectangle(250, 600, 450, 750);
            PopupAnnotation popup = new PopupAnnotation(page, popupRect)
            {
                Parent   = markup,                    // link to the markup annotation
                Contents = "This is the additional note that appears when the parent annotation is selected.",
                Open     = false                      // popup is hidden until the markup is activated
            };
            page.Annotations.Add(popup);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with popup annotation saved to '{outputPath}'.");
    }
}