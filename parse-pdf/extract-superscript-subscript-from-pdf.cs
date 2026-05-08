using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class SuperscriptSubscriptExtractor
{
    // Thresholds for detecting superscript/subscript
    private const double FontSizeRatioThreshold = 0.8;   // smaller than 80% of normal size
    private const double BaselineOffsetTolerance = 2.0; // points tolerance

    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputTxtPath = "extracted_with_annotations.txt";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use Document constructor)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            System.Text.StringBuilder resultBuilder = new System.Text.StringBuilder();

            // Process each page (Aspose.Pdf uses 1‑based indexing)
            for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
            {
                Page page = pdfDoc.Pages[pageIndex];

                // Use TextFragmentAbsorber with Raw formatting to get individual fragments
                TextExtractionOptions extractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Raw);
                TextFragmentAbsorber absorber = new TextFragmentAbsorber { ExtractionOptions = extractionOptions };
                page.Accept(absorber);

                var fragments = absorber.TextFragments;

                if (fragments == null || fragments.Count == 0)
                    continue;

                // Determine the reference (normal) font size and baseline Y
                // Assume the most common (largest) font size represents normal text
                double normalFontSize = fragments
                    .GroupBy(f => f.TextState.FontSize)
                    .OrderByDescending(g => g.Count())
                    .First()
                    .Key;

                // Average baseline Y for fragments with normal font size
                // Use Rectangle.LLY (lower‑left Y) as the baseline coordinate
                double normalBaselineY = fragments
                    .Where(f => Math.Abs(f.TextState.FontSize - normalFontSize) < 0.1)
                    .Average(f => f.Rectangle.LLY);

                // Build annotated line for the current page
                foreach (TextFragment fragment in fragments)
                {
                    string text = fragment.Text;

                    // Skip empty fragments
                    if (string.IsNullOrEmpty(text))
                        continue;

                    double sizeRatio = fragment.TextState.FontSize / normalFontSize;
                    double baselineDiff = fragment.Rectangle.LLY - normalBaselineY;

                    bool isSuperscript = sizeRatio < FontSizeRatioThreshold && baselineDiff > BaselineOffsetTolerance;
                    bool isSubscript   = sizeRatio < FontSizeRatioThreshold && baselineDiff < -BaselineOffsetTolerance;

                    if (isSuperscript)
                    {
                        // Annotate superscript with ^{...}
                        resultBuilder.Append($"^{{{text}}}");
                    }
                    else if (isSubscript)
                    {
                        // Annotate subscript with _{...}
                        resultBuilder.Append($"_{{{text}}}");
                    }
                    else
                    {
                        // Normal text
                        resultBuilder.Append(text);
                    }
                }

                // Add a line break after each page
                resultBuilder.AppendLine();
            }

            // Write the annotated text to an output file
            File.WriteAllText(outputTxtPath, resultBuilder.ToString());

            Console.WriteLine($"Extraction completed. Output written to '{outputTxtPath}'.");
        }
    }
}
