using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "right_aligned.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF, modify, and save using proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure there is at least one page
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("Document has no pages.");
                return;
            }

            // Get the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Create a text fragment with the desired content
            TextFragment fragment = new TextFragment("Right‑aligned text example");

            // Set horizontal alignment to Right using TextState
            fragment.TextState.HorizontalAlignment = Aspose.Pdf.HorizontalAlignment.Right;

            // Optionally set position if needed (e.g., top‑right corner)
            // fragment.Position = new Position(400, 750);

            // Add the fragment to the page's paragraphs collection
            page.Paragraphs.Add(fragment);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with right‑aligned text to '{outputPath}'.");
    }
}