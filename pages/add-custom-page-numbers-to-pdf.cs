using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Total number of pages (1‑based indexing)
            int totalPages = doc.Pages.Count;

            // Iterate through all pages using 1‑based index
            for (int i = 1; i <= totalPages; i++)
            {
                Page page = doc.Pages[i];

                // Create the page number text "Page X of Y"
                string pageNumberText = $"Page {i} of {totalPages}";
                TextFragment tf = new TextFragment(pageNumberText);

                // Optional: set font size and color
                tf.TextState.FontSize = 12;
                tf.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

                // Position the text near the bottom‑right corner
                // Adjust margins as needed (e.g., 20 points from right and bottom)
                double marginRight = 20;
                double marginBottom = 20;
                double x = page.PageInfo.Width - marginRight - tf.TextState.FontSize * pageNumberText.Length * 0.5;
                double y = marginBottom;

                tf.Position = new Position(x, y);

                // Add the fragment to the page's paragraphs collection
                page.Paragraphs.Add(tf);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page numbers added and saved to '{outputPath}'.");
    }
}