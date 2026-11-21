using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_numbered.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Total number of pages (1‑based indexing)
            int totalPages = doc.Pages.Count;

            // Iterate over all pages using 1‑based index as required by Aspose.Pdf
            for (int i = 1; i <= totalPages; i++)
            {
                Page page = doc.Pages[i];

                // Build the custom page number text
                string pageNumberText = $"Page {i} of {totalPages}";

                // Create a TextFragment with the page number
                TextFragment tf = new TextFragment(pageNumberText);

                // Set visual appearance (font size, color, etc.)
                tf.TextState.FontSize = 12;
                tf.TextState.ForegroundColor = Aspose.Pdf.Color.Black;
                tf.TextState.Font = FontRepository.FindFont("Arial");

                // Position the text near the bottom‑right corner
                // Adjust margins as needed
                const double marginRight = 40;
                const double marginBottom = 30;

                double x = page.PageInfo.Width - marginRight;
                double y = marginBottom;

                // TextFragment.Position expects Aspose.Pdf.Text.Position
                tf.Position = new Position(x, y);

                // Add the fragment to the page's paragraph collection
                page.Paragraphs.Add(tf);
            }

            // Save the modified PDF (still PDF format, so no SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page numbers added and saved to '{outputPath}'.");
    }
}