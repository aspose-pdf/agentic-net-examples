using System;
using System.Collections.Generic;
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

        // Load the PDF inside a using block for deterministic disposal.
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Collect all text fragments from every page.
            List<TextFragment> allFragments = new List<TextFragment>();
            foreach (Page page in pdfDoc.Pages)
            {
                TextFragmentAbsorber absorber = new TextFragmentAbsorber();
                // Use Pure mode to get formatting information (font size, etc.).
                absorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);
                page.Accept(absorber);
                allFragments.AddRange(absorber.TextFragments);
            }

            if (allFragments.Count == 0)
            {
                Console.WriteLine("No text fragments found.");
                return;
            }

            // Compute average font size and average baseline Y position across all fragments.
            double totalFontSize = 0;
            double totalY = 0;
            foreach (TextFragment frag in allFragments)
            {
                totalFontSize += frag.TextState.FontSize;
                // Use the lower‑left Y coordinate of the fragment's rectangle as its baseline.
                totalY += frag.Rectangle.LLY;
            }

            double avgFontSize = totalFontSize / allFragments.Count;
            double avgY = totalY / allFragments.Count;

            // Thresholds for detecting superscript/subscript.
            double sizeThreshold = avgFontSize * 0.8;               // significantly smaller than average
            double ySupThreshold = avgY + avgFontSize * 0.2;       // noticeably higher
            double ySubThreshold = avgY - avgFontSize * 0.2;       // noticeably lower

            // Build the output with annotations.
            StringBuilder sb = new StringBuilder();

            foreach (TextFragment frag in allFragments)
            {
                string annotation = string.Empty;
                double fragSize = frag.TextState.FontSize;
                double fragY = frag.Rectangle.LLY;

                if (fragSize < sizeThreshold && fragY > ySupThreshold)
                {
                    annotation = "[Superscript] ";
                }
                else if (fragSize < sizeThreshold && fragY < ySubThreshold)
                {
                    annotation = "[Subscript] ";
                }

                sb.AppendLine($"{annotation}{frag.Text}");
            }

            // Write the annotated text to a plain .txt file.
            File.WriteAllText(outputTxtPath, sb.ToString());
            Console.WriteLine($"Extraction complete. Output written to '{outputTxtPath}'.");
        }
    }
}
