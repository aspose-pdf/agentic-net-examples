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
            // Regular‑expression pattern for dates in MM/DD/YYYY format
            // Two digits, '/', two digits, '/', four digits
            const string dateRegex = @"(?<!\d)(\d{2}/\d{2}/\d{4})(?!\d)";

            // Use TextFragmentAbsorber (core API) to find all matching fragments
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(dateRegex);
            // Apply the absorber to all pages
            doc.Pages.Accept(absorber);

            TextFragmentCollection matches = absorber.TextFragments;
            Console.WriteLine($"Found {matches.Count} date(s) in the document.");

            // Output each found date with its page number
            foreach (TextFragment fragment in matches)
            {
                // fragment.Page returns the Page object; its Number property is the page index (1‑based)
                Console.WriteLine($"Page {fragment.Page.Number}: {fragment.Text}");
            }
        }
    }
}
