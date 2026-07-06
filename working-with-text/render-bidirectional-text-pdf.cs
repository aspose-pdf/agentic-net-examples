using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "bidi_output.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single page to the document
            Page page = doc.Pages.Add();

            // Construct a Unicode string that mixes right‑to‑left (Arabic) and left‑to‑right (English) text.
            // The Unicode control characters U+202B (RLE) and U+202C (PDF) enforce right‑to‑left embedding.
            string bidiText = "\u202B" +               // Right‑to‑left embedding start
                              "مرحبا بالعالم " +      // Arabic phrase: “Hello World”
                              "\u202C" +               // Pop directional formatting (end embedding)
                              "Hello World";           // English phrase

            // Create a TextFragment containing the bidirectional text
            TextFragment fragment = new TextFragment(bidiText);

            // Position the fragment on the page (coordinates are from the bottom‑left corner)
            fragment.Position = new Aspose.Pdf.Text.Position(100, 700);

            // Configure the visual appearance via TextState
            TextState ts = fragment.TextState;
            ts.Font = FontRepository.FindFont("Arial");   // Use a font that supports Arabic glyphs
            ts.FontSize = 20;
            ts.ForegroundColor = Aspose.Pdf.Color.Black;

            // If the TextState class in the used version exposes a Direction property,
            // it can be set to enforce right‑to‑left rendering:
            // ts.Direction = Aspose.Pdf.Direction.R2L;

            // Add the fragment to the page's paragraph collection
            page.Paragraphs.Add(fragment);

            // Save the resulting PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Bidirectional PDF saved to '{outputPath}'.");
    }
}