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
        const string outputPath = "highlighted.pdf";
        const string keyword = "Aspose";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate through each page and search for the keyword on that page
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Create an absorber that searches for the keyword on the current page
                TextFragmentAbsorber absorber = new TextFragmentAbsorber(keyword);
                page.Accept(absorber);

                // Iterate over each found text fragment on this page
                foreach (TextFragment fragment in absorber.TextFragments)
                {
                    // Get the fragment's bounding rectangle
                    Aspose.Pdf.Rectangle rect = fragment.Rectangle;

                    // Create a highlight annotation for this rectangle on the current page
                    HighlightAnnotation highlight = new HighlightAnnotation(page, rect)
                    {
                        // Set a cross‑platform color (avoid System.Drawing)
                        Color = Aspose.Pdf.Color.Yellow
                    };

                    // Add the annotation to the page
                    page.Annotations.Add(highlight);
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Keyword highlights saved to '{outputPath}'.");
    }
}
