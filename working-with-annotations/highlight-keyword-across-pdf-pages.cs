using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "highlighted_output.pdf";
        const string keyword    = "yourKeyword"; // replace with the word to highlight

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate through each page and search for the keyword on that page.
            // This approach avoids the need for a non‑existent TextFragment.PageNumber property.
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Search for all occurrences of the keyword on the current page.
                TextFragmentAbsorber absorber = new TextFragmentAbsorber(keyword);
                page.Accept(absorber);

                // Iterate over each found text fragment on this page.
                foreach (TextFragment fragment in absorber.TextFragments)
                {
                    // The fragment provides its bounding rectangle (Aspose.Pdf.Rectangle)
                    Aspose.Pdf.Rectangle rect = fragment.Rectangle;

                    // Create a highlight annotation on the identified page and rectangle
                    HighlightAnnotation highlight = new HighlightAnnotation(page, rect)
                    {
                        // Set the highlight color (use Aspose.Pdf.Color to avoid ambiguity)
                        Color = Aspose.Pdf.Color.Yellow
                    };

                    // Add the annotation to the page's annotation collection
                    page.Annotations.Add(highlight);
                }
            }

            // Save the modified document (lifecycle rule: use Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Keyword highlights applied and saved to '{outputPath}'.");
    }
}
