using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputXps = "output.xps";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle area to be highlighted (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 520);

            // Create a HighlightAnnotation on the specified page and rectangle
            HighlightAnnotation highlight = new HighlightAnnotation(page, rect)
            {
                // Set the highlight color (use Aspose.Pdf.Color, not System.Drawing.Color)
                Color = Aspose.Pdf.Color.Yellow,
                // Optional: add a comment that appears in the annotation popup
                Contents = "Highlighted text"
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(highlight);

            // Save the document as XPS; must pass XpsSaveOptions explicitly
            XpsSaveOptions xpsOptions = new XpsSaveOptions();
            doc.Save(outputXps, xpsOptions);
        }

        Console.WriteLine($"PDF with highlight saved as XPS: {outputXps}");
    }
}