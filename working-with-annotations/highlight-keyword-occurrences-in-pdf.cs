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
        const string outputPath = "highlighted.pdf";
        const string keyword    = "yourKeyword"; // replace with the word to highlight

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Search for all occurrences of the keyword in the whole document
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(keyword);
            doc.Pages.Accept(absorber); // accept absorber for all pages

            // Iterate over each found text fragment
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                // The TextFragment provides direct access to the page it resides on.
                Page page = fragment.Page; // 1‑based page reference obtained from the fragment

                // Get the rectangle of the fragment (coordinates are page‑relative)
                Aspose.Pdf.Rectangle rect = fragment.Rectangle;

                // Create a highlight annotation on that page and rectangle
                HighlightAnnotation highlight = new HighlightAnnotation(page, rect)
                {
                    Color   = Aspose.Pdf.Color.Yellow, // highlight color
                    Opacity = 0.5                         // semi‑transparent
                };

                // Add the annotation to the page's annotation collection
                page.Annotations.Add(highlight);
            }

            // Save the modified PDF (lifecycle rule: use the same Document instance)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Keyword highlights saved to '{outputPath}'.");
    }
}
