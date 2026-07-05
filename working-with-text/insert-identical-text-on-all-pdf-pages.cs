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
        const string textToInsert = "Sample plain text";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create a text fragment with the desired content
                TextFragment tf = new TextFragment(textToInsert);

                // Position the text on the page (example coordinates)
                tf.Position = new Position(100, 700);

                // Optional: set basic text appearance
                tf.TextState.FontSize = 12;
                tf.TextState.Font = FontRepository.FindFont("Helvetica");
                tf.TextState.ForegroundColor = Color.Black;

                // Add the fragment to the page's Paragraphs collection
                page.Paragraphs.Add(tf);
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text inserted on all pages and saved to '{outputPath}'.");
    }
}