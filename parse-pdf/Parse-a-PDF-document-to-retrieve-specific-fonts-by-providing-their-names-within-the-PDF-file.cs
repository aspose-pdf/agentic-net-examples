using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";

        // Font names to look for (case‑insensitive)
        string[] targetFontNames = { "Arial", "Times New Roman", "Courier New" };

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Retrieve all fonts referenced in the document
            Font[] allFonts = doc.FontUtilities.GetAllFonts();

            // Build a lookup for quick case‑insensitive matching
            var nameSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (string name in targetFontNames)
                nameSet.Add(name);

            // Collect fonts that match the requested names
            List<Font> matchedFonts = new List<Font>();
            foreach (Font font in allFonts)
            {
                if (font != null && nameSet.Contains(font.FontName))
                    matchedFonts.Add(font);
            }

            // Output the results
            Console.WriteLine($"Total fonts in document: {allFonts.Length}");
            Console.WriteLine($"Requested fonts: {string.Join(", ", targetFontNames)}");
            Console.WriteLine();

            if (matchedFonts.Count == 0)
            {
                Console.WriteLine("No matching fonts were found in the PDF.");
            }
            else
            {
                Console.WriteLine("Matched fonts:");
                foreach (Font f in matchedFonts)
                {
                    Console.WriteLine($"- FontName: {f.FontName}");
                    Console.WriteLine($"  BaseFont: {f.BaseFont}");
                    Console.WriteLine($"  IsEmbedded: {f.IsEmbedded}");
                    Console.WriteLine($"  IsAccessible: {f.IsAccessible}");
                    Console.WriteLine();
                }
            }
        }
    }
}