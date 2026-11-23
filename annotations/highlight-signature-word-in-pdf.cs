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
        const string outputPath = "HighlightedSignature.pdf";
        const string searchWord = "Signature";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Search for the word "Signature" on all pages
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(searchWord);
            // Accept the absorber on the whole document
            doc.Pages.Accept(absorber);

            // Iterate over all found fragments
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                // The page that contains the fragment
                Page page = fragment.Page;
                // The fragment's rectangle defines the area to highlight
                Aspose.Pdf.Rectangle rect = fragment.Rectangle;

                // Create a highlight annotation for the fragment's page
                HighlightAnnotation highlight = new HighlightAnnotation(page, rect)
                {
                    // MarkupAnnotation does not expose an Author property; use Subject instead
                    Subject = "Alice",
                    Opacity = 0.9
                };

                // Add the annotation to the page's annotation collection
                page.Annotations.Add(highlight);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Highlighted PDF saved to '{outputPath}'.");
    }
}
