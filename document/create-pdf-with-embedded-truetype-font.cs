using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Output PDF path
        const string outputPath = "EmbeddedTrueType.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Ensure standard Type1 fonts are also embedded if used
            doc.EmbedStandardFonts = true;

            // Add a blank page
            Page page = doc.Pages.Add();

            // Load a TrueType font from the system (e.g., Arial)
            // FontRepository.FindFont returns a Font object that can be embedded
            Font trueTypeFont = FontRepository.FindFont("Arial");
            trueTypeFont.IsEmbedded = true; // Mark the font for embedding

            // Create a text fragment with sample content
            TextFragment fragment = new TextFragment("This PDF uses an embedded TrueType font.");
            fragment.TextState.Font = trueTypeFont;   // Apply the embedded font
            fragment.TextState.FontSize = 14;        // Set desired size
            fragment.TextState.ForegroundColor = Color.Black;

            // Add the text fragment to the page
            page.Paragraphs.Add(fragment);

            // Save the document; the TrueType font will be embedded in the output PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}' with embedded TrueType font.");
    }
}