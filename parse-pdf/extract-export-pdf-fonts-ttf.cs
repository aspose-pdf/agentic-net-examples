using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";          // PDF to process
        const string outputDirectory = "ExportedFonts";   // Target folder for .ttf files

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Ensure the output folder exists
        Directory.CreateDirectory(outputDirectory);

        try
        {
            // Load the PDF document (lifecycle rule: wrap in using)
            using (Document doc = new Document(inputPdfPath))
            {
                // Keep track of fonts already exported to avoid duplicates
                HashSet<string> exportedFonts = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

                // Pages are 1‑based (rule: page-indexing-one-based)
                for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
                {
                    Page page = doc.Pages[pageIndex];

                    // Iterate over all fonts referenced on the page
                    foreach (Aspose.Pdf.Text.Font font in page.Resources.Fonts)
                    {
                        // Use a readable identifier for the font file name
                        string fontIdentifier = !string.IsNullOrEmpty(font.FontName)
                            ? font.FontName
                            : font.BaseFont ?? "UnnamedFont";

                        // Skip if this font has already been saved
                        if (exportedFonts.Contains(fontIdentifier))
                            continue;

                        exportedFonts.Add(fontIdentifier);

                        // Build a safe file name (replace invalid characters)
                        string safeFileName = MakeSafeFileName(fontIdentifier) + ".ttf";
                        string outputPath = Path.Combine(outputDirectory, safeFileName);

                        // Export the font to a TTF file
                        using (FileStream fs = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                        {
                            font.Save(fs);   // Font.Save writes the font in TTF format
                        }

                        Console.WriteLine($"Exported font '{fontIdentifier}' to '{outputPath}'.");
                    }
                }
            }

            Console.WriteLine("Font extraction completed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Replaces characters that are invalid in file names with an underscore
    private static string MakeSafeFileName(string name)
    {
        foreach (char c in Path.GetInvalidFileNameChars())
        {
            name = name.Replace(c, '_');
        }
        return name;
    }
}