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
            // Get the first page (pages are 1‑based)
            Page page = doc.Pages[1];

            // Create a text fragment
            TextFragment fragment = new TextFragment("Semi‑transparent text");

            // Set opacity by using a color with an alpha component (0‑255).
            // 128 (≈50%) alpha makes the text semi‑transparent.
            fragment.TextState.ForegroundColor = Color.FromArgb(128, 0, 0, 0); // 50% transparent black

            // Optional styling
            fragment.TextState.Font = FontRepository.FindFont("Arial");
            fragment.TextState.FontSize = 24;

            // Add the fragment to the page
            page.Paragraphs.Add(fragment);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with transparent text to '{outputPath}'.");
    }
}
