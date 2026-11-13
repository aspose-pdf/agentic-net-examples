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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based; iterate through all pages
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                // Apply page number only on odd pages
                if (i % 2 == 1)
                {
                    // Create a text fragment with the page number
                    TextFragment tf = new TextFragment(i.ToString());

                    // Position the number (example: bottom center)
                    tf.Position = new Position(300, 20); // adjust X/Y as needed
                    tf.TextState.FontSize = 12;
                    tf.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

                    // Add the fragment to the page's paragraph collection
                    doc.Pages[i].Paragraphs.Add(tf);
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page numbers added to odd pages. Saved as '{outputPath}'.");
    }
}