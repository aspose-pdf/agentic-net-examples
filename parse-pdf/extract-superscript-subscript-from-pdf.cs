using System;
using System.IO;
using System.Text;
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

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Create a TextFragmentAbsorber to capture all text fragments with positioning info
            TextFragmentAbsorber absorber = new TextFragmentAbsorber();

            // Use the Flatten mode to get coordinates for each fragment (required for superscript/subscript detection)
            absorber.ExtractionOptions = new TextExtractionOptions(
                Aspose.Pdf.Text.TextExtractionOptions.TextFormattingMode.Flatten);

            // Extract text from all pages
            doc.Pages.Accept(absorber);

            // Build the output string with annotations
            StringBuilder sb = new StringBuilder();

            // Keep reference to the previous fragment for comparison
            TextFragment prev = null;

            foreach (TextFragment cur in absorber.TextFragments)
            {
                // If there is a previous fragment, try to detect superscript/subscript
                if (prev != null)
                {
                    // Font size comparison (threshold 0.8)
                    bool smallerFont = cur.TextState.FontSize < prev.TextState.FontSize * 0.8;

                    // Baseline Y position comparison (higher Y = lower on page because PDF origin is bottom‑left)
                    // In PDF coordinates, larger Y means higher on the page.
                    bool higherBaseline = cur.BaselinePosition.YIndent > prev.BaselinePosition.YIndent;
                    bool lowerBaseline = cur.BaselinePosition.YIndent < prev.BaselinePosition.YIndent;

                    if (smallerFont && higherBaseline)
                    {
                        // Superscript detected
                        sb.Append("[sup]");
                        sb.Append(cur.Text);
                        sb.Append("[/sup]");
                    }
                    else if (smallerFont && lowerBaseline)
                    {
                        // Subscript detected
                        sb.Append("[sub]");
                        sb.Append(cur.Text);
                        sb.Append("[/sub]");
                    }
                    else
                    {
                        // Normal text
                        sb.Append(cur.Text);
                    }
                }
                else
                {
                    // First fragment – just append its text
                    sb.Append(cur.Text);
                }

                // Preserve spacing if the fragment ends with a space
                if (cur.Text.EndsWith(" "))
                    sb.Append(' ');

                prev = cur;
            }

            // Write the annotated text to a plain text file
            File.WriteAllText(outputTxt, sb.ToString());

            Console.WriteLine($"Extraction completed. Output written to '{outputTxt}'.");
        }
    }
}