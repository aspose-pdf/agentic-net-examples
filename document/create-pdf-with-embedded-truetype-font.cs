using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string outputPdf = "EmbeddedFontOutput.pdf";
        const string ttfPath   = @"C:\Windows\Fonts\arial.ttf"; // TrueType font file

        // Ensure the TrueType font file exists
        if (!File.Exists(ttfPath))
        {
            Console.Error.WriteLine($"Font file not found: {ttfPath}");
            return;
        }

        // Create a new PDF document and embed the TrueType font
        using (Document doc = new Document())
        {
            // Load the TrueType font from file
            Font ttfFont = FontRepository.OpenFont(ttfPath);
            // Mark the font to be embedded into the PDF
            ttfFont.IsEmbedded = true;

            // Optional: embed standard Type1 fonts if they are used elsewhere
            doc.EmbedStandardFonts = true;

            // Add a page
            Page page = doc.Pages.Add();

            // Create a text fragment using the embedded font
            TextFragment fragment = new TextFragment("Sample text with embedded TrueType font.");
            fragment.TextState.Font = ttfFont;
            fragment.TextState.FontSize = 14;
            fragment.TextState.ForegroundColor = Color.Black;

            // Position the text on the page (optional)
            fragment.Position = new Position(100, 700);

            // Add the fragment to the page
            page.Paragraphs.Add(fragment);

            // Save the PDF – fonts are embedded as part of the document
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF created with embedded TrueType font: {outputPdf}");
    }
}