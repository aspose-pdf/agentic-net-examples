using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_even_page_numbers.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based in Aspose.Pdf
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                // Apply page number only on even pages
                if (i % 2 == 0)
                {
                    // Create a text fragment with the page number
                    TextFragment tf = new TextFragment(i.ToString());

                    // Styling: font size, color, centered alignment
                    tf.TextState.FontSize = 12;
                    tf.TextState.ForegroundColor = Aspose.Pdf.Color.Gray;
                    tf.TextState.HorizontalAlignment = HorizontalAlignment.Center;

                    // Position near the bottom of the page (y = 20 points from bottom)
                    tf.Position = new Position(0, 20); // X = 0 works with Center alignment

                    // Add the fragment to the current page
                    Page page = doc.Pages[i];
                    page.Paragraphs.Add(tf);
                }
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved PDF with page numbers on even pages to '{outputPath}'.");
    }
}