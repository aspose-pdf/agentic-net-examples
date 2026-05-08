using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string outputPdfPath = "embedded_font_output.pdf";
        const string trueTypeFontPath = @"C:\Windows\Fonts\arial.ttf";

        // Ensure the TrueType font file exists
        if (!File.Exists(trueTypeFontPath))
        {
            Console.Error.WriteLine($"TrueType font not found: {trueTypeFontPath}");
            return;
        }

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Load the TrueType font from file using a Stream (required overload)
            using (FileStream fontStream = File.OpenRead(trueTypeFontPath))
            {
                Font ttfFont = FontRepository.OpenFont(fontStream, FontTypes.TTF);
                // Mark the font to be embedded (full font, not subset)
                ttfFont.IsEmbedded = true;
                ttfFont.IsSubset = false;

                // Create a text fragment using the embedded font
                TextFragment fragment = new TextFragment("Sample text with embedded TrueType font.");
                fragment.TextState.Font = ttfFont;
                fragment.TextState.FontSize = 14;
                fragment.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

                // Add the fragment to the page
                page.Paragraphs.Add(fragment);
            }

            // Optional: embed standard Type1 fonts if they are used elsewhere
            doc.EmbedStandardFonts = true;

            // Save the PDF – fonts are now embedded in the output file
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF created with embedded TrueType font: {outputPdfPath}");
    }
}
