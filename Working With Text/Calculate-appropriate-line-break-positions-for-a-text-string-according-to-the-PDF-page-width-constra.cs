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
        // Input text to be wrapped
        const string inputText = "The quick brown fox jumps over the lazy dog. This sentence is deliberately long to demonstrate word wrapping based on page width constraints.";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Define the rectangle where the paragraph will be placed.
            // Left = 50, Bottom = 500, Right = 550, Top = 750  (width = 500 points)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(50, 500, 550, 750);

            // Create a TextParagraph and set its rectangle
            TextParagraph paragraph = new TextParagraph
            {
                Rectangle = rect
            };

            // Configure word‑wrap mode to wrap by whole words
            paragraph.FormattingOptions.WrapMode = TextFormattingOptions.WordWrapMode.ByWords;

            // Append the long text as a single line (the builder will split it into lines)
            paragraph.AppendLine(inputText);

            // Use TextBuilder to layout the paragraph on the page
            TextBuilder builder = new TextBuilder(page);
            builder.AppendParagraph(paragraph);

            // -----------------------------------------------------------------
            // Retrieve the laid‑out lines.
            // TextBuilder does not expose a GetLines method in recent versions of
            // Aspose.Pdf, therefore we use a TextFragmentAbsorber to collect the
            // fragments after the layout has been performed.
            // -----------------------------------------------------------------
            TextFragmentAbsorber absorber = new TextFragmentAbsorber();
            page.Accept(absorber);

            // Sort fragments top‑to‑bottom (higher Y first) and left‑to‑right.
            var sortedFragments = absorber.TextFragments
                                          .Cast<TextFragment>()
                                          .OrderByDescending(f => f.Rectangle?.URY ?? 0)
                                          .ThenBy(f => f.Rectangle?.LLX ?? 0)
                                          .ToList();

            // Group fragments that belong to the same visual line. A tolerance of 1.0
            // point is sufficient for most fonts.
            const double yTolerance = 1.0;
            var lines = new List<List<TextFragment>>();
            foreach (var fragment in sortedFragments)
            {
                if (lines.Count == 0)
                {
                    lines.Add(new List<TextFragment> { fragment });
                    continue;
                }

                var lastLine = lines[lines.Count - 1];
                var referenceFragment = lastLine[0];
                double refY = referenceFragment.Rectangle?.URY ?? 0;
                double curY = fragment.Rectangle?.URY ?? 0;

                if (Math.Abs(refY - curY) <= yTolerance)
                {
                    // Same visual line
                    lastLine.Add(fragment);
                }
                else
                {
                    // New line
                    lines.Add(new List<TextFragment> { fragment });
                }
            }

            Console.WriteLine("Calculated line break positions:");
            int lineNumber = 1;
            foreach (var lineFragments in lines)
            {
                string lineText = string.Empty;
                double lineStartX = double.MaxValue;
                double lineEndX = double.MinValue;

                foreach (var fragment in lineFragments)
                {
                    lineText += fragment.Text;
                    if (fragment.Rectangle != null)
                    {
                        if (fragment.Rectangle.LLX < lineStartX)
                            lineStartX = fragment.Rectangle.LLX;
                        if (fragment.Rectangle.URX > lineEndX)
                            lineEndX = fragment.Rectangle.URX;
                    }
                }

                // Fallback to the defined rectangle bounds if no fragment data was found
                if (lineStartX == double.MaxValue) lineStartX = rect.LLX;
                if (lineEndX == double.MinValue) lineEndX = rect.URX;

                Console.WriteLine($"Line {lineNumber}: \"{lineText}\"");
                Console.WriteLine($"    Start X = {lineStartX}, End X = {lineEndX}");
                lineNumber++;
            }

            // (Optional) Save the document to verify the layout visually
            string outPath = Path.Combine(Environment.CurrentDirectory, "WrappedText.pdf");
            doc.Save(outPath);
            Console.WriteLine($"PDF saved to: {outPath}");
        }
    }
}
