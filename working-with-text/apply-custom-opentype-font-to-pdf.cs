using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Path to the custom OpenType (OTF) font file – resolved relative to the executable folder
        string fontPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "custom.otf");

        // Verify that the font file exists before trying to load it
        if (!File.Exists(fontPath))
        {
            Console.Error.WriteLine($"Error: Font file not found at '{fontPath}'.");
            return;
        }

        // Output PDF file
        const string outputPath = "styled_text.pdf";

        // Load the OTF font from file
        Font customFont = FontRepository.OpenFont(fontPath);
        // Ensure the font will be embedded in the PDF
        customFont.IsEmbedded = true;

        // Create a new PDF document (wrapped in using for proper disposal)
        using (Document doc = new Document())
        {
            // Add a new page to the document
            Page page = doc.Pages.Add();

            // Create a TextFragment with the desired text
            TextFragment fragment = new TextFragment("Hello, Aspose PDF with OTF font!");

            // Apply the custom font to the text fragment
            fragment.TextState.Font = customFont;
            // Optional styling: set font size and color
            fragment.TextState.FontSize = 24;
            fragment.TextState.ForegroundColor = Color.Blue;

            // Add the text fragment to the page's paragraph collection
            page.Paragraphs.Add(fragment);

            // Save the PDF document to the specified path
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
