using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "embedded_font.pdf";
        const string ttfPath = @"C:\Windows\Fonts\arial.ttf"; // adjust if needed

        if (!File.Exists(ttfPath))
        {
            Console.Error.WriteLine($"TrueType font not found: {ttfPath}");
            return;
        }

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Load the TrueType font from a stream and mark it for embedding
            Font trueTypeFont;
            using (FileStream fontStream = File.OpenRead(ttfPath))
            {
                trueTypeFont = FontRepository.OpenFont(fontStream, FontTypes.TTF);
            }
            trueTypeFont.IsEmbedded = true;

            // Create a text fragment that uses the embedded font
            TextFragment fragment = new TextFragment("Sample text with embedded TrueType font.");
            fragment.TextState.Font = trueTypeFont;
            fragment.TextState.FontSize = 14;
            fragment.TextState.ForegroundColor = Color.Black;

            // Add the text fragment to the page
            page.Paragraphs.Add(fragment);

            // Ensure any standard Type1 fonts are also embedded if used
            doc.EmbedStandardFonts = true;

            // Save the PDF with the embedded TrueType font
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF created at '{outputPath}' with embedded TrueType font.");
    }
}
