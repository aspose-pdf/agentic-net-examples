using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string fontFilePath  = "custom.ttf";

        if (!File.Exists(inputPdfPath) || !File.Exists(fontFilePath))
        {
            Console.Error.WriteLine("Input PDF or font file not found.");
            return;
        }

        // Load the custom TrueType font from a memory stream
        Font customFont;
        using (FileStream fontStream = File.OpenRead(fontFilePath))
        {
            customFont = FontRepository.OpenFont(fontStream, FontTypes.TTF);
            // Ensure the font is embedded in the resulting PDF
            customFont.IsEmbedded = true;
        }

        // Open the existing PDF document
        using (Document doc = new Document(inputPdfPath))
        {
            // Create a text fragment and assign the custom font via TextState
            TextFragment textFragment = new TextFragment("Text rendered with custom font");
            textFragment.TextState.Font = customFont;
            textFragment.TextState.FontSize = 14; // optional size

            // Add the fragment to the first page
            Page page = doc.Pages[1];
            page.Paragraphs.Add(textFragment);

            // Save the modified document
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved to '{outputPdfPath}'.");
    }
}