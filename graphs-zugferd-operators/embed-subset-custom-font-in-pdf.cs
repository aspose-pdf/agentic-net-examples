using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output_subset.pdf";
        const string customFontPath = "MyFont.ttf";

        // Verify required files exist
        if (!File.Exists(inputPdf) || !File.Exists(customFontPath))
        {
            Console.Error.WriteLine("Input PDF or custom font file not found.");
            return;
        }

        // Load the source PDF (using the standard load rule)
        using (Document doc = new Document(inputPdf))
        {
            // Load the custom TrueType font from file using FontRepository
            Font customFont = FontRepository.OpenFont(customFontPath);
            // Mark the font to be embedded and to be treated as a subset
            customFont.IsEmbedded = true;
            customFont.IsSubset = true;

            // Apply the custom font to every text fragment in the document
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                // Extract all text fragments on the current page
                TextFragmentAbsorber absorber = new TextFragmentAbsorber();
                doc.Pages[pageIndex].Accept(absorber);

                // Replace the font of each fragment with the custom font
                foreach (TextFragment fragment in absorber.TextFragments)
                {
                    fragment.TextState.Font = customFont;
                }
            }

            // Use the low‑level font utility to subset all fonts in the document
            // This reduces file size while preserving only the glyphs actually used
            doc.FontUtilities.SubsetFonts(FontSubsetStrategy.SubsetAllFonts);

            // Save the modified PDF (using the standard save rule)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Subset PDF saved to '{outputPdf}'.");
    }
}
