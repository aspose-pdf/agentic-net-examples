using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class RomanHeadingPdf
{
    // Helper to convert integer to uppercase Roman numerals (I, II, III, …)
    static string ToRoman(int number)
    {
        if (number < 1) return string.Empty;
        var map = new (int value, string numeral)[] {
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
        const string outputPath = "RomanHeadings.pdf";

        // ---------- Create PDF with Roman numeral headings ----------
        using (Document doc = new Document())
        {
            // NOTE: In evaluation mode Aspose.PDF allows a maximum of 4 elements
            // (pages, annotations, bookmarks, etc.) per collection. Therefore we
            // create only 4 pages here. A full license removes this limitation.
            for (int i = 1; i <= 4; i++) // changed from 5 to 4
            {
                // Add a new page
                Page page = doc.Pages.Add();

                // Build heading text
                string roman = ToRoman(i);
                string heading = $"Chapter {roman}";

                // Add heading as a TextFragment
                TextFragment tf = new TextFragment(heading);
                tf.TextState.Font = FontRepository.FindFont("Helvetica");
                tf.TextState.FontSize = 24;
                tf.TextState.FontStyle = FontStyles.Bold;
                tf.Position = new Position(50, 750); // top-left position
                page.Paragraphs.Add(tf);
            }

            // Save the document
            doc.Save(outputPath);
        }

        // ---------- Validate the Roman numeral headings on each page ----------
        using (Document loaded = new Document(outputPath))
        {
            bool allValid = true;
            for (int i = 1; i <= loaded.Pages.Count; i++)
            {
                // Extract the text of the page (the heading we added)
                TextAbsorber absorber = new TextAbsorber();
                loaded.Pages[i].Accept(absorber);
                string extracted = absorber.Text.Trim();

                string expected = $"Chapter {ToRoman(i)}";
                if (!extracted.Contains(expected))
                {
                    allValid = false;
                    Console.WriteLine($"Page {i} heading mismatch. Expected: {expected}, Found: {extracted}");
                }
                else
                {
                    Console.WriteLine($"Page {i} heading OK – {expected}");
                }
            }

            Console.WriteLine(allValid
                ? "All headings are correctly sequenced with uppercase Roman numerals."
                : "Some headings are incorrect.");
        }
    }
}
