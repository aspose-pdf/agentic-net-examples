using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputDirectory = "ExtractedFonts";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDirectory);

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (var doc = new Document(inputPdfPath))
            {
                // Retrieve all fonts used in the document via FontUtilities
                Font[] fonts = doc.FontUtilities.GetAllFonts();

                foreach (var font in fonts)
                {
                    // Process only embedded fonts – they contain the actual font data
                    if (!font.IsEmbedded)
                        continue;

                    // Build a safe file name from the font name
                    string safeFontName = MakeSafeFileName(font.FontName);
                    string outputPath = Path.Combine(outputDirectory, safeFontName + ".ttf");

                    // Save the embedded font bytes to a .ttf file
                    using (var fs = File.Create(outputPath))
                    {
                        font.Save(fs);
                    }

                    Console.WriteLine($"Extracted font: {outputPath}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during extraction: {ex.Message}");
        }
    }

    // Helper method to replace invalid filename characters
    static string MakeSafeFileName(string name)
    {
        foreach (char invalidChar in Path.GetInvalidFileNameChars())
        {
            name = name.Replace(invalidChar, '_');
        }
        return string.IsNullOrWhiteSpace(name) ? "font" : name;
    }
}
