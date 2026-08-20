using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input PDF, output PDF and the keyword to highlight
        const string inputPath  = "input.pdf";
        const string outputPath = "highlighted_output.pdf";
        const string keyword    = "sample"; // change to the desired word

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                // Create a TextFragmentAbsorber that searches for the keyword
                TextFragmentAbsorber absorber = new TextFragmentAbsorber(keyword);

                // Search the current page
                doc.Pages[pageIndex].Accept(absorber);

                // For each found text fragment, create a highlight annotation
                foreach (TextFragment fragment in absorber.TextFragments)
                {
                    // The rectangle that bounds the text fragment
                    Aspose.Pdf.Rectangle rect = fragment.Rectangle;

                    // Create the highlight annotation on the current page
                    HighlightAnnotation highlight = new HighlightAnnotation(doc.Pages[pageIndex], rect);

                    // Set visual appearance (yellow highlight, 50% opacity)
                    highlight.Color   = Aspose.Pdf.Color.Yellow;
                    highlight.Opacity = 0.5;

                    // Add the annotation to the page
                    doc.Pages[pageIndex].Annotations.Add(highlight);
                }
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Keyword highlights saved to '{outputPath}'.");
    }
}