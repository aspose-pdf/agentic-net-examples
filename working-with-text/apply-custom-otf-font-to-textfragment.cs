using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string fontPath   = "MyFont.otf";      // OpenType font file
        const string outputPath = "styled_text.pdf";

        // Verify font file exists
        if (!File.Exists(fontPath))
        {
            Console.Error.WriteLine($"Font file not found: {fontPath}");
            return;
        }

        // Load the custom OTF font
        Font customFont = FontRepository.OpenFont(fontPath);
        // Ensure the font will be embedded in the PDF
        customFont.IsEmbedded = true;

        // Create a new PDF document (wrapped in using for deterministic disposal)
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Create a TextFragment with the desired text
            TextFragment tf = new TextFragment("Hello, Aspose.Pdf with custom OTF font!");

            // Apply the loaded font to the TextFragment
            tf.TextState.Font = customFont;
            tf.TextState.FontSize = 24;                     // optional styling
            tf.TextState.ForegroundColor = Color.Black;     // optional color

            // Position the text on the page (optional)
            tf.Position = new Position(100, 700);

            // Add the TextFragment to the page
            page.Paragraphs.Add(tf);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}