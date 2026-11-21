using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Wrap Document in a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least one page
            Page page = doc.Pages[1];

            // ----- Parent markup annotation (TextAnnotation) -----
            // Define custom dimensions for the parent annotation
            Aspose.Pdf.Rectangle parentRect = new Aspose.Pdf.Rectangle(100, 600, 200, 650);
            TextAnnotation parentAnno = new TextAnnotation(page, parentRect)
            {
                Title    = "Note",
                Contents = "Click to see details.",
                Icon     = TextIcon.Note,
                Open     = false // collapsed initially
            };
            page.Annotations.Add(parentAnno);

            // ----- Popup annotation linked to the parent markup -----
            // Position the popup relative to the parent (e.g., 20 pts right, 30 pts down)
            double offsetX = 20;
            double offsetY = -30; // negative Y moves downwards in PDF coordinate space
            Aspose.Pdf.Rectangle popupRect = new Aspose.Pdf.Rectangle(
                parentRect.LLX + offsetX,
                parentRect.LLY + offsetY,
                parentRect.URX + offsetX,
                parentRect.URY + offsetY);

            PopupAnnotation popupAnno = new PopupAnnotation(page, popupRect)
            {
                // Title property is not available in older Aspose.Pdf versions; omit it.
                Parent   = parentAnno, // associate with the parent markup
                Contents = "This is the popup content providing more information."
            };
            page.Annotations.Add(popupAnno);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with popup annotation saved to '{outputPath}'.");
    }
}
