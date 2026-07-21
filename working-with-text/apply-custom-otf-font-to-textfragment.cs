using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string fontPath = "custom.otf";          // Path to the OTF font file
        const string outputPath = "styled_text.pdf";   // Output PDF file

        // Verify that the font file exists
        if (!File.Exists(fontPath))
        {
            Console.Error.WriteLine($"Font file not found: {fontPath}");
            return;
        }

        // Load the custom OTF font
        Font customFont = FontRepository.OpenFont(fontPath);
        // Ensure the font is embedded in the PDF
        customFont.IsEmbedded = true;

        // Create a new PDF document (wrapped in using for proper disposal)
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Create a TextFragment with the desired text
            TextFragment tf = new TextFragment("Hello, Aspose PDF with OTF font!");

            // Apply the custom font and additional styling
            tf.TextState.Font = customFont;                     // Set the OTF font
            tf.TextState.FontSize = 24;                         // Set font size
            tf.TextState.ForegroundColor = Aspose.Pdf.Color.Blue; // Set text color

            // Add the TextFragment to the page's paragraph collection
            page.Paragraphs.Add(tf);

            // Save the document as PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}