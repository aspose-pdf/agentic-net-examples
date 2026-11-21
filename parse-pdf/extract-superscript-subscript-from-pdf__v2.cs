using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class SuperscriptSubscriptExtractor
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // Iterate pages using 1‑based indexing (Aspose.Pdf rule).
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Extract text fragments with their layout information.
                TextFragmentAbsorber absorber = new TextFragmentAbsorber();
                absorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);
                page.Accept(absorber);

                // Collect font sizes and baseline Y positions of all fragments.
                List<double> fontSizes = new List<double>();
                List<double> yPositions = new List<double>();
                foreach (TextFragment fragment in absorber.TextFragments)
                {
                    fontSizes.Add(fragment.TextState.FontSize);
                    // Rectangle.LLY is the lower‑left Y coordinate (baseline) of the fragment.
                    yPositions.Add(fragment.Rectangle.LLY);
                }

                if (fontSizes.Count == 0)
                {
                    continue; // No text on this page.
                }

                // Determine median font size and median Y position for the page.
                double medianFontSize = GetMedian(fontSizes);
                double medianY = GetMedian(yPositions);

                // Build the output for the current page.
                StringBuilder pageOutput = new StringBuilder();

                foreach (TextFragment fragment in absorber.TextFragments)
                {
                    bool isSmaller = fragment.TextState.FontSize < medianFontSize * 0.9;
                    double baselineY = fragment.Rectangle.LLY;
                    bool isHigher = baselineY > medianY + (medianFontSize * 0.2);
                    bool isLower = baselineY < medianY - (medianFontSize * 0.2);

                    if (isSmaller && isHigher)
                    {
                        // Superscript – wrap with Unicode superscript markers U+207D … U+207E.
                        pageOutput.Append('\u207D'); // ⁽
                        pageOutput.Append(fragment.Text);
                        pageOutput.Append('\u207E'); // ⁾
                    }
                    else if (isSmaller && isLower)
                    {
                        // Subscript – wrap with Unicode subscript markers U+208D … U+208E.
                        pageOutput.Append('\u208D'); // ₍
                        pageOutput.Append(fragment.Text);
                        pageOutput.Append('\u208E'); // ₎
                    }
                    else
                    {
                        // Regular text.
                        pageOutput.Append(fragment.Text);
                    }
                }

                // Output the processed text for the page.
                Console.WriteLine($"--- Page {pageIndex} ---");
                Console.WriteLine(pageOutput.ToString());
            }
        }
    }

    // Helper to compute the median of a numeric list.
    private static double GetMedian(List<double> values)
    {
        var sorted = values.OrderBy(v => v).ToList();
        int count = sorted.Count;
        if (count % 2 == 1)
        {
            return sorted[count / 2];
        }
        else
        {
            return (sorted[(count / 2) - 1] + sorted[count / 2]) / 2.0;
        }
    }
}
