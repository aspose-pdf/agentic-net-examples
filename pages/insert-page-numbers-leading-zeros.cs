using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "numbered_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate pages using 1‑based indexing (Aspose.Pdf requirement)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Build page number with leading zero for single‑digit pages
                string pageNumber = i < 10 ? $"0{i}" : i.ToString();

                // Create a text fragment for the page number
                TextFragment tf = new TextFragment(pageNumber);

                // Set visual appearance (font, size, color)
                tf.TextState.Font = FontRepository.FindFont("Arial");
                tf.TextState.FontSize = 12;
                tf.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

                // Position the number at the bottom‑right corner
                // Adjust margins as needed; here we use 20 points from right and bottom edges
                double marginRight = 20;
                double marginBottom = 20;
                double x = page.PageInfo.Width - marginRight;
                double y = marginBottom;
                tf.Position = new Position(x, y);
                tf.HorizontalAlignment = HorizontalAlignment.Right;
                tf.VerticalAlignment   = VerticalAlignment.Bottom;

                // Add the fragment to the page's paragraphs collection
                page.Paragraphs.Add(tf);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page numbers added and saved to '{outputPath}'.");
    }
}