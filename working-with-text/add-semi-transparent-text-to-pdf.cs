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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Create a text fragment
            TextFragment fragment = new TextFragment("Semi‑transparent text");

            // Configure opacity via the alpha component of the foreground color (0‑255).
            // 128 = 50 % opacity. Keep the original blue hue.
            fragment.TextState.ForegroundColor = Aspose.Pdf.Color.FromArgb(128, 0, 0, 255);

            // Optionally set other visual properties
            fragment.TextState.FontSize = 24;
            // If you also want a solid color elsewhere, you can set it separately.

            // Add the fragment to the page
            page.Paragraphs.Add(fragment);

            // Save the modified PDF (still PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with transparent text to '{outputPath}'.");
    }
}
