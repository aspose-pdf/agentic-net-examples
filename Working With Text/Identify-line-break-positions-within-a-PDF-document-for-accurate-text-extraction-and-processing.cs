using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    // Tolerance (in points) to decide when Y coordinates belong to different lines
    const double LineTolerance = 2.0;

    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block (ensures deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Absorb all text fragments on the current page
                TextFragmentAbsorber absorber = new TextFragmentAbsorber();
                // Use Flatten mode to get fragments ordered by their visual position
                absorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Flatten);
                page.Accept(absorber);

                // No fragments -> continue to next page
                if (absorber.TextFragments == null || absorber.TextFragments.Count == 0)
                    continue;

                // Sort fragments by Y (descending = top to bottom) then X (ascending = left to right)
                List<TextFragment> sorted = absorber.TextFragments
                    .OrderByDescending(f => f.Position.YIndent)   // higher Y = higher on page
                    .ThenBy(f => f.Position.XIndent)
                    .ToList();

                double? currentLineY = null;
                int lineNumber = 0;

                Console.WriteLine($"Page {pageIndex}:");

                foreach (TextFragment fragment in sorted)
                {
                    double fragmentY = fragment.Position.YIndent;

                    // If this is the first fragment or the Y difference exceeds the tolerance,
                    // we have encountered a new line.
                    if (!currentLineY.HasValue || Math.Abs(currentLineY.Value - fragmentY) > LineTolerance)
                    {
                        lineNumber++;
                        currentLineY = fragmentY;

                        // Output line break information (page, line number, Y coordinate)
                        Console.WriteLine($"  Line {lineNumber} starts at Y = {fragmentY:F2}");
                    }

                    // Optionally, output the fragment text (commented out to keep output concise)
                    // Console.WriteLine($"    [{fragment.Position.XIndent:F2}, {fragmentY:F2}] \"{fragment.Text}\"");
                }
            }
        }
    }
}