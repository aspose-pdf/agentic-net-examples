using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;
using Aspose.Pdf.Text;

class Program
{
    // Simple Roman numeral to integer converter (supports I to XXX)
    static int RomanToInt(string roman)
    {
        var map = new Dictionary<char, int>
        {
            {'I', 1}, {'V', 5}, {'X', 10}, {'L', 50}, {'C', 100}, {'D', 500}, {'M', 1000}
        };
        int total = 0;
        int prev = 0;
        foreach (char c in roman.ToUpper())
        {
            if (!map.ContainsKey(c)) throw new ArgumentException($"Invalid Roman numeral character: {c}");
            int cur = map[c];
            if (cur > prev)
                total += cur - 2 * prev; // adjust for previous addition
            else
                total += cur;
            prev = cur;
        }
        return total;
    }

    static void Main()
    {
        const string outputPath = "roman_headings.pdf";

        // Define heading texts with Roman numerals
        string[] headings = new[]
        {
            "I. Introduction",
            "II. Background",
            "III. Methodology",
            "IV. Results",
            "V. Discussion",
            "VI. Conclusion"
        };

        // ---------- Create PDF with tagged headings ----------
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Enable tagging and set basic metadata
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Roman Numeral Headings Example");

            // Root of the logical structure tree
            StructureElement root = tagged.RootElement;

            // Add visible text and corresponding structure elements
            foreach (string heading in headings)
            {
                // Visible heading text on the page
                TextFragment tf = new TextFragment(heading)
                {
                    // Simple styling for visibility
                    TextState = { FontSize = 14, FontStyle = FontStyles.Bold }
                };
                page.Paragraphs.Add(tf);

                // Logical heading element (H1 level)
                HeaderElement headerElem = tagged.CreateHeaderElement(1);
                headerElem.SetText(heading);
                root.AppendChild(headerElem);
            }

            // Save the PDF
            doc.Save(outputPath);
        }

        // ---------- Validate Roman numeral sequence ----------
        using (Document checkDoc = new Document(outputPath))
        {
            // Retrieve all header elements from the tagged structure
            StructureElement root = checkDoc.TaggedContent.RootElement;
            var headerElements = root.FindElements<HeaderElement>(true);

            List<int> numbers = new List<int>();
            foreach (HeaderElement hdr in headerElements)
            {
                // Expected format: "RomanNumeral. Text"
                string text = hdr.ActualText ?? string.Empty;
                int dotIndex = text.IndexOf('.');
                if (dotIndex <= 0)
                {
                    Console.WriteLine($"Invalid heading format: \"{text}\"");
                    continue;
                }

                string romanPart = text.Substring(0, dotIndex).Trim();
                try
                {
                    int value = RomanToInt(romanPart);
                    numbers.Add(value);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Failed to parse Roman numeral \"{romanPart}\": {ex.Message}");
                }
            }

            // Verify sequential order starting from 1
            bool isSequential = true;
            for (int i = 0; i < numbers.Count; i++)
            {
                if (numbers[i] != i + 1)
                {
                    isSequential = false;
                    Console.WriteLine($"Heading out of order: expected {i + 1}, found {numbers[i]}");
                }
            }

            if (isSequential && numbers.Count == headings.Length)
                Console.WriteLine("Roman numeral headings are in correct sequential order.");
            else
                Console.WriteLine("Roman numeral heading validation failed.");
        }
    }
}