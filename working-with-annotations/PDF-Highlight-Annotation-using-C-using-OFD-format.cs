using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputOfd = "input.ofd";
        const string outputPdf = "highlighted_output.pdf";

        if (!File.Exists(inputOfd))
        {
            Console.Error.WriteLine($"File not found: {inputOfd}");
            return;
        }

        // Load the OFD file (input‑only format) as a PDF document
        using (Document doc = new Document(inputOfd, new OfdLoadOptions()))
        {
            // Ensure the document contains at least one page
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("Document has no pages.");
                return;
            }

            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle that bounds the highlighted text
            // Coordinates are in points: lower‑left (llx, lly) and upper‑right (urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 520);

            // Create a highlight annotation on the specified page and rectangle
            HighlightAnnotation highlight = new HighlightAnnotation(page, rect)
            {
                // Set the visual highlight color (yellow is typical)
                Color = Aspose.Pdf.Color.Yellow,
                // Optional popup text that appears when the annotation is selected
                Contents = "Important text highlighted"
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(highlight);

            // Save the modified document as a PDF file
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Highlight annotation added and saved to '{outputPdf}'.");
    }
}