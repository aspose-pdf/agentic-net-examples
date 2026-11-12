using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    // Convert an integer to a lower‑case Roman numeral (supports values up to 3999)
    static string ToLowerRoman(int number)
    {
        if (number <= 0 || number > 3999) return number.ToString();

        var map = new (int value, string numeral)[]
        {
            (1000, "M"), (900, "CM"), (500, "D"), (400, "CD"),
            (100, "C"), (90, "XC"), (50, "L"), (40, "XL"),
            (10, "X"), (9, "IX"), (5, "V"), (4, "IV"), (1, "I")
        };

        var result = string.Empty;
        foreach (var (value, numeral) in map)
        {
            while (number >= value)
            {
                result += numeral;
                number -= value;
            }
        }
        return result.ToLowerInvariant();
    }

    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_numbered.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Iterate using 1‑based page indexing (Aspose.Pdf rule)
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    // Apply numbering only to odd pages
                    if (i % 2 == 1)
                    {
                        // Create a TextStamp with the lower‑roman page number
                        TextStamp stamp = new TextStamp(ToLowerRoman(i));

                        // Set visual appearance of the stamp
                        stamp.TextState.Font = FontRepository.FindFont("Helvetica");
                        stamp.TextState.FontSize = 12;
                        stamp.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

                        // Position the stamp at the bottom‑center of the page
                        stamp.HorizontalAlignment = HorizontalAlignment.Center;
                        stamp.VerticalAlignment   = VerticalAlignment.Bottom;
                        stamp.YIndent = 10; // 10 points from bottom edge

                        // Add the stamp to the current page
                        Page page = doc.Pages[i];
                        page.AddStamp(stamp);
                    }
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Page numbers added. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
