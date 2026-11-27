using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "UniqueFonts.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPath))
            {
                // Retrieve all fonts used in the PDF via FontUtilities
                Font[] fonts = doc.FontUtilities.GetAllFonts();

                // Collect unique font names (case‑insensitive)
                HashSet<string> uniqueFonts = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                foreach (var font in fonts)
                {
                    if (!string.IsNullOrEmpty(font.FontName))
                        uniqueFonts.Add(font.FontName);
                }

                // Write each unique font name to a plain‑text file, one per line
                File.WriteAllLines(outputPath, uniqueFonts);
            }

            Console.WriteLine($"Unique font list saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
