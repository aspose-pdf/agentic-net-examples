using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class HighlightKeyword
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "highlighted_output.pdf";
        const string keyword = "Aspose";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate through each page and search for the keyword on that page.
            // This approach avoids the need for the non‑existent TextFragment.PageNumber property.
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Search for the keyword on the current page only.
                TextFragmentAbsorber absorber = new TextFragmentAbsorber(keyword);
                page.Accept(absorber);

                // Apply a highlight annotation to every found fragment on this page.
                foreach (TextFragment fragment in absorber.TextFragments)
                {
                    HighlightAnnotation highlight = new HighlightAnnotation(page, fragment.Rectangle)
                    {
                        Color = Aspose.Pdf.Color.Yellow // optional: set highlight colour
                    };
                    page.Annotations.Add(highlight);
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Keyword highlights saved to '{outputPath}'.");
    }
}
