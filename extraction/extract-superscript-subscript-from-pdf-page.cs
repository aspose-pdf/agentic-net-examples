using System;
using System.IO;
using System.Text;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "extracted.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least three pages
            if (doc.Pages.Count < 3)
            {
                Console.Error.WriteLine("The document does not contain a third page.");
                return;
            }

            // Create a TextFragmentAbsorber to capture individual text fragments
            TextFragmentAbsorber absorber = new TextFragmentAbsorber();
            absorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);

            // Apply the absorber to page three (pages are 1‑based)
            doc.Pages[3].Accept(absorber);

            // Calculate the average font size on the page – this will be used as a baseline
            var fragments = absorber.TextFragments;
            double averageFontSize = 0;
            int count = 0;
            foreach (TextFragment frag in fragments)
            {
                if (frag.TextState != null && frag.TextState.FontSize > 0)
                {
                    averageFontSize += frag.TextState.FontSize;
                    count++;
                }
            }
            if (count > 0)
                averageFontSize /= count;

            // Collect fragments that are likely superscript or subscript (smaller than the average)
            StringBuilder sb = new StringBuilder();
            const double sizeThresholdFactor = 0.8; // 80% of average size is considered smaller

            foreach (TextFragment fragment in fragments)
            {
                var state = fragment.TextState;
                if (state != null && state.FontSize > 0)
                {
                    // If the fragment's font size is noticeably smaller than the page average,
                    // treat it as superscript/subscript.
                    if (state.FontSize < averageFontSize * sizeThresholdFactor)
                    {
                        sb.AppendLine(fragment.Text);
                    }
                }
            }

            // Write the extracted text to a .txt file
            File.WriteAllText(outputPath, sb.ToString(), Encoding.UTF8);
        }

        Console.WriteLine($"Extraction complete. Results saved to '{outputPath}'.");
    }
}
