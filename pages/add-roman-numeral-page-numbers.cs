using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    // Convert an integer to a Roman numeral (supports values up to 3999)
    static string ToRoman(int number)
    {
        if (number < 1 || number > 3999) throw new ArgumentOutOfRangeException(nameof(number));
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
        return result;
    }

    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_numbered.pdf";
        const int introPagesCount = 5; // number of introductory pages to number with Roman numerals

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
                // Ensure we do not exceed the actual page count
                int pagesToNumber = Math.Min(introPagesCount, doc.Pages.Count);

                // Iterate using 1‑based indexing (Aspose.Pdf rule)
                for (int i = 1; i <= pagesToNumber; i++)
                {
                    Page page = doc.Pages[i];

                    // Create a Roman numeral string for the current page
                    string roman = ToRoman(i);

                    // Prepare a text fragment with the Roman numeral
                    TextFragment tf = new TextFragment(roman)
                    {
                        // Position near the bottom‑center of the page
                        Position = new Position(page.PageInfo.Width / 2, 20),
                        // Center align the text
                        HorizontalAlignment = HorizontalAlignment.Center,
                        // Use a readable font size and color
                        TextState = { FontSize = 12, ForegroundColor = Aspose.Pdf.Color.Black }
                    };

                    // Add the fragment to the page's paragraphs collection
                    page.Paragraphs.Add(tf);
                }

                // Save the modified PDF (no special SaveOptions needed)
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