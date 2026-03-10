using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "highlighted.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Choose the page to annotate (1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle area to be highlighted
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 520);

            // Create a HighlightAnnotation on the specified page and rectangle
            HighlightAnnotation highlight = new HighlightAnnotation(page, rect)
            {
                // Set the highlight color (e.g., yellow)
                Color = Aspose.Pdf.Color.Yellow,

                // Optional: add a comment that appears in the popup
                Contents = "Important text highlighted."
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(highlight);

            // Save the modified document.
            // Note: Aspose.Pdf does NOT support saving to PCL format.
            // Document.Save(string) always writes PDF regardless of file extension.
            // Therefore we save as PDF.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Highlight annotation added. Saved to '{outputPdf}'.");
    }
}