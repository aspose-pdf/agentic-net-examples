using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Path to the OpenType (OTF) font file
        const string otfPath = "customfont.otf";
        // Output PDF file
        const string outputPath = "styled_text.pdf";

        if (!File.Exists(otfPath))
        {
            Console.Error.WriteLine($"Font file not found: {otfPath}");
            return;
        }

        // Load the custom OTF font
        Font customFont = FontRepository.OpenFont(otfPath);
        // Ensure the font is embedded in the PDF
        customFont.IsEmbedded = true;

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a text fragment with the desired content
            TextFragment textFragment = new TextFragment("Hello, Aspose PDF with custom OTF font!");

            // Apply the custom font and styling to the text fragment
            textFragment.TextState.Font = customFont;
            textFragment.TextState.FontSize = 24;
            textFragment.TextState.ForegroundColor = Color.Blue;

            // Add the text fragment to the page
            page.Paragraphs.Add(textFragment);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}