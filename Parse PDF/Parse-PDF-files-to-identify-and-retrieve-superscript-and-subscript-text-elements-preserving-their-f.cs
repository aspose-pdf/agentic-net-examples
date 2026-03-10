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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a TextFragmentAbsorber with Flatten mode to obtain positioning info
            TextFragmentAbsorber absorber = new TextFragmentAbsorber();
            absorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Flatten);

            // Apply the absorber to each page
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                page.Accept(absorber);
            }

            // Simple heuristic: compute average font size to compare against
            double avgFontSize = 0;
            int fragmentCount = 0;
            foreach (var tf in absorber.TextFragments)
            {
                avgFontSize += tf.TextState.FontSize;
                fragmentCount++;
            }
            avgFontSize = fragmentCount > 0 ? avgFontSize / fragmentCount : 0;

            // Iterate over all fragments and their segments to detect superscript/subscript
            foreach (var fragment in absorber.TextFragments)
            {
                // Use the fragment's rectangle to obtain its baseline Y coordinate
                double fragmentBaselineY = fragment.Rectangle.LLY;

                foreach (var segment in fragment.Segments)
                {
                    bool isSuperscript = false;
                    bool isSubscript = false;

                    // Consider a segment smaller than 80% of the average size as a candidate
                    if (segment.TextState.FontSize < avgFontSize * 0.8)
                    {
                        // Use the segment's rectangle for Y comparison
                        double segmentBaselineY = segment.Rectangle.LLY;

                        // Higher Y => superscript, lower Y => subscript (origin is bottom‑left)
                        if (segmentBaselineY > fragmentBaselineY + 1)
                            isSuperscript = true;
                        else if (segmentBaselineY < fragmentBaselineY - 1)
                            isSubscript = true;
                    }

                    if (isSuperscript || isSubscript)
                    {
                        string type = isSuperscript ? "Superscript" : "Subscript";

                        // Preserve formatting information
                        string fontName = segment.TextState.Font?.FontName ?? "Unknown";
                        double fontSize = segment.TextState.FontSize;
                        double posX = segment.Rectangle.LLX; // left‑most X of the segment
                        double posY = segment.Rectangle.LLY; // lower‑left Y of the segment

                        Console.WriteLine($"{type} (Page {fragment.Page.Number}): \"{segment.Text}\"");
                        Console.WriteLine($"  Font: {fontName}, Size: {fontSize}");
                        Console.WriteLine($"  Position: X={posX}, Y={posY}");
                    }
                }
            }
        }
    }
}
