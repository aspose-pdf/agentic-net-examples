using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // ---------------------------------------------------------------------
        // Load a TrueType font from an in‑memory source.
        // If the Base64 string is missing, empty or malformed we fall back to a
        // built‑in font (e.g., Arial) so the program can still run without throwing.
        // ---------------------------------------------------------------------
        const string base64Font = ""; // Insert a valid Base64‑encoded .ttf if available.
        Font customFont;

        // Quick guard – an empty or whitespace string cannot be a valid Base64 font.
        if (string.IsNullOrWhiteSpace(base64Font))
        {
            customFont = FontRepository.FindFont("Arial");
        }
        else
        {
            try
            {
                // Attempt to decode the Base64 string.
                byte[] fontBytes = Convert.FromBase64String(base64Font);
                using (MemoryStream fontStream = new MemoryStream(fontBytes))
                {
                    // Open the font from the stream – the overload that accepts a stream
                    // and the font type (TTF).
                    customFont = FontRepository.OpenFont(fontStream, FontTypes.TTF);
                    // Ensure the font is embedded in the generated PDF.
                    customFont.IsEmbedded = true;
                }
            }
            catch (Exception)
            {
                // Fallback: use a standard system font that Aspose.PDF knows about.
                // "Arial" is a safe default that exists on most platforms.
                customFont = FontRepository.FindFont("Arial");
            }
        }

        // Create the PDF document.
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Create a text fragment and assign the custom (or fallback) font via TextState.
            TextFragment fragment = new TextFragment("Sample text with custom font");
            fragment.TextState.Font = customFont;          // custom font applied (or fallback)
            fragment.TextState.FontSize = 14;              // optional size
            fragment.TextState.ForegroundColor = Aspose.Pdf.Color.Blue; // optional color

            page.Paragraphs.Add(fragment);

            // Save the PDF.
            doc.Save("output.pdf");
        }
    }
}
