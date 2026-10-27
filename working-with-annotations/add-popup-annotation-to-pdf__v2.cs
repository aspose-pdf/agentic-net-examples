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
            // Ensure there is at least one page
            Page page = doc.Pages[1];

            // Create a markup annotation (TextAnnotation) that will own the popup
            // Use fully qualified Rectangle to avoid ambiguity with System.Drawing
            Aspose.Pdf.Rectangle markupRect = new Aspose.Pdf.Rectangle(100, 600, 200, 650);
            TextAnnotation markup = new TextAnnotation(page, markupRect)
            {
                Title    = "Note",
                Contents = "Click the icon to see more details.",
                Open     = false,
                Icon     = TextIcon.Note,
                // Use Aspose.Pdf.Color for cross‑platform compatibility
                Color    = Aspose.Pdf.Color.Yellow
            };
            page.Annotations.Add(markup);

            // Define custom dimensions for the popup (width 250, height 150)
            // Position it relative to the markup: offset 20 points right and 20 points down
            double popupWidth  = 250;
            double popupHeight = 150;
            double offsetX = 20;
            double offsetY = -20; // negative Y moves down in PDF coordinate system

            // Calculate popup rectangle based on markup rectangle
            double llx = markupRect.LLX + offsetX;
            double lly = markupRect.LLY + offsetY - popupHeight; // lower‑left Y
            double urx = llx + popupWidth;
            double ury = lly + popupHeight;

            Aspose.Pdf.Rectangle popupRect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

            // Create the PopupAnnotation and associate it with the markup annotation
            PopupAnnotation popup = new PopupAnnotation(page, popupRect)
            {
                // The Parent property links the popup to its markup annotation
                Parent = markup,
                // Optional: set a background color for the popup window
                Color = Aspose.Pdf.Color.LightGray
                // Title property is not available on PopupAnnotation in older versions; omitted intentionally
            };
            page.Annotations.Add(popup);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with popup annotation saved to '{outputPath}'.");
    }
}
