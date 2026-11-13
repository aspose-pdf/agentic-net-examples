using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block (lifecycle rule)
        using (Document doc = new Document(inputPath))
        {
            int totalPages = doc.Pages.Count; // total number of pages

            // Iterate pages using 1‑based indexing (global rule)
            for (int i = 1; i <= totalPages; i++)
            {
                Page page = doc.Pages[i];

                // Build the page number string with custom separator
                string pageNumber = $"{i}/{totalPages}";

                // Create a text fragment for the page number
                TextFragment tf = new TextFragment(pageNumber);
                tf.TextState.FontSize = 12;
                tf.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

                // Position the fragment near the bottom‑right corner
                tf.Position = new Position(page.PageInfo.Width - 50, 20);
                tf.HorizontalAlignment = HorizontalAlignment.Right;

                // Add the fragment to the page
                page.Paragraphs.Add(tf);
            }

            // Save the modified PDF (lifecycle rule)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page numbers added and saved to '{outputPath}'.");
    }
}