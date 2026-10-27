using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Aspose.Pdf;
using System.Drawing.Text;   // Used to enumerate installed system fonts

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
            // 1. Collect font names that are referenced by the PDF.
            //    Fonts are stored per‑page in the page resources (Page.Resources.Fonts).
            // -------------------------------------------------------------------
            HashSet<string> usedFontNamesSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            foreach (Page page in doc.Pages)
            {
                // Resources or the Fonts collection can be null for some pages.
                if (page.Resources?.Fonts != null)
                {
                    // Use dynamic to avoid compile‑time dependency on the FontInfo type.
                    foreach (dynamic fi in page.Resources.Fonts)
                    {
                        try
                        {
                            string fontName = fi.FontName as string;
                            if (!string.IsNullOrWhiteSpace(fontName))
                            {
                                usedFontNamesSet.Add(fontName);
                            }
                        }
                        catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
                        {
                            // If the object does not expose FontName, skip it.
                        }
                    }
                }
            }

            List<string> usedFontNames = usedFontNamesSet.ToList();

            // -------------------------------------------------------------------
            // 2. Enumerate fonts installed on the operating system.
            // -------------------------------------------------------------------
            InstalledFontCollection installedFonts = new InstalledFontCollection();
            HashSet<string> systemFontNames = installedFonts.Families
                .Select(f => f.Name)
                .Where(name => !string.IsNullOrWhiteSpace(name))
                .ToHashSet(StringComparer.OrdinalIgnoreCase);

            // -------------------------------------------------------------------
            // 3. Determine which PDF fonts are missing from the system.
            // -------------------------------------------------------------------
            List<string> missingFonts = usedFontNames
                .Where(pdfFont => !systemFontNames.Contains(pdfFont))
                .ToList();

            // -------------------------------------------------------------------
            // 4. Report results.
            // -------------------------------------------------------------------
            Console.WriteLine($"Total fonts referenced in PDF: {usedFontNames.Count}");
            Console.WriteLine($"System installed fonts: {systemFontNames.Count}");
            Console.WriteLine();

            if (missingFonts.Count == 0)
            {
                Console.WriteLine("All referenced fonts are available on the system.");
            }
            else
            {
                Console.WriteLine("Missing fonts:");
                foreach (string font in missingFonts)
                {
                    Console.WriteLine($" - {font}");
                }
            }
        }
    }
}
