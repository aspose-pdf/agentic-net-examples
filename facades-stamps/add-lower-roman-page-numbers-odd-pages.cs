using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the document once – we will modify it in‑place.
        Document pdfDoc = new Document(inputPath);
        int pageCount = pdfDoc.Pages.Count;

        // Iterate over odd pages and add a lower‑roman page number stamp.
        for (int pageNumber = 1; pageNumber <= pageCount; pageNumber += 2)
        {
            // Convert the page index (1‑based) to lower‑roman.
            string roman = ToLowerRoman(pageNumber);

            // Create a TextStamp with the computed number.
            TextStamp stamp = new TextStamp(roman);
            stamp.TextState.FontSize = 12;
            stamp.HorizontalAlignment = HorizontalAlignment.Center;
            stamp.VerticalAlignment = VerticalAlignment.Bottom;
            stamp.BottomMargin = 10;

            // Apply the stamp to the current odd page.
            pdfDoc.Pages[pageNumber].AddStamp(stamp);
        }

        // Save the modified document.
        pdfDoc.Save(outputPath);
        Console.WriteLine($"Page numbers (lower‑roman) added to odd pages. Saved as '{outputPath}'.");
    }

    // Helper: convert an integer (>=1) to a lower‑case Roman numeral.
    private static string ToLowerRoman(int number)
    {
        if (number <= 0) throw new ArgumentOutOfRangeException(nameof(number));
        var map = new[]
        {
            new { Value = 1000, Numeral = "m" },
            new { Value = 900,  Numeral = "cm" },
            new { Value = 500,  Numeral = "d" },
            new { Value = 400,  Numeral = "cd" },
            new { Value = 100,  Numeral = "c" },
            new { Value = 90,   Numeral = "xc" },
            new { Value = 50,   Numeral = "l" },
            new { Value = 40,   Numeral = "xl" },
            new { Value = 10,   Numeral = "x" },
            new { Value = 9,    Numeral = "ix" },
            new { Value = 5,    Numeral = "v" },
            new { Value = 4,    Numeral = "iv" },
            new { Value = 1,    Numeral = "i" }
        };
        var result = string.Empty;
        foreach (var entry in map)
        {
            while (number >= entry.Value)
            {
                result += entry.Numeral;
                number -= entry.Value;
            }
        }
        return result;
    }
}
