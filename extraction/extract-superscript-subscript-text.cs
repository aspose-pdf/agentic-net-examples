using System;
using System.IO;
using System.Linq;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputTxtPath = "extracted.txt";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Page indexing in Aspose.Pdf is 1‑based; page 2 is accessed via Pages[2]
            Page page = doc.Pages[2];

            // Use TextFragmentAbsorber to get access to individual text fragments
            TextFragmentAbsorber absorber = new TextFragmentAbsorber();
            absorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);
            page.Accept(absorber);

            var fragments = absorber.TextFragments;
            if (fragments == null || fragments.Count == 0)
            {
                File.WriteAllText(outputTxtPath, string.Empty);
                return;
            }

            // Determine a baseline font size (average of all fragments)
            double averageFontSize = fragments.Select(f => f.TextState.FontSize).Average();
            // Consider a fragment as superscript/subscript if its font size is noticeably smaller
            const double sizeThresholdFactor = 0.8; // 20% smaller than average

            StringBuilder extracted = new StringBuilder();
            foreach (TextFragment fragment in fragments)
            {
                if (fragment.TextState.FontSize < averageFontSize * sizeThresholdFactor)
                {
                    extracted.AppendLine(fragment.Text);
                }
            }

            // Save the collected text to a .txt file
            File.WriteAllText(outputTxtPath, extracted.ToString());
        }

        Console.WriteLine($"Superscript/subscript text from page 2 saved to '{outputTxtPath}'.");
    }
}
