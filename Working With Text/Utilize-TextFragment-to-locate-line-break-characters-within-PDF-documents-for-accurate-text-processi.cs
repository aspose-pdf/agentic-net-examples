using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "processed.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Collect extracted text with line breaks inserted
            List<string> pageTexts = new List<string>();

            // Iterate pages (1‑based indexing rule)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Absorb all text fragments on the current page
                TextFragmentAbsorber absorber = new TextFragmentAbsorber();
                page.Accept(absorber);

                // Sort fragments top‑to‑bottom, then left‑to‑right
                List<TextFragment> fragments = new List<TextFragment>(absorber.TextFragments);
                fragments.Sort((a, b) =>
                {
                    // Higher YIndent means higher on the page (PDF coordinate origin is bottom‑left)
                    int yCompare = b.Position.YIndent.CompareTo(a.Position.YIndent);
                    if (yCompare != 0) return yCompare;
                    return a.Position.XIndent.CompareTo(b.Position.XIndent);
                });

                // Build the page text, inserting a line break when the Y position changes
                string pageText = string.Empty;
                double? previousY = null;
                const double lineTolerance = 2.0; // tolerance for Y coordinate differences

                foreach (TextFragment fragment in fragments)
                {
                    double currentY = fragment.Position.YIndent;

                    if (previousY.HasValue && Math.Abs(previousY.Value - currentY) > lineTolerance)
                    {
                        // Y position changed enough to consider a new line
                        pageText += "\n";
                    }

                    pageText += fragment.Text;
                    previousY = currentY;
                }

                pageTexts.Add(pageText);
            }

            // Output the extracted text with line breaks
            for (int i = 0; i < pageTexts.Count; i++)
            {
                Console.WriteLine($"--- Page {i + 1} ---");
                Console.WriteLine(pageTexts[i]);
                Console.WriteLine();
            }

            // Save the (unchanged) document to demonstrate proper save usage
            doc.Save(outputPath); // PDF format, no SaveOptions needed
            Console.WriteLine($"Document saved to '{outputPath}'.");
        }
    }
}