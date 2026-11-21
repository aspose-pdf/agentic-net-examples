using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
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

        // Load PDF
        using (Document doc = new Document(inputPdf))
        {
            // Collect all text fragments from all pages
            List<TextFragment> fragments = new List<TextFragment>();
            foreach (Page page in doc.Pages)
            {
                TextFragmentAbsorber absorber = new TextFragmentAbsorber();
                absorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);
                page.Accept(absorber);
                fragments.AddRange(absorber.TextFragments);
            }

            if (fragments.Count == 0)
            {
                Console.WriteLine("No text found in the document.");
                return;
            }

            // Compute average font size (reference for normal text)
            double avgFontSize = fragments.Average(f => f.TextState.FontSize);

            // Determine a baseline Y coordinate using fragments close to average size
            double baselineY = fragments
                .Where(f => Math.Abs(f.TextState.FontSize - avgFontSize) < avgFontSize * 0.1)
                .Select(f => f.Rectangle.LLY) // lower‑left Y of the fragment's bounding box
                .DefaultIfEmpty(0)
                .Average();

            // Offsets for superscript/subscript detection (tuned as 20% of avg font size)
            double yOffset = avgFontSize * 0.2;
            double sizeThreshold = avgFontSize * 0.8; // smaller than 80% of avg considered a script

            StringBuilder sb = new StringBuilder();

            foreach (TextFragment frag in fragments)
            {
                string text = frag.Text;
                double fontSize = frag.TextState.FontSize;
                double yPos = frag.Rectangle.LLY; // vertical position of the fragment

                bool isSuperscript = fontSize < sizeThreshold && yPos > baselineY + yOffset;
                bool isSubscript = fontSize < sizeThreshold && yPos < baselineY - yOffset;

                if (isSuperscript)
                {
                    // Annotate superscript with ^{...}
                    sb.Append($"^{{{text}}}");
                }
                else if (isSubscript)
                {
                    // Annotate subscript with _{...}
                    sb.Append($"_{{{text}}}");
                }
                else
                {
                    sb.Append(text);
                }
            }

            // Write annotated text to output file
            File.WriteAllText(outputTxt, sb.ToString(), Encoding.UTF8);
            Console.WriteLine($"Extraction complete. Output written to '{outputTxt}'.");
        }
    }
}
