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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Collect names of fonts that are not installed on the system
            List<string> missingFonts = new List<string>();

            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Each page has a Fonts collection in its Resources
                foreach (Aspose.Pdf.Text.Font font in page.Resources.Fonts)
                {
                    // Font.IsAccessible indicates whether the font is present on the system
                    if (!font.IsAccessible && !missingFonts.Contains(font.FontName))
                    {
                        missingFonts.Add(font.FontName);
                    }
                }
            }

            // Report the results
            if (missingFonts.Count == 0)
            {
                Console.WriteLine("All fonts used in the document are installed on the system.");
            }
            else
            {
                Console.WriteLine("The following fonts are referenced in the PDF but are NOT installed on the system:");
                foreach (string fontName in missingFonts)
                {
                    Console.WriteLine($"- {fontName}");
                }
            }
        }
    }
}