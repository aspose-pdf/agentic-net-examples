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
        const string content    = "Right aligned text";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure there is at least one page (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count == 0)
                doc.Pages.Add();

            Page page = doc.Pages[1]; // first page

            // Create a text fragment and set its horizontal alignment to Right
            TextFragment fragment = new TextFragment(content);
            fragment.TextState.HorizontalAlignment = HorizontalAlignment.Right;

            // Optional: set vertical position (X is ignored for right alignment)
            fragment.Position = new Position(0, 800);

            // Add the fragment to the page
            page.Paragraphs.Add(fragment);

            // Save the modified PDF (PDF format, no extra SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Right‑aligned PDF saved to '{outputPath}'.");
    }
}