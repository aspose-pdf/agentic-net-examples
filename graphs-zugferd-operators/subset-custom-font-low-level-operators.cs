using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Operators;

public class FontSubsetExample
{
    public static void Main()
    {
        // Paths to the source PDF, the custom TrueType font, and the output PDF
        const string inputPdfPath = "input.pdf";
        const string customFontPath = "MyCustomFont.ttf";
        const string outputPdfPath = "output_subset.pdf";

        // Verify that required files exist
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

        // Load the existing PDF document
        using (Document doc = new Document(inputPdfPath))
        {
            // Load the custom TrueType font from file
            Font customFont;
            using (FileStream fontStream = File.OpenRead(customFontPath))
            {
                customFont = FontRepository.OpenFont(fontStream, FontTypes.TTF);
            }

            // Mark the font to be embedded in the PDF
            customFont.IsEmbedded = true;

            // Choose a page to add text (using the first page here)
            Page page = doc.Pages[1];

            // Create a text fragment that uses the custom font
            TextFragment fragment = new TextFragment("Sample text with custom font");
            fragment.TextState.Font = customFont;
            fragment.TextState.FontSize = 14;
            fragment.Position = new Position(100, 700); // Position on the page

            // Add the fragment to the page's content
            page.Paragraphs.Add(fragment);

            // Demonstrate low‑level PDF operators: save and restore graphics state
            OperatorCollection ops = page.Contents;
            ops.Insert(0, new GSave());   // Equivalent to the "q" operator
            ops.Add(new GRestore());      // Equivalent to the "Q" operator

            // Subset all fonts in the document, keeping only the glyphs actually used
            doc.FontUtilities.SubsetFonts(FontSubsetStrategy.SubsetAllFonts);

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with subsetted custom font: {outputPdfPath}");
    }
}