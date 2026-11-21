using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "bidirectional.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single page (Pages are 1‑based)
            Page page = doc.Pages.Add();

            // Mixed left‑to‑right (English) and right‑to‑left (Hebrew, Arabic) text
            // Unicode characters are used directly in the string literal
            string mixedText = "Hello, שלום, مرحبا!";

            // Create a TextFragment containing the mixed text
            TextFragment fragment = new TextFragment(mixedText);

            // Configure the TextState for proper rendering of RTL characters
            TextState ts = fragment.TextState;
            ts.Font = FontRepository.FindFont("Arial"); // Font that supports both LTR and RTL glyphs
            ts.FontSize = 14;
            // Note: Aspose.Pdf automatically handles bidirectional layout when a Unicode‑capable font is used.

            // Position the fragment on the page
            fragment.Position = new Position(50, 750);

            // Add the fragment to the page's paragraph collection
            page.Paragraphs.Add(fragment);

            // Save the document as PDF (no SaveOptions needed for PDF output)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to {outputPath}");
    }
}
