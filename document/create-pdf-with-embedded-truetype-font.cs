using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "embedded_font.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Load a TrueType font (e.g., Arial) and mark it for embedding
            Font trueTypeFont = FontRepository.FindFont("Arial");
            trueTypeFont.IsEmbedded = true;

            // Create a text fragment using the embedded font
            TextFragment fragment = new TextFragment("Hello, embedded TrueType font!");
            fragment.TextState.Font = trueTypeFont;
            fragment.TextState.FontSize = 24;
            fragment.Position = new Position(100, 700); // Position on the page

            // Add the text fragment to the page
            page.Paragraphs.Add(fragment);

            // If any standard Type1 fonts are used, embed them as well
            doc.EmbedStandardFonts = true;

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}' with embedded TrueType font.");
    }
}