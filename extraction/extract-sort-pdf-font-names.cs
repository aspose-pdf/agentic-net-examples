using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputTxtPath = "SortedFonts.txt";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Retrieve all fonts used in the document via FontUtilities
            Aspose.Pdf.Text.Font[] allFonts = doc.FontUtilities.GetAllFonts();

            // Extract the font names, filter out empty entries, remove duplicates, and sort alphabetically (case‑insensitive)
            List<string> fontNames = allFonts
                .Select(f => f.FontName)
                .Where(name => !string.IsNullOrEmpty(name))
                .Distinct()
                .OrderBy(name => name, StringComparer.OrdinalIgnoreCase)
                .ToList();

            // Write the sorted list to a text file
            File.WriteAllLines(outputTxtPath, fontNames);
        }

        Console.WriteLine($"Sorted font list saved to '{outputTxtPath}'.");
    }
}
