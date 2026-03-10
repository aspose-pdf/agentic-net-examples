using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // Font class resides here

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string fontsOutputDir = "ExtractedFonts";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(fontsOutputDir);

        // Load the PDF document (lifecycle rule: use Document constructor)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Keep track of fonts already extracted to avoid duplicates
            var extractedFonts = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            int fontCounter = 1;

            // Iterate over all pages (page indexing is 1‑based)
            for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
            {
                Page page = pdfDoc.Pages[pageNum];

                // Resources.Fonts gives the collection of Font objects used on this page
                foreach (Font font in page.Resources.Fonts)
                {
                    // Process only embedded fonts
                    if (font.IsEmbedded)
                    {
                        // Use the font's name as a base for the file name; fall back to a generated name
                        string baseName = !string.IsNullOrEmpty(font.FontName)
                            ? font.FontName
                            : $"EmbeddedFont_{fontCounter}";

                        // Sanitize the file name to remove invalid characters
                        string safeName = MakeSafeFileName(baseName);
                        string fontFilePath = Path.Combine(fontsOutputDir, $"{safeName}.ttf");

                        // Avoid writing the same font multiple times
                        if (extractedFonts.Contains(fontFilePath))
                            continue;

                        // Save the font data (preserves the original font format as TTF)
                        using (FileStream fs = new FileStream(fontFilePath, FileMode.Create, FileAccess.Write))
                        {
                            font.Save(fs);
                        }

                        Console.WriteLine($"Extracted embedded font: {font.FontName} -> {fontFilePath}");
                        extractedFonts.Add(fontFilePath);
                        fontCounter++;
                    }
                }
            }
        }
    }

    // Helper to replace characters that are illegal in file names
    static string MakeSafeFileName(string name)
    {
        foreach (char invalidChar in Path.GetInvalidFileNameChars())
        {
            name = name.Replace(invalidChar, '_');
        }
        return name;
    }
}