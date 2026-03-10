using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "embedded_fonts.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the source PDF
        Document sourceDoc = new Document(inputPdfPath);

        // Collect distinct embedded font names from the document
        var embeddedFontNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        foreach (Page page in sourceDoc.Pages)
        {
            // Absorb all text fragments on the current page
            var absorber = new TextFragmentAbsorber();
            page.Accept(absorber);

            foreach (TextFragment fragment in absorber.TextFragments)
            {
                var font = fragment.TextState.Font;
                if (font != null && font.IsEmbedded)
                {
                    embeddedFontNames.Add(font.FontName);
                }
            }
        }

        // Create a new PDF to list the embedded fonts
        Document outputDoc = new Document();
        Page outPage = outputDoc.Pages.Add();
        double yPos = outPage.PageInfo.Height - 50; // start near top margin

        foreach (string fontName in embeddedFontNames)
        {
            // Resolve the actual Font object (fallback to Helvetica if not found)
            Font font = FontRepository.FindFont(fontName) ?? FontRepository.FindFont("Helvetica");

            string sampleText = $"Font: {fontName} (Embedded) - The quick brown fox jumps over the lazy dog.";
            var textFragment = new TextFragment(sampleText)
            {
                TextState =
                {
                    Font = font,
                    FontSize = 12,
                    ForegroundColor = Color.Black
                },
                Position = new Position(50, yPos)
            };

            outPage.Paragraphs.Add(textFragment);
            yPos -= 30; // line spacing

            // Add a new page if we run out of space
            if (yPos < 50)
            {
                outPage = outputDoc.Pages.Add();
                yPos = outPage.PageInfo.Height - 50;
            }
        }

        // Save the resulting PDF containing the list of embedded fonts
        outputDoc.Save(outputPdfPath);

        Console.WriteLine($"Embedded fonts list saved to '{outputPdfPath}'.");
    }
}
