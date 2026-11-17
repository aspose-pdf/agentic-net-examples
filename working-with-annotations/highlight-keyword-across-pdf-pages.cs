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
        const string outputPath = "highlighted_output.pdf";
        const string keyword = "YOUR_KEYWORD"; // replace with the word to highlight

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Search for all occurrences of the keyword
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(keyword);
            // Accept the absorber on all pages
            doc.Pages.Accept(absorber);

            // Iterate over each found fragment
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                // Get the page that contains the fragment (pages are 1‑based)
                Page page = fragment.Page; // fragment.Page returns the Page object

                // The rectangle of the fragment defines the area to highlight
                Aspose.Pdf.Rectangle rect = fragment.Rectangle;

                // Create a highlight annotation for that rectangle
                HighlightAnnotation highlight = new HighlightAnnotation(page, rect)
                {
                    // Use a cross‑platform color (Aspose.Pdf.Color, not System.Drawing.Color)
                    Color = Aspose.Pdf.Color.Yellow
                };

                // Add the annotation to the page
                page.Annotations.Add(highlight);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Keyword highlights saved to '{outputPath}'.");
    }
}
