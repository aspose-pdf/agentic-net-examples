using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths to the XSL‑FO source and the resulting PDF
        const string xslFoPath = "input.xslfo";
        const string outputPdf = "highlighted_output.pdf";

        if (!File.Exists(xslFoPath))
        {
            Console.Error.WriteLine($"XSL‑FO file not found: {xslFoPath}");
            return;
        }

        // Load the XSL‑FO file into a PDF document
        using (Document pdfDoc = new Document(xslFoPath, new XslFoLoadOptions()))
        {
            // Ensure the document has at least one page
            if (pdfDoc.Pages.Count == 0)
            {
                Console.Error.WriteLine("The generated PDF has no pages.");
                return;
            }

            // Define the rectangle (llx, lly, urx, ury) for the highlight annotation
            // Coordinates are in points; adjust as needed for your content
            Aspose.Pdf.Rectangle highlightRect = new Aspose.Pdf.Rectangle(100, 500, 300, 520);

            // Create the highlight annotation on the first page
            HighlightAnnotation highlight = new HighlightAnnotation(pdfDoc.Pages[1], highlightRect)
            {
                // Set visual properties
                Color   = Aspose.Pdf.Color.Yellow, // Use Aspose.Pdf.Color (cross‑platform)
                Opacity = 0.5,
                Contents = "Highlighted text"
            };

            // Add the annotation to the page's annotation collection
            pdfDoc.Pages[1].Annotations.Add(highlight);

            // Save the modified PDF
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with highlight annotation saved to '{outputPdf}'.");
    }
}