using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_footnotes.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF and ensure deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate using 1‑based page indexing (Aspose.Pdf rule)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create a footnote reference with the page number
                TextFragment footnote = new TextFragment(i.ToString());

                // Superscript formatting: smaller font size
                footnote.TextState.FontSize = 8;               // smaller than body text
                footnote.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

                // Position near the bottom‑right corner (adjust margins as needed)
                double marginRight = 30;   // distance from right edge
                double marginBottom = 20;  // distance from bottom edge
                double x = page.PageInfo.Width - marginRight;
                // Raise the baseline by increasing the Y coordinate (no TextRise property)
                double y = marginBottom + 5; // raise 5 points for superscript effect

                footnote.Position = new Position(x, y);

                // Add the fragment to the page's paragraph collection
                page.Paragraphs.Add(footnote);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with superscript footnote page numbers to '{outputPath}'.");
    }
}
