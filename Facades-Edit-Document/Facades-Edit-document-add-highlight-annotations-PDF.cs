using System;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing;   // for BorderStyle

class Program
{
    static void Main()
    {
        // Paths to the source PDF and the output PDF
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Load the existing PDF document
        Document pdfDocument = new Document(inputPath);

        // Get the first page (Aspose.Pdf uses 1‑based indexing)
        Page page = pdfDocument.Pages[1];

        // Define the rectangle that bounds the text to be highlighted
        // (llx, lly, urx, ury) – adjust coordinates as needed
        Aspose.Pdf.Rectangle highlightRect = new Aspose.Pdf.Rectangle(100, 500, 300, 520);

        // Create a HighlightAnnotation on the specified page and rectangle
        HighlightAnnotation highlight = new HighlightAnnotation(page, highlightRect);

        // Set the highlight color (yellow is typical)
        highlight.Color = Color.Yellow;

        // Optional: add a tooltip or comment
        highlight.Contents = "Highlighted text";

        // Initialize the border after the annotation object is created
        // (using the provided border‑initialization rule)
        highlight.Border = new Border(highlight)
        {
            Style = BorderStyle.Solid,
            Width = 1
        };

        // Add the annotation to the page's annotation collection
        page.Annotations.Add(highlight);

        // Save the modified PDF document (using the provided document‑save rule)
        pdfDocument.Save(outputPath);
    }
}