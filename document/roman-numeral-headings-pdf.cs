using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class RomanHeadingPdf
{
    // Convert integer (1..3999) to Roman numeral
    static string ToRoman(int number)
    {
        if (number < 1 || number > 3999) throw new ArgumentOutOfRangeException(nameof(number));
        var map = new[] {
            new { Value = 1000, Numeral = "M" },
            new { Value = 900,  Numeral = "CM" },
            new { Value = 500,  Numeral = "D" },
            new { Value = 400,  Numeral = "CD" },
            new { Value = 100,  Numeral = "C" },
            new { Value = 90,   Numeral = "XC" },
            new { Value = 50,   Numeral = "L" },
            new { Value = 40,   Numeral = "XL" },
            new { Value = 10,   Numeral = "X" },
            new { Value = 9,    Numeral = "IX" },
            new { Value = 5,    Numeral = "V" },
            new { Value = 4,    Numeral = "IV" },
            new { Value = 1,    Numeral = "I" }
        };
        var result = "";
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

    // Parse Roman numeral (valid for numbers up to 3999)
    static int FromRoman(string roman)
    {
        if (string.IsNullOrWhiteSpace(roman)) throw new ArgumentException("Empty roman numeral", nameof(roman));
        var map = new Dictionary<char, int>
        {
            {'I',1},{'V',5},{'X',10},{'L',50},
            {'C',100},{'D',500},{'M',1000}
        };
        int total = 0;
        int prev = 0;
        foreach (char c in roman.ToUpperInvariant())
        {
            if (!map.TryGetValue(c, out int cur))
                throw new ArgumentException($"Invalid Roman numeral character '{c}'", nameof(roman));
            if (cur > prev)
            {
                total += cur - 2 * prev; // adjust for previous addition
            }
            else
            {
                total += cur;
            }
            prev = cur;
        }
        return total;
    }

    static void Main()
    {
        const string outputPath = "roman_headings.pdf";

        // Sample section titles
        List<string> titles = new List<string>
        {
            "Introduction",
            "Methods",
            "Results",
            "Discussion",
            "Conclusion"
        };

        // Create PDF with Roman numeral headings
        using (Document doc = new Document())
        {
            // Add a single page
            Page page = doc.Pages.Add();

            // Starting Y position (top of page)
            double yPos = page.PageInfo.Height - 50; // 50 points margin

            foreach (var (title, index) in titles.WithIndex())
            {
                int headingNumber = index + 1; // 1‑based
                string roman = ToRoman(headingNumber);
                string headingText = $"{roman}. {title}";

                // Create a text fragment for the heading
                TextFragment tf = new TextFragment(headingText);
                tf.TextState.FontSize = 16;
                tf.TextState.FontStyle = FontStyles.Bold;
                tf.Position = new Position(50, yPos); // left margin 50

                // Add to page
                page.Paragraphs.Add(tf);

                // Move Y position down for next heading
                yPos -= tf.TextState.FontSize + 10; // line spacing
            }

            // Save the PDF
            doc.Save(outputPath);
        }

        // Validate Roman numeral sequence by reading back the PDF
        using (Document doc = new Document(outputPath))
        {
            TextAbsorber absorber = new TextAbsorber();
            doc.Pages.Accept(absorber);
            string allText = absorber.Text;

            // Extract lines that start with a Roman numeral followed by a dot
            var headingNumbers = new List<int>();
            string[] lines = allText.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            Regex regex = new Regex(@"^(?<roman>[IVXLCDM]+)\.\s", RegexOptions.IgnoreCase);
            foreach (string line in lines)
            {
                Match m = regex.Match(line.Trim());
                if (m.Success)
                {
                    string roman = m.Groups["roman"].Value;
                    try
                    {
                        int value = FromRoman(roman);
                        headingNumbers.Add(value);
                    }
                    catch
                    {
                        // ignore invalid numerals
                    }
                }
            }

            // Verify sequential order (1,2,3,...)
            bool isSequential = true;
            for (int i = 0; i < headingNumbers.Count; i++)
            {
                if (headingNumbers[i] != i + 1)
                {
                    isSequential = false;
                    break;
                }
            }

            Console.WriteLine(isSequential
                ? "Roman numeral headings are in correct sequential order."
                : "Roman numeral headings sequence is incorrect.");
        }
    }
}

// Extension method to provide index while iterating
static class EnumerableExtensions
{
    public static IEnumerable<(T item, int index)> WithIndex<T>(this IEnumerable<T> source)
    {
        int i = 0;
        foreach (var item in source)
        {
            yield return (item, i++);
        }
    }
}