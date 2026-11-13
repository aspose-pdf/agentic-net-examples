using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing.Text; // Access system installed fonts
using Aspose.Pdf;
using Aspose.Pdf.Text; // For Font type

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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // -------------------------------------------------------------------
            // 1. Extract unique font names used in the PDF.
            //    Document does not expose a Fonts collection; fonts are stored per page
            //    in the page's Resources.Fonts collection.
            // -------------------------------------------------------------------
            var pdfFonts = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (Page page in doc.Pages)
            {
                // Resources may be null for some pages (e.g., blank pages)
                var fonts = page.Resources?.Fonts;
                if (fonts == null) continue;

                foreach (var fontObj in fonts)
                {
                    // The collection stores objects of type Aspose.Pdf.Text.Font.
                    // Use a safe cast; if the cast fails we fall back to dynamic access.
                    string name = null;
                    if (fontObj is Font font)
                    {
                        // Aspose.Pdf.Text.Font exposes FontName (the font's full name).
                        // FontFamily is not available, so we rely on FontName.
                        name = font.FontName;
                    }
                    else
                    {
                        // Defensive fallback – some older versions expose a dynamic object.
                        dynamic dyn = fontObj;
                        name = dyn.FontName as string;
                    }

                    if (!string.IsNullOrEmpty(name))
                        pdfFonts.Add(name);
                }
            }

            // -------------------------------------------------------------------
            // 2. Retrieve system‑installed font family names.
            // -------------------------------------------------------------------
            var installed = new InstalledFontCollection();
            var installedNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (var family in installed.Families)
            {
                installedNames.Add(family.Name);
            }

            // -------------------------------------------------------------------
            // 3. Identify fonts that are used in the PDF but not installed on the system.
            // -------------------------------------------------------------------
            var missingFonts = new List<string>();
            foreach (string fontName in pdfFonts)
            {
                if (!installedNames.Contains(fontName))
                    missingFonts.Add(fontName);
            }

            // -------------------------------------------------------------------
            // 4. Report the validation result.
            // -------------------------------------------------------------------
            if (missingFonts.Count == 0)
            {
                Console.WriteLine("All fonts used in the PDF are installed on the system.");
            }
            else
            {
                Console.WriteLine("Missing fonts:");
                foreach (string missing in missingFonts)
                {
                    Console.WriteLine($"- {missing}");
                }
            }
        }
    }
}
