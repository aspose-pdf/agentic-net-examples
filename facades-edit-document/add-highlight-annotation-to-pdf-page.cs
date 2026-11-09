using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "highlighted_output.pdf";
        const string searchText = "Text to highlight"; // the exact text range to highlight

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF and locate the rectangle of the desired text on page 3
        Rectangle highlightRect = null;
        Document doc = new Document(inputPath);
        // Ensure we are working with page 3 (1‑based indexing)
        Page page = doc.Pages[3];

        // Search for the text fragment
        TextFragmentAbsorber absorber = new TextFragmentAbsorber(searchText);
        page.Accept(absorber);

        // If the text is found, take the first occurrence's rectangle
        if (absorber.TextFragments.Count > 0)
        {
            TextFragment fragment = absorber.TextFragments[1]; // collections are 1‑based
            highlightRect = fragment.Rectangle;
        }
        else
        {
            Console.Error.WriteLine($"Text \"{searchText}\" not found on page 3.");
            return;
        }

        // Add a highlight annotation directly to the page's Annotations collection
        var highlight = new HighlightAnnotation(page, highlightRect)
        {
            Color = Color.Yellow // use Aspose.Pdf.Color for the highlight color
        };
        page.Annotations.Add(highlight);

        // Save the modified document
        doc.Save(outputPath);

        Console.WriteLine($"Highlight annotation added and saved to '{outputPath}'.");
    }
}
