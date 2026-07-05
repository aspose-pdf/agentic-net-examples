using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Ensure system fonts are loaded (optional but speeds up IsAccessible checks)
        FontRepository.LoadFonts();

        // Collect names of fonts that are not installed on the system
        var missingFonts = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Absorb all text fragments from the whole document
            TextFragmentAbsorber absorber = new TextFragmentAbsorber();
            doc.Pages.Accept(absorber);

            // Iterate over each text fragment and examine its font
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                Font font = fragment.TextState.Font;
                if (font != null && !font.IsAccessible)
                {
                    // Font.IsAccessible indicates the font is not installed
                    // Use Font.FontName if available; fallback to ToString()
                    string fontName = font.FontName ?? font.ToString();
                    missingFonts.Add(fontName);
                }
            }
        }

        // Report the results
        if (missingFonts.Count == 0)
        {
            Console.WriteLine("All fonts used in the document are installed on this system.");
        }
        else
        {
            Console.WriteLine("The following fonts are referenced in the PDF but are NOT installed on this system:");
            foreach (string name in missingFonts)
            {
                Console.WriteLine($"- {name}");
            }
        }
    }
}