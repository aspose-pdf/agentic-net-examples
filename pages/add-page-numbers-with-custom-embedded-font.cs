using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input PDF and custom TrueType font file paths
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output_with_page_numbers.pdf";
        const string customFontPath = "custom_font.ttf";

        // Verify files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(customFontPath))
        {
            Console.Error.WriteLine($"Custom font not found: {customFontPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Load the external TrueType font and mark it for embedding
            Font customFont = FontRepository.FindFont(customFontPath);
            customFont.IsEmbedded = true;

            // Ensure standard Type1 fonts are also embedded when required
            doc.EmbedStandardFonts = true;

            // Create a PageNumberStamp with default format ("#")
            PageNumberStamp pageNumberStamp = new PageNumberStamp();

            // Assign the custom font to the stamp's TextState
            pageNumberStamp.TextState.Font = customFont;

            // Optional styling
            pageNumberStamp.TextState.FontSize = 12;               // Font size
            pageNumberStamp.TextState.ForegroundColor = Color.Black; // Text color
            pageNumberStamp.HorizontalAlignment = HorizontalAlignment.Center;
            pageNumberStamp.VerticalAlignment   = VerticalAlignment.Bottom;
            pageNumberStamp.BottomMargin = 20; // Distance from bottom edge

            // Add the stamp to every page in the document
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(pageNumberStamp);
            }

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Page numbers added with custom font. Output saved to '{outputPdfPath}'.");
    }
}