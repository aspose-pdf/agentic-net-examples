using System;
using System.IO;
using Aspose.Pdf;                 // Core PDF API
using Aspose.Pdf.Text;            // Font class resides here
using Aspose.Pdf.Facades;         // Required by task specification

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string fontsOutputDir = "EmbeddedFonts";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(fontsOutputDir);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Retrieve all fonts used in the document
            Aspose.Pdf.Text.Font[] allFonts = doc.FontUtilities.GetAllFonts();

            foreach (Aspose.Pdf.Text.Font font in allFonts)
            {
                // Process only embedded fonts
                if (font.IsEmbedded)
                {
                    // Determine a safe file name for the font
                    string rawName = !string.IsNullOrEmpty(font.FontName)
                                     ? font.FontName
                                     : (!string.IsNullOrEmpty(font.BaseFont) ? font.BaseFont : "font");
                    // Remove characters that are invalid in file names
                    foreach (char c in Path.GetInvalidFileNameChars())
                        rawName = rawName.Replace(c, '_');

                    string fontFilePath = Path.Combine(fontsOutputDir, $"{rawName}.ttf");

                    // Save the font to a TTF file (original format as provided by Aspose.Pdf)
                    using (FileStream fs = new FileStream(fontFilePath, FileMode.Create, FileAccess.Write))
                    {
                        font.Save(fs);
                    }

                    Console.WriteLine($"Embedded font saved: {fontFilePath}");
                }
            }
        }

        Console.WriteLine("Font extraction completed.");
    }
}