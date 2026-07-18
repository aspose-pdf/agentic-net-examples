using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";          // source PDF
        const string outputPdfPath  = "output_subset.pdf"; // result PDF
        const string customFontPath = "MyFont.ttf";        // custom TrueType font file

        // Verify required files exist
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

        // Load the PDF document (using the standard load rule)
        using (Document doc = new Document(inputPdfPath))
        {
            // Load the custom font from file and mark it for embedding
            Font customFont = FontRepository.OpenFont(customFontPath);
            customFont.IsEmbedded = true; // ensure the font will be embedded

            // Apply the custom font to all existing text fragments on the first page.
            // This step guarantees that the font is actually used, so subsetting will keep only required glyphs.
            TextFragmentAbsorber absorber = new TextFragmentAbsorber();
            doc.Pages[1].Accept(absorber);
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                fragment.TextState.Font = customFont;
            }

            // Subset all fonts in the document, keeping only glyphs that are referenced.
            // FontSubsetStrategy.SubsetAllFonts handles both embedded and non‑embedded fonts.
            doc.FontUtilities.SubsetFonts(FontSubsetStrategy.SubsetAllFonts);

            // Save the modified PDF (using the standard save rule)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Subset PDF saved to '{outputPdfPath}'.");
    }
}
