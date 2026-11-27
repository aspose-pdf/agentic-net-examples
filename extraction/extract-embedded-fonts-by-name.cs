using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "ExtractedFonts";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Retrieve all fonts via the FontUtilities API (Document.Fonts does not exist)
            Font[] allFonts = doc.FontUtilities.GetAllFonts();

            // Keep only embedded fonts – only they contain the actual font data that can be saved
            var embeddedFonts = allFonts.Where(f => f.IsEmbedded);

            // Group fonts by their FontName property
            var groups = embeddedFonts.GroupBy(f => f.FontName);

            foreach (var group in groups)
            {
                // Sanitize the font name to be a valid file name
                string safeFontName = string.Concat(group.Key.Split(Path.GetInvalidFileNameChars()));
                if (string.IsNullOrWhiteSpace(safeFontName))
                    safeFontName = "UnnamedFont";

                int counter = 1;
                foreach (Font font in group)
                {
                    // Default to .ttf – Aspose.Pdf does not expose the original file extension
                    string extension = ".ttf";

                    string outputPath = Path.Combine(
                        outputFolder,
                        $"{safeFontName}_{counter}{extension}");

                    try
                    {
                        // Save the embedded font to the file system
                        using (FileStream fs = File.Create(outputPath))
                        {
                            font.Save(fs);
                        }
                        Console.WriteLine($"Saved font '{font.FontName}' to '{outputPath}'.");
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine($"Failed to save font '{font.FontName}': {ex.Message}");
                    }

                    counter++;
                }
            }
        }
    }
}
