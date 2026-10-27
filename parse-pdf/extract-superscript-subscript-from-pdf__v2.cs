using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Process each page separately
            foreach (Page page in doc.Pages)
            {
                // Use TextFragmentAbsorber to obtain text fragments for the current page
                TextFragmentAbsorber absorber = new TextFragmentAbsorber();
                absorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);
                page.Accept(absorber);

                var fragments = absorber.TextFragments;
                if (fragments == null || fragments.Count == 0)
                    continue;

                // Compute average font size and average baseline Y (LLY) for the page
                double avgFontSize = fragments.Sum(f => f.TextState.FontSize) / fragments.Count;
                double avgBaselineY = fragments.Sum(f => f.Rectangle.LLY) / fragments.Count;

                // Heuristic thresholds
                const double sizeThreshold = 0.8;   // smaller than 80% of average size
                const double yOffset = 2.0;        // vertical offset from average baseline (points)

                // Output detected superscript and subscript characters
                foreach (var frag in fragments)
                {
                    bool isSmall = frag.TextState.FontSize < avgFontSize * sizeThreshold;
                    double baselineY = frag.Rectangle.LLY;
                    bool isSup = isSmall && baselineY > avgBaselineY + yOffset;
                    bool isSub = isSmall && baselineY < avgBaselineY - yOffset;

                    foreach (char ch in frag.Text)
                    {
                        if (isSup)
                        {
                            // U+207A = SUPERSCRIPT PLUS used as a generic superscript marker
                            Console.Write($"\u207A{ch}");
                        }
                        else if (isSub)
                        {
                            // U+208A = SUBSCRIPT PLUS used as a generic subscript marker
                            Console.Write($"\u208A{ch}");
                        }
                        else
                        {
                            Console.Write(ch);
                        }
                    }
                }

                Console.WriteLine(); // separate pages
            }
        }
    }
}
