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
        const string text = "Sample plain text";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create a TextFragment containing the identical text
                TextFragment tf = new TextFragment(text);

                // Set the position where the text will appear on the page
                tf.Position = new Position(100, 700);

                // Optional: define basic text appearance
                tf.TextState.FontSize = 12;
                tf.TextState.Font = FontRepository.FindFont("Helvetica");
                tf.TextState.ForegroundColor = Color.Black;

                // Add the fragment to the page's Paragraphs collection
                page.Paragraphs.Add(tf);
            }

            // Save the modified PDF (PDF format is the default)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text added to all pages and saved as '{outputPath}'.");
    }
}