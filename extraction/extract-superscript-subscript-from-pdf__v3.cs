using System;
using System.IO;
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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Use TextFragmentAbsorber (not TextAbsorber) to gain access to TextFragments collection
            TextFragmentAbsorber absorber = new TextFragmentAbsorber();
            // Pure formatting mode preserves formatting flags in TextState
            absorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);

            // Apply the absorber only to the first page (pages are 1‑based)
            pdfDoc.Pages[1].Accept(absorber);

            // First pass – collect all font sizes to compute a baseline (average) size
            double totalFontSize = 0;
            int fragmentCount = 0;
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                totalFontSize += fragment.TextState.FontSize;
                fragmentCount++;
            }

            // Guard against division by zero (empty page)
            double averageFontSize = fragmentCount > 0 ? totalFontSize / fragmentCount : 0;

            // Heuristic: treat fragments whose font size is noticeably smaller than the page average
            // as superscript or subscript. The 0.8 factor works well for typical PDFs.
            const double sizeThresholdFactor = 0.8;

            StringBuilder extracted = new StringBuilder();
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                if (averageFontSize > 0 && fragment.TextState.FontSize < averageFontSize * sizeThresholdFactor)
                {
                    extracted.AppendLine(fragment.Text);
                }
            }

            // Write the collected text to a plain text file
            File.WriteAllText(outputTxtPath, extracted.ToString());

            Console.WriteLine($"Superscript/subscript text saved to '{outputTxtPath}'.");
        }
    }
}
