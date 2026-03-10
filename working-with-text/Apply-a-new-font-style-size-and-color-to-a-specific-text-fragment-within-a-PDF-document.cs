using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // FontRepository and Font are in this namespace

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string searchPhrase = "Hello World"; // text to locate
        const float newFontSize = 14f;               // desired size (float matches TextState.FontSize)
        Aspose.Pdf.Color newColor = Aspose.Pdf.Color.Red; // desired color
        const string fontName = "Arial";            // desired font

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Locate the font to be applied
            Font newFont = FontRepository.FindFont(fontName);

            // Search for the specific text on the first page
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(searchPhrase);
            doc.Pages[1].Accept(absorber);

            // Verify that at least one occurrence was found
            if (absorber.TextFragments.Count > 0)
            {
                // TextFragment collection is 1‑based; modify the first occurrence
                TextFragment fragment = absorber.TextFragments[1];

                // Apply new font, size, and color via the TextState property
                fragment.TextState.Font = newFont;
                fragment.TextState.FontSize = newFontSize; // now matches expected float type
                fragment.TextState.ForegroundColor = newColor;
            }
            else
            {
                Console.WriteLine("Specified text not found in the document.");
            }

            // Save the modified PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
