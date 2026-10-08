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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Footer text with current date
            string footer = $"Generated on {DateTime.Now:yyyy-MM-dd}";

            // Pages are 1‑based in Aspose.Pdf
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create a text fragment for the footer
                TextFragment tf = new TextFragment(footer);
                tf.TextState.FontSize = 9;
                tf.TextState.ForegroundColor = Aspose.Pdf.Color.Gray;

                // Position the footer near the bottom of the page (20 points from bottom, 50 points from left)
                tf.Position = new Position(50, 20);

                // Add the fragment to the page's paragraph collection
                page.Paragraphs.Add(tf);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Footer added and saved to '{outputPath}'.");
    }
}