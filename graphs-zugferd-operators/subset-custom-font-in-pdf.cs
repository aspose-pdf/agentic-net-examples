using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string fontPath   = "MyFont.ttf";          // Path to the custom TrueType/OpenType font
        const string outputPath = "subset_font.pdf";    // Resulting PDF file

        if (!File.Exists(fontPath))
        {
            Console.Error.WriteLine($"Font file not found: {fontPath}");
            return;
        }

        // Load the font and configure it for subset embedding.
        // Subset mode ensures only glyphs actually used in the document are embedded,
        // dramatically reducing the PDF size while preserving required characters.
        Font customFont = FontRepository.OpenFont(fontPath);
        customFont.IsEmbedded = true;   // embed the font into the PDF
        customFont.IsSubset   = true;   // enable subset embedding (only used glyphs)

        // Create the PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document())
        {
            // Add a single page.
            Page page = doc.Pages.Add();

            // Create a text fragment that uses the subsetted font.
            TextFragment fragment = new TextFragment("Hello, Aspose PDF with subsetted font!");
            fragment.TextState.Font     = customFont;
            fragment.TextState.FontSize = 24;
            fragment.Position = new Position(50, 750); // Position on the page (x, y)

            // Add the fragment to the page's paragraph collection.
            page.Paragraphs.Add(fragment);

            // Save the document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}' with subsetted font.");
    }
}
