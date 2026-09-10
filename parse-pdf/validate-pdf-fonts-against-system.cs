using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // Font, Font.IsAccessible, page.Resources.GetFonts

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        // Verify the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document (using statement ensures proper disposal)
            using (Document doc = new Document(inputPath))
            {
                // List to hold names of fonts that are not installed on the system
                List<string> missingFonts = new List<string>();

                // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    Page page = doc.Pages[i];

                    // Retrieve the font collection for the page; create it if absent
                    FontCollection fonts = page.Resources.GetFonts(true);

                    // Examine each font used on the page
                    foreach (Font font in fonts)
                    {
                        // Font.IsAccessible indicates whether the font is present on the system
                        if (!font.IsAccessible)
                        {
                            string name = font.FontName;
                            // Avoid duplicate entries
                            if (!missingFonts.Contains(name))
                                missingFonts.Add(name);
                        }
                    }
                }

                // Report the result
                if (missingFonts.Count == 0)
                {
                    Console.WriteLine("All fonts used in the document are installed on the system.");
                }
                else
                {
                    Console.WriteLine("Missing fonts (not installed on the system):");
                    foreach (string name in missingFonts)
                    {
                        Console.WriteLine($"- {name}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}