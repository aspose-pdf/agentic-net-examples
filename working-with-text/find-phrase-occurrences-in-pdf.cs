using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string phrase = "your specific phrase";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Store found fragments together with their page numbers
            var found = new List<(int PageNumber, TextFragment Fragment)>();

            // Iterate through each page and search for the phrase
            foreach (Page page in doc.Pages)
            {
                // Create a TextFragmentAbsorber that searches for the given phrase on the current page
                var absorber = new TextFragmentAbsorber(phrase);
                page.Accept(absorber);

                // Add each found fragment together with the current page number
                foreach (TextFragment fragment in absorber.TextFragments)
                {
                    found.Add((page.Number, fragment));
                }
            }

            // Output the total number of occurrences found
            Console.WriteLine($"Found {found.Count} occurrence(s) of \"{phrase}\".");

            // Iterate through each found fragment and display details
            int idx = 1;
            foreach (var entry in found)
            {
                // Use fully qualified Rectangle to avoid ambiguity
                Aspose.Pdf.Rectangle rect = entry.Fragment.Rectangle;
                Console.WriteLine($"[{idx}] Page {entry.PageNumber}, Text: \"{entry.Fragment.Text}\", Rect: ({rect.LLX}, {rect.LLY}, {rect.URX}, {rect.URY})");
                idx++;
            }
        }
    }
}
