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
        const string keyword    = "YOUR_KEYWORD"; // replace with the word to highlight

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];

                // Search for the keyword on the current page
                TextFragmentAbsorber absorber = new TextFragmentAbsorber(keyword);
                page.Accept(absorber);

                // Add a highlight annotation for each occurrence found
                foreach (TextFragment fragment in absorber.TextFragments)
                {
                    // The rectangle of the fragment defines the area to highlight
                    Aspose.Pdf.Rectangle rect = fragment.Rectangle;

                    // Create the highlight annotation and set its visual properties
                    HighlightAnnotation highlight = new HighlightAnnotation(page, rect)
                    {
                        Color = Aspose.Pdf.Color.Yellow, // highlight color
                        Opacity = 0.5,                    // semi‑transparent
                        Contents = $"Highlighted \"{keyword}\""
                    };

                    // Attach the annotation to the page
                    page.Annotations.Add(highlight);
                }
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Highlighting completed. Output saved to '{outputPath}'.");
    }
}