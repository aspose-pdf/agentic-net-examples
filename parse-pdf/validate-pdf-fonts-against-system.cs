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

        // List to hold names of fonts that are not installed on the system
        List<string> missingFonts = new List<string>();

        // Open the PDF document
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPath))
        {
            // Iterate over all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Aspose.Pdf.Page page = doc.Pages[i];

                // Get the font collection from the page resources
                Aspose.Pdf.Text.FontCollection fonts = page.Resources.Fonts;

                // Examine each font used on the page
                foreach (Aspose.Pdf.Text.Font font in fonts)
                {
                    // If the font is not accessible (not installed), record its name
                    if (!font.IsAccessible)
                    {
                        string fontName = font.FontName ?? "(Unnamed Font)";
                        if (!missingFonts.Contains(fontName))
                        {
                            missingFonts.Add(fontName);
                        }
                    }
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
            Console.WriteLine("The following fonts are referenced in the PDF but are NOT installed on this system:");
            foreach (string name in missingFonts)
            {
                Console.WriteLine($"- {name}");
            }
        }
    }
}