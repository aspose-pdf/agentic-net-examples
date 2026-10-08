using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    // Convert an integer (1‑3999) to a Roman numeral string.
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
        const int introPageCount = 5; // number of introductory pages to number with Roman numerals

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the existing PDF inside a using block for deterministic disposal.
            using (Document doc = new Document(inputPath))
            {
                // Ensure we do not exceed the actual page count.
                int pagesToNumber = Math.Min(introPageCount, doc.Pages.Count);

                // Iterate using 1‑based page indexing (Aspose.Pdf requirement).
                for (int i = 1; i <= pagesToNumber; i++)
                {
                    Page page = doc.Pages[i];

                    // Create a TextFragment with the Roman numeral page number.
                    string roman = ToRoman(i);
                    TextFragment tf = new TextFragment(roman);

                    // Set visual appearance.
                    tf.TextState.FontSize = 12;
                    tf.TextState.ForegroundColor = Aspose.Pdf.Color.Black;
                    tf.TextState.Font = FontRepository.FindFont("Helvetica");

                    // Position the fragment at the bottom‑center of the page.
                    // Coordinates are in points; (0,0) is lower‑left.
                    double pageWidth  = page.PageInfo.Width;
                    double pageHeight = page.PageInfo.Height;
                    double marginBottom = 20; // distance from bottom edge
                    tf.Position = new Position(pageWidth / 2, marginBottom);

                    // Center alignment.
                    tf.TextState.HorizontalAlignment = HorizontalAlignment.Center;

                    // Add the fragment to the page's paragraph collection.
                    page.Paragraphs.Add(tf);
                }

                // Save the modified PDF. No SaveOptions needed for PDF output.
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