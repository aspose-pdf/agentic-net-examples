using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_links.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block (ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Ensure that at least two pages exist for the internal link target.
            if (doc.Pages.Count < 2)
                doc.Pages.Add(); // adds a blank page as page 2

            // -----------------------------------------------------------------
            // 1) Add an internal link that jumps to page 2 (fit the whole page)
            // -----------------------------------------------------------------
            Page page1 = doc.Pages[1]; // first page (1‑based indexing)

            // Define the clickable rectangle (llx, lly, urx, ury)
            Rectangle internalRect = new Rectangle(100, 500, 300, 550);

            // Create the link annotation on page 1
            LinkAnnotation internalLink = new LinkAnnotation(page1, internalRect)
            {
                Color = Color.Blue,          // visual color of the link border
                Contents = "Go to page 2",   // tooltip text
                Highlighting = HighlightingMode.Invert   // visual effect on mouse over
            };

            // Destination: page 2, fit to window
            internalLink.Destination = new FitExplicitDestination(doc.Pages[2]);

            // Add the annotation to the page
            page1.Annotations.Add(internalLink);

            // -----------------------------------------------------------------
            // 2) Add an external link that opens a web URL
            // -----------------------------------------------------------------
            // Define another rectangle on the same page
            Rectangle externalRect = new Rectangle(100, 400, 300, 450);

            LinkAnnotation externalLink = new LinkAnnotation(page1, externalRect)
            {
                Color = Color.Red,
                Contents = "Open Aspose website",
                Highlighting = HighlightingMode.Invert
            };

            // Use GoToURIAction for external URLs (Hyperlink property cannot be set directly)
            externalLink.Action = new GoToURIAction("https://www.aspose.com");

            // Add the external link annotation
            page1.Annotations.Add(externalLink);

            // -----------------------------------------------------------------
            // Save the modified PDF (PDF format)
            // -----------------------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with link annotations saved to '{outputPath}'.");
    }
}
