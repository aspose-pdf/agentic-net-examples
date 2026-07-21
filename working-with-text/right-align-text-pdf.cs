using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "right_aligned.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Create a text fragment with the desired content
            TextFragment fragment = new TextFragment("Right aligned text");

            // Set the horizontal alignment to Right
            fragment.TextState.HorizontalAlignment = HorizontalAlignment.Right;

            // Position the baseline of the text (X is ignored for right alignment)
            fragment.Position = new Position(100, 700);

            // Add the fragment to the page
            page.Paragraphs.Add(fragment);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved right‑aligned PDF to '{outputPath}'.");
    }
}