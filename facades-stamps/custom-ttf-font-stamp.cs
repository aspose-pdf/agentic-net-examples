using System;
using System.IO;
using Aspose.Pdf;                     // Document, Page, FontRepository, Color
using Aspose.Pdf.Text;                // TextStamp, TextState

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF
        const string outputPdf = "branded_output.pdf"; // result PDF
        const string fontPath  = "MyBrandFont.ttf";    // custom TrueType font file
        const string brandText = "My Brand";

        // Verify source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdf}");
            return;
        }

        // Verify custom font file exists
        if (!File.Exists(fontPath))
        {
            Console.Error.WriteLine($"Font file not found: {fontPath}");
            return;
        }

        // Load the custom TrueType font. The returned Font object can be used directly in TextState.
        Font customFont = FontRepository.OpenFont(fontPath);

        // Create a TextStamp that uses the custom font.
        TextStamp textStamp = new TextStamp(brandText)
        {
            // Position the stamp (coordinates are measured from the bottom‑left corner).
            XIndent = 100,   // horizontal offset
            YIndent = 500,   // vertical offset

            // Make the stamp appear behind page content.
            Background = true,

            // Semi‑transparent branding.
            Opacity = 0.6f
        };

        // Configure the visual appearance via TextState.
        textStamp.TextState.Font = customFont;          // custom TrueType font
        textStamp.TextState.FontSize = 48;              // font size
        textStamp.TextState.ForegroundColor = Color.Blue; // text colour

        // Load the PDF document.
        Document pdfDoc = new Document(inputPdf);

        // Apply the stamp to every page.
        foreach (Page page in pdfDoc.Pages)
        {
            page.AddStamp(textStamp);
        }

        // Save the stamped PDF.
        pdfDoc.Save(outputPdf);

        Console.WriteLine($"Stamped PDF saved to '{outputPdf}'.");
    }
}
