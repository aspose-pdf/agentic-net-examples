using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "bidirectional_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Wrap Document in a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Add a new page where the bidirectional text will be placed
            Page page = doc.Pages.Add();

            // Mixed left‑to‑right (English) and right‑to‑left (Arabic, Hebrew) Unicode text
            string bidiText = "English text, العربية, שלום";

            // Create a TextFragment containing the Unicode sequence
            TextFragment fragment = new TextFragment(bidiText);

            // Use a Unicode font that supports Arabic and Hebrew characters.
            // Aspose.PDF automatically renders RTL scripts when a suitable font is used.
            fragment.TextState.Font = FontRepository.FindFont("Arial Unicode MS");
            fragment.TextState.FontSize = 14;

            // Add the fragment to the page's paragraph collection
            page.Paragraphs.Add(fragment);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Bidirectional PDF saved to '{outputPath}'.");
    }
}
