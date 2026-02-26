using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF (must exist)
        const string outputSvg = "highlighted_output.svg";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF, add a highlight annotation, then save as SVG
        using (Document doc = new Document(inputPdf))
        {
            // Aspose.Pdf uses 1‑based page indexing
            Page page = doc.Pages[1];

            // Define the rectangle area to be highlighted (coordinates in points)
            // Fully qualified to avoid ambiguity
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 520);

            // Create the highlight annotation on the specified page
            HighlightAnnotation highlight = new HighlightAnnotation(page, rect)
            {
                // Optional: set the highlight color (e.g., yellow)
                Color = Aspose.Pdf.Color.Yellow,
                // Optional: add a comment that appears in the annotation popup
                Contents = "Important text highlighted."
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(highlight);

            // Save the modified document as SVG; must pass SvgSaveOptions explicitly
            SvgSaveOptions svgOptions = new SvgSaveOptions();
            doc.Save(outputSvg, svgOptions);
        }

        Console.WriteLine($"PDF with highlight annotation saved as SVG: '{outputSvg}'");
    }
}