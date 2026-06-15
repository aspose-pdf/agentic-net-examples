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

        // Pre‑load system fonts to avoid repeated searches
        FontRepository.LoadFonts();

        var missingFonts = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        // Load the PDF document (using rule: document-disposal-with-using)
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based (rule: page-indexing-one-based)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Get the fonts declared in the page resources
                FontCollection pageFonts = page.Resources.Fonts;

                foreach (Aspose.Pdf.Text.Font font in pageFonts)
                {
                    // Font.IsAccessible indicates whether the font is installed on the system
                    if (!font.IsAccessible)
                    {
                        // Use FontName if available; fallback to generic identifier
                        string name = !string.IsNullOrEmpty(font.FontName) ? font.FontName : "UnnamedFont";
                        missingFonts.Add(name);
                    }
                }
            }
        }

        // Report results
        if (missingFonts.Count == 0)
        {
            Console.WriteLine("All fonts used in the document are installed on the system.");
        }
        else
        {
            Console.WriteLine("Missing fonts:");
            foreach (string name in missingFonts)
            {
                Console.WriteLine($"- {name}");
            }
        }
    }
}