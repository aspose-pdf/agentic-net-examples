using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFolder = "ExtractedFonts";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // FontAbsorber collects all font resources used in the document
            FontAbsorber fontAbsorber = new FontAbsorber();
            fontAbsorber.Visit(doc);

            int index = 1;
            foreach (Aspose.Pdf.Text.Font font in fontAbsorber.Fonts)
            {
                // Build a safe file name based on the font's name (or fallback to an index)
                string safeName = MakeSafeFileName(font.FontName ?? $"font_{index}");
                string extension = GetFontExtension(font);
                string outPath = Path.Combine(outputFolder, $"{safeName}{extension}");

                // Save the font to a file, preserving its original format (TTF is the default)
                using (FileStream fs = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                {
                    font.Save(fs);
                }

                Console.WriteLine($"Saved font '{font.FontName}' to '{outPath}' (Embedded={font.IsEmbedded}, Subset={font.IsSubset})");
                index++;
            }
        }
    }

    // Replace any characters that are invalid in file names
    static string MakeSafeFileName(string name)
    {
        foreach (char c in Path.GetInvalidFileNameChars())
            name = name.Replace(c, '_');
        return name;
    }

    // Determine a suitable file extension for the extracted font.
    // Aspose.Pdf typically saves fonts in TTF format via Font.Save().
    static string GetFontExtension(Aspose.Pdf.Text.Font font)
    {
        // If needed, inspect font.FontOptions.FontType to choose .ttf, .otf, etc.
        // For simplicity, default to .ttf as most extracted fonts are saved this way.
        return ".ttf";
    }
}