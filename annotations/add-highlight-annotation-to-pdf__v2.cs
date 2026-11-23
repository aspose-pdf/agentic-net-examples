using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "HighlightedTerms.pdf";
        const string searchText = "Terms and Conditions";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Locate the phrase in the document
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(searchText);
            doc.Pages.Accept(absorber);

            if (absorber.TextFragments.Count == 0)
            {
                Console.WriteLine($"Phrase \"{searchText}\" not found.");
                doc.Save(outputPath); // Save unchanged
                return;
            }

            // Use the first occurrence (TextFragmentCollection is 1‑based)
            TextFragment fragment = absorber.TextFragments[1];
            int pageNumber = fragment.Page.Number; // corrected property access
            Page page = doc.Pages[pageNumber];

            // Create a highlight annotation over the found rectangle
            HighlightAnnotation highlight = new HighlightAnnotation(page, fragment.Rectangle);
            highlight.Title = "Legal";                     // author
            highlight.Opacity = 0.9;                       // opacity
            highlight.Color = Aspose.Pdf.Color.Yellow;    // typical highlight color

            // Attach the annotation to the page
            page.Annotations.Add(highlight);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved highlighted PDF to '{outputPath}'.");
    }
}
