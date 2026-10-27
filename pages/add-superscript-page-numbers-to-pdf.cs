using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_footnote_numbers.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate pages using 1‑based indexing (Aspose.Pdf requirement)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create a TextFragment containing the page number
                TextFragment pageNumber = new TextFragment(i.ToString());

                // Superscript‑like formatting: smaller font size and raised Y position
                pageNumber.TextState.FontSize = 8; // smaller than normal body text

                // Position the fragment near the bottom left of the page.
                // Raise the Y coordinate to simulate superscript (no Rise property exists).
                double x = 50;   // 50 points from the left edge
                double y = 24;   // raised 4 points above a typical footnote baseline (20 pts)
                pageNumber.Position = new Position(x, y);

                // Add the fragment to the page's paragraph collection
                page.Paragraphs.Add(pageNumber);
            }

            // Save the modified PDF (no extra SaveOptions needed for PDF output)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with superscript footnote page numbers: '{outputPath}'");
    }
}
