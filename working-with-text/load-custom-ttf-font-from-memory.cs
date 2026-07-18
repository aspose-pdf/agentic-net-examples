using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string customFontPath = "custom.ttf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(customFontPath))
        {
            Console.Error.WriteLine($"Custom font file not found: {customFontPath}");
            return;
        }

        // Load the TrueType font into a memory stream
        byte[] fontBytes = File.ReadAllBytes(customFontPath);
        using (MemoryStream fontStream = new MemoryStream(fontBytes))
        {
            // Open the font from the memory stream
            Font customFont = FontRepository.OpenFont(fontStream, FontTypes.TTF);
            customFont.IsEmbedded = true; // ensure the font is embedded in the PDF

            // Open the existing PDF document
            using (Document doc = new Document(inputPdfPath))
            {
                // Create a TextFragment and assign the custom font via its TextState
                TextFragment fragment = new TextFragment("Sample text rendered with a custom TrueType font");
                fragment.TextState.Font = customFont;
                fragment.TextState.FontSize = 14;
                fragment.TextState.ForegroundColor = Color.Black;

                // Add the fragment to the first page
                doc.Pages[1].Paragraphs.Add(fragment);

                // Save the modified document
                doc.Save(outputPdfPath);
            }
        }

        Console.WriteLine($"PDF saved with custom font to '{outputPdfPath}'.");
    }
}