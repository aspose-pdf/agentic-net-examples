using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Path to the TrueType font file (could be any .ttf file)
        const string fontPath = "custom_font.ttf";
        // Path to the output PDF
        const string outputPdf = "output.pdf";

        // Load the font – if the file does not exist fall back to a system font (e.g., Arial)
        Font customFont;
        if (File.Exists(fontPath))
        {
            // Load the font bytes into memory
            byte[] fontBytes = File.ReadAllBytes(fontPath);
            using (MemoryStream fontStream = new MemoryStream(fontBytes))
            {
                // Open the font from the memory stream (TTF format)
                customFont = FontRepository.OpenFont(fontStream, FontTypes.TTF);
            }
        }
        else
        {
            // Graceful fallback – use a built‑in font that is always available
            Console.WriteLine($"Font file '{fontPath}' not found. Falling back to Arial.");
            customFont = FontRepository.FindFont("Arial");
        }

        // Ensure the font will be embedded in the PDF (has effect for custom fonts only)
        if (customFont != null)
        {
            customFont.IsEmbedded = true;
        }
        else
        {
            throw new InvalidOperationException("Unable to obtain a font for the PDF.");
        }

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a text fragment and assign the custom font via TextState
            TextFragment fragment = new TextFragment("Hello with custom font!");
            fragment.TextState.Font = customFont;          // assign the custom font
            fragment.TextState.FontSize = 24;              // optional: set font size
            fragment.TextState.ForegroundColor = Color.Black; // set text color

            // Add the fragment to the page
            page.Paragraphs.Add(fragment);

            // Save the PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved to '{outputPdf}' with custom TrueType font.");
    }
}
