using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    // Helper to convert an integer to uppercase Roman numeral (I, II, III, ...).
    static string ToRoman(int number)
    {
        if (number < 1) return string.Empty;
        var map = new[]
        {
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

    static void Main()
    {
        const string outputPdf = "RomanHeadings.pdf";

        // -----------------------------------------------------------------
        // Create a PDF with Roman numeral headings
        // -----------------------------------------------------------------
        using (Document doc = new Document())
        {
            // Add a single page (you can add more if needed)
            Page page = doc.Pages.Add();

            // Define common text properties
            var font = FontRepository.FindFont("Helvetica");
            const float fontSize = 24f;
            const float lineSpacing = 30f;
            float cursorY = (float)page.PageInfo.Height - 100f; // start near top

            // Create 5 headings as an example
            for (int i = 1; i <= 5; i++)
            {
                string roman = ToRoman(i);
                string headingText = $"{roman}. Heading {i}";

                TextFragment tf = new TextFragment(headingText);
                tf.TextState.Font = font;
                tf.TextState.FontSize = fontSize;
                tf.Position = new Position(50f, cursorY);
                page.Paragraphs.Add(tf);

                cursorY -= lineSpacing; // move down for next heading
            }

            // Save the created PDF
            doc.Save(outputPdf);
        }

        // -----------------------------------------------------------------
        // Load the PDF and validate the Roman numeral sequence
        // -----------------------------------------------------------------
        using (Document doc = new Document(outputPdf))
        {
            // Extract all text from the document
            TextAbsorber absorber = new TextAbsorber();
            doc.Pages.Accept(absorber);
            string allText = absorber.Text ?? string.Empty;

            // Split into lines (headings are on separate lines)
            string[] lines = allText.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            // Collect headings that match the pattern "Roman. ..."
            List<string> extractedHeadings = new List<string>();
            foreach (string line in lines)
            {
                // Trim leading/trailing spaces
                string trimmed = line.Trim();
                // Simple check: starts with a Roman numeral followed by a dot
                int dotIndex = trimmed.IndexOf('.');
                if (dotIndex > 0)
                {
                    string possibleRoman = trimmed.Substring(0, dotIndex);
                    // Validate that the possibleRoman consists only of I,V,X,L,C,D,M characters
                    if (System.Text.RegularExpressions.Regex.IsMatch(possibleRoman, @"^[IVXLCDM]+$", System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                    {
                        extractedHeadings.Add(trimmed);
                    }
                }
            }

            // Verify sequence
            bool sequenceIsValid = true;
            for (int i = 0; i < extractedHeadings.Count; i++)
            {
                string expectedRoman = ToRoman(i + 1);
                string expectedPrefix = expectedRoman + ".";
                if (!extractedHeadings[i].StartsWith(expectedPrefix, StringComparison.Ordinal))
                {
                    sequenceIsValid = false;
                    Console.WriteLine($"Mismatch at heading {i + 1}: expected \"{expectedPrefix}\" but found \"{extractedHeadings[i]}\"");
                    break;
                }
            }

            if (sequenceIsValid)
            {
                Console.WriteLine("Roman numeral heading sequence is correct.");
            }
            else
            {
                Console.WriteLine("Roman numeral heading sequence is incorrect.");
            }
        }
    }
}
