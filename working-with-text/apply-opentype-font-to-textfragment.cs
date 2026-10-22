using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string fontPath = "MyFont.otf";
        const string fontAlias = "MyCustomFont"; // an arbitrary name for the font
        const string outputPath = "styled_text.pdf";

        // Verify that the OTF font file exists
        if (!File.Exists(fontPath))
        {
            Console.Error.WriteLine($"Font file not found: {fontPath}");
            return;
        }

        // Register the custom OpenType font with Aspose.Pdf using the modern API
        FontRepository.Substitutions.Add(new SimpleFontSubstitution(fontAlias, fontPath));
        Font customFont = FontRepository.FindFont(fontAlias);

        // Create a new PDF document inside a using block for proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Create a TextFragment containing the desired text
            TextFragment fragment = new TextFragment("Hello, Aspose.Pdf with custom OTF font!");

            // Apply the custom font and styling to the TextFragment
            fragment.TextState.Font = customFont;               // use the OTF font
            fragment.TextState.FontSize = 24;                  // set font size
            fragment.TextState.ForegroundColor = Aspose.Pdf.Color.Black; // set text color

            // Add the styled TextFragment to the page
            page.Paragraphs.Add(fragment);

            // Save the PDF document to the specified path
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
