using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class DuplicateFontFinder
{
    static void Main()
    {
        // Path to the source PDF file
        const string inputPdfPath = "input.pdf";

        // Path for the output text file containing duplicate font names
        const string outputTxtPath = "DuplicateFonts.txt";

        // Verify that the input PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Collect all font names used in the document via FontUtilities
            List<string> fontNames = new List<string>();
            Aspose.Pdf.Text.Font[] fonts = doc.FontUtilities.GetAllFonts();
            foreach (Aspose.Pdf.Text.Font font in fonts)
            {
                if (!string.IsNullOrEmpty(font.FontName))
                {
                    fontNames.Add(font.FontName);
                }
            }

            // Count occurrences of each font name (case‑insensitive)
            Dictionary<string, int> nameCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            foreach (string name in fontNames)
            {
                if (nameCounts.ContainsKey(name))
                    nameCounts[name]++;
                else
                    nameCounts[name] = 1;
            }

            // Extract names that appear more than once (duplicates)
            List<string> duplicateNames = new List<string>();
            foreach (KeyValuePair<string, int> kvp in nameCounts)
            {
                if (kvp.Value > 1)
                    duplicateNames.Add(kvp.Key);
            }

            // Write duplicate font names to the output text file
            using (StreamWriter writer = new StreamWriter(outputTxtPath, false))
            {
                foreach (string dup in duplicateNames)
                {
                    writer.WriteLine(dup);
                }
            }

            Console.WriteLine($"Duplicate font names written to '{outputTxtPath}'.");
        }
    }
}
