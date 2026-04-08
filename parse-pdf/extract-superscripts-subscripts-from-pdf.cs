using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputTxt = "extracted.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document (using block ensures proper disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Absorb all text fragments from the whole document
            TextFragmentAbsorber absorber = new TextFragmentAbsorber();

            // Use Flatten mode – it keeps fragments in visual order (useful for baseline analysis)
            absorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Flatten);

            // Apply absorber to all pages
            doc.Pages.Accept(absorber);

            // If no text was found, exit early
            if (absorber.TextFragments.Count == 0)
            {
                Console.WriteLine("No text found in the document.");
                return;
            }

            // Determine a reference font size and baseline.
            // We'll use the most common font size as the "normal" size.
            Dictionary<double, int> sizeHistogram = new Dictionary<double, int>();
            List<double> baselines = new List<double>();

            foreach (TextFragment fragment in absorber.TextFragments)
            {
                double size = fragment.TextState.FontSize;
                // Use the lower‑left Y coordinate of the fragment's rectangle as the baseline
                double baseline = fragment.Rectangle?.LLY ?? 0;

                // Populate histogram for font sizes
                double roundedSize = Math.Round(size, 1);
                if (sizeHistogram.ContainsKey(roundedSize))
                    sizeHistogram[roundedSize]++;
                else
                    sizeHistogram[roundedSize] = 1;

                baselines.Add(baseline);
            }

            // Find the most frequent font size – treat it as the normal size
            double normalFontSize = 0;
            int maxCount = 0;
            foreach (var kvp in sizeHistogram)
            {
                if (kvp.Value > maxCount)
                {
                    maxCount = kvp.Value;
                    normalFontSize = kvp.Key;
                }
            }

            // Compute average baseline for fragments that use the normal font size
            double baselineSum = 0;
            int baselineCount = 0;
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                if (Math.Abs(fragment.TextState.FontSize - normalFontSize) < 0.1)
                {
                    baselineSum += fragment.Rectangle?.LLY ?? 0;
                    baselineCount++;
                }
            }
            double averageBaseline = baselineCount > 0 ? baselineSum / baselineCount : 0;

            // Thresholds for detecting superscript/subscript
            const double sizeRatioThreshold = 0.8; // smaller than 80% of normal size
            const double baselineOffset = 2.0;     // points above/below average baseline

            // Prepare output
            using (StreamWriter writer = new StreamWriter(outputTxt))
            {
                writer.WriteLine($"Reference font size: {normalFontSize}");
                writer.WriteLine($"Average baseline (normal size): {averageBaseline}");
                writer.WriteLine();

                foreach (TextFragment fragment in absorber.TextFragments)
                {
                    double size = fragment.TextState.FontSize;
                    double baseline = fragment.Rectangle?.LLY ?? 0;

                    // Identify reduced-size fragments
                    if (size < normalFontSize * sizeRatioThreshold)
                    {
                        string type;
                        if (baseline > averageBaseline + baselineOffset)
                            type = "Superscript";
                        else if (baseline < averageBaseline - baselineOffset)
                            type = "Subscript";
                        else
                            type = "ReducedSize (undetermined)";

                        writer.WriteLine($"{type} on page {fragment.Page.Number}: \"{fragment.Text}\"");
                        writer.WriteLine($"  FontSize: {size}, BaselineY: {baseline}");
                    }
                }
            }

            Console.WriteLine($"Extraction completed. Results written to '{outputTxt}'.");
        }
    }
}
