using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document with deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Regular‑expression pattern for dates in MM/DD/YYYY format.
            // \d{2} matches two digits, '/' matches the slash, and \d{4} matches four digits.
            const string dateRegex = @"\d{2}/\d{2}/\d{4}";

            // Create a TextFragmentAbsorber that uses the regular‑expression pattern.
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(dateRegex);
            absorber.TextSearchOptions = new TextSearchOptions(true); // enable RegEx

            // Apply the absorber to all pages of the document.
            doc.Pages.Accept(absorber);

            // Retrieve the collection of matched fragments.
            TextFragmentCollection matches = absorber.TextFragments;

            Console.WriteLine($"Found {matches.Count} date(s) in the document:");

            // Iterate over each matched fragment and output the date string.
            foreach (TextFragment fragment in matches)
            {
                Console.WriteLine(fragment.Text);
            }
        }
    }
}
