using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Text; // Font class resides here

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "ExtractedFonts";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the PDF document (lifecycle rule: use using for disposal)
            using (Document doc = new Document(inputPath))
            {
                // Keep track of fonts already processed to avoid duplicates
                var processed = new HashSet<string>();

                // NOTE: The Document.Fonts collection is not available in older Aspose.Pdf versions.
                // All fonts can be obtained from each page's resource dictionary, so we only iterate pages.
                // If you are using a newer version that exposes doc.Fonts, you may uncomment the block below.
                //
                // foreach (Aspose.Pdf.Text.Font font in doc.Fonts)
                // {
                //     SaveFontIfNew(font, outputDir, processed);
                // }

                // Extract fonts that are defined in each page's resources
                foreach (Page page in doc.Pages) // page indexing is 1‑based
                {
                    foreach (Aspose.Pdf.Text.Font font in page.Resources.Fonts)
                    {
                        SaveFontIfNew(font, outputDir, processed);
                    }
                }

                Console.WriteLine($"All embedded fonts have been saved to '{outputDir}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Saves the font to a .ttf file and writes its metadata to a .txt side‑car file
    static void SaveFontIfNew(Aspose.Pdf.Text.Font font, string outputDir, HashSet<string> processed)
    {
        // Use BaseFont as a unique identifier; fall back to FontName or a GUID if needed
        string identifier = !string.IsNullOrEmpty(font.BaseFont) ? font.BaseFont :
                            (!string.IsNullOrEmpty(font.FontName) ? font.FontName : Guid.NewGuid().ToString());

        // Skip fonts that have already been saved
        if (processed.Contains(identifier))
            return;

        processed.Add(identifier);

        // Create safe file names
        string safeName = MakeSafeFileName(identifier);
        string fontPath = Path.Combine(outputDir, safeName + ".ttf");
        string metaPath = Path.Combine(outputDir, safeName + ".txt");

        // Write metadata (preserving characteristics)
        File.WriteAllText(metaPath,
            $"BaseFont: {font.BaseFont}{Environment.NewLine}" +
            $"FontName: {font.FontName}{Environment.NewLine}" +
            $"IsEmbedded: {font.IsEmbedded}{Environment.NewLine}" +
            $"IsSubset: {font.IsSubset}{Environment.NewLine}" +
            $"IsAccessible: {font.IsAccessible}{Environment.NewLine}");

        // Save the actual font binary (TTF format) to disk.
        // The Font.Save overload in the used Aspose.Pdf version expects a Stream, not a file path.
        using (FileStream fs = new FileStream(fontPath, FileMode.Create, FileAccess.Write))
        {
            font.Save(fs);
        }
    }

    // Helper to replace invalid filename characters
    static string MakeSafeFileName(string name)
    {
        foreach (char c in Path.GetInvalidFileNameChars())
            name = name.Replace(c, '_');
        return name;
    }
}
