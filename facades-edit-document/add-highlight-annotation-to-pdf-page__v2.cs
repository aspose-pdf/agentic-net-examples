using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Annotations;
// System.Drawing is kept for potential future use, but all Aspose.Pdf types are fully qualified to avoid ambiguity.
using System.Drawing; // only needed if you explicitly use System.Drawing types elsewhere

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "highlighted_output.pdf";
        const string searchText = "sample text"; // text to highlight on page 3

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // 1. Locate the rectangle of the selected text on page 3
        Aspose.Pdf.Rectangle textRect;
        using (Document doc = new Document(inputPath))
        {
            // Ensure page indexing is 1‑based
            if (doc.Pages.Count < 3)
            {
                Console.Error.WriteLine("The document has fewer than 3 pages.");
                return;
            }

            // Search for the text fragment
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(searchText);
            doc.Pages[3].Accept(absorber);

            if (absorber.TextFragments.Count == 0)
            {
                Console.Error.WriteLine($"Text \"{searchText}\" not found on page 3.");
                return;
            }

            // Take the first occurrence's rectangle
            textRect = absorber.TextFragments[1].Rectangle;
        }

        // 2. Add a highlight annotation using the Document API (not PdfAnnotationEditor)
        using (Document doc = new Document(inputPath))
        {
            Page page = doc.Pages[3];
            var highlight = new HighlightAnnotation(page, textRect)
            {
                Color = Aspose.Pdf.Color.Yellow, // fully qualified to avoid CS0104
                Title = "Highlight",
                Contents = searchText
            };
            page.Annotations.Add(highlight);
            doc.Save(outputPath);
        }

        Console.WriteLine($"Highlight annotation added. Saved to '{outputPath}'.");
    }
}
