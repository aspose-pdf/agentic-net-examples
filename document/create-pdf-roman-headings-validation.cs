using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    // Simple converter for numbers 1‑10 to Roman numerals (uppercase)
    static string ToRoman(int number)
    {
        if (number < 1 || number > 10) throw new ArgumentOutOfRangeException(nameof(number));
        string[] romans = { "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX", "X" };
        return romans[number - 1];
    }

    static void Main()
    {
        const string outputPath = "RomanHeadings.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Define a font to be used for headings
            Aspose.Pdf.Text.Font headingFont = FontRepository.FindFont("Helvetica-Bold");

            // NOTE: The evaluation version of Aspose.Pdf limits the number of pages that can be
            // created/viewed to 4. Using more pages throws an IndexOutOfRangeException with the
            // message "At most 4 elements (for any collection) can be viewed in evaluation mode".
            // Therefore we limit the document to 4 pages.
            int totalPages = 4;
            for (int i = 1; i <= totalPages; i++)
            {
                // Add a new page
                Page page = doc.Pages.Add();

                // Prepare heading text (e.g., "Chapter I")
                string headingText = $"Chapter {ToRoman(i)}";

                // Create a TextFragment for the heading
                TextFragment tf = new TextFragment(headingText);
                tf.TextState.Font = headingFont;
                tf.TextState.FontSize = 24;
                tf.TextState.ForegroundColor = Aspose.Pdf.Color.DarkBlue;

                // Position the heading near the top of the page
                tf.Position = new Position(50, page.PageInfo.Height - 50);
                page.Paragraphs.Add(tf);
            }

            // Save the document
            doc.Save(outputPath);
        }

        // Validation: extract headings and verify correct Roman numeral order
        using (Document doc = new Document(outputPath))
        {
            // Extract all text from the document
            TextAbsorber absorber = new TextAbsorber();
            doc.Pages.Accept(absorber);
            string fullText = absorber.Text;

            // Split lines and keep those that start with "Chapter"
            List<string> extractedHeadings = fullText
                .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Where(l => l.StartsWith("Chapter"))
                .ToList();

            // Build expected headings
            List<string> expectedHeadings = Enumerable.Range(1, extractedHeadings.Count)
                .Select(i => $"Chapter {ToRoman(i)}")
                .ToList();

            // Compare sequences
            bool isValid = extractedHeadings.SequenceEqual(expectedHeadings);

            Console.WriteLine(isValid
                ? "Validation succeeded: Roman numeral headings are in correct order."
                : "Validation failed: Heading sequence is incorrect.");

            // Optional: output the extracted headings for inspection
            Console.WriteLine("Extracted headings:");
            foreach (var h in extractedHeadings)
                Console.WriteLine($"  {h}");
        }
    }
}
