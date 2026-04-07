using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths for the OTF font file and the output PDF
        const string fontPath = "C:\\Fonts\\customfont.otf";
        const string outputPdf = "styled_text.pdf";

        // Ensure the font file exists
        if (!File.Exists(fontPath))
        {
            Console.Error.WriteLine($"Font file not found: {fontPath}");
            return;
        }

        // Open the custom OTF font
        Font customFont = FontRepository.OpenFont(fontPath);
        // Optionally embed the font in the PDF
        customFont.IsEmbedded = true;

        // Create a new PDF document (empty document with a default page)
        using (Document doc = new Document())
        {
            // Create a TextFragment with the desired text
            TextFragment fragment = new TextFragment("Hello, Aspose.Pdf with OTF font!");

            // Apply the custom font to the TextFragment
            fragment.TextState.Font = customFont;
            // Optionally set additional styling
            fragment.TextState.FontSize = 24;
            fragment.TextState.ForegroundColor = Color.Blue;

            // Add the TextFragment to the first page
            doc.Pages[1].Paragraphs.Add(fragment);

            // Save the PDF (Document.Save without SaveOptions always writes PDF)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF created with custom OTF font: {outputPdf}");
    }
}