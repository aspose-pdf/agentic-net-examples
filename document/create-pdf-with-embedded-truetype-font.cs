using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Output PDF path
        const string outputPath = "EmbeddedFont.pdf";
        // Path to a TrueType font file (ensure the file exists on disk)
        const string ttfPath = "arial.ttf";

        if (!File.Exists(ttfPath))
        {
            Console.Error.WriteLine($"Font file not found: {ttfPath}");
            return;
        }

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Open the TrueType font and mark it for embedding
            Font trueTypeFont;
            using (FileStream fontStream = File.OpenRead(ttfPath))
            {
                trueTypeFont = FontRepository.OpenFont(fontStream, FontTypes.TTF);
            }
            trueTypeFont.IsEmbedded = true;   // Ensure the font is embedded
            trueTypeFont.IsSubset = false;    // Embed the full font (optional)

            // Create a text fragment using the embedded font
            TextFragment fragment = new TextFragment("Hello, embedded TrueType font!");
            fragment.TextState.Font = trueTypeFont;
            fragment.TextState.FontSize = 24;
            fragment.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;
            fragment.Position = new Position(100, 700); // Position on the page

            // Add the text fragment to the page
            page.Paragraphs.Add(fragment);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}' with embedded TrueType font.");
    }
}