using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_numbered.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based in Aspose.Pdf
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create a text fragment with the custom prefix and page number
                TextFragment tf = new TextFragment($"Chapter {i}");

                // Set visual properties
                tf.TextState.FontSize = 12;
                tf.TextState.Font = FontRepository.FindFont("Arial");
                tf.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

                // Position near the bottom‑right corner
                double margin = 20;
                double x = page.PageInfo.Width - margin;
                double y = margin;
                tf.Position = new Position(x, y);
                tf.HorizontalAlignment = HorizontalAlignment.Right;
                tf.VerticalAlignment = VerticalAlignment.Bottom;

                // Add the fragment to the page
                page.Paragraphs.Add(tf);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page numbers added and saved to '{outputPath}'.");
    }
}