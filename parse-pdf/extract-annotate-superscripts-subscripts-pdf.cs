using System;
using System.IO;
using System.Collections.Generic;
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

        // Load the PDF document
        using (Document doc = new Document(inputPdfPath))
        {
            // Prepare a TextFragmentAbsorber with Flatten mode to get positioning info
            TextFragmentAbsorber absorber = new TextFragmentAbsorber();
            absorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Flatten);

            // Extract text from all pages
            doc.Pages.Accept(absorber);

            // List to hold annotated lines
            List<string> annotatedLines = new List<string>();

            // Process each text fragment
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                // Baseline of the whole fragment (used as reference line)
                double fragmentBaseline = fragment.Rectangle?.LLY ?? 0;
                double fragmentFontSize = fragment.TextState?.FontSize ?? 0;

                // Process each segment within the fragment
                foreach (TextSegment segment in fragment.Segments)
                {
                    double segmentBaseline = segment.Rectangle?.LLY ?? 0;
                    double segmentFontSize = segment.TextState?.FontSize ?? 0;
                    string annotation = string.Empty;

                    // Simple heuristic: compare baseline and font size to decide superscript/subscript
                    if (segmentBaseline > fragmentBaseline + 1 && segmentFontSize < fragmentFontSize)
                    {
                        annotation = " (Superscript)";
                    }
                    else if (segmentBaseline < fragmentBaseline - 1 && segmentFontSize < fragmentFontSize)
                    {
                        annotation = " (Subscript)";
                    }

                    // Append the segment text with possible annotation
                    annotatedLines.Add($"{segment.Text}{annotation}");
                }
            }

            // Write the annotated text to the output file
            File.WriteAllLines(outputTxtPath, annotatedLines);
            Console.WriteLine($"Extraction completed. Output written to '{outputTxtPath}'.");
        }
    }
}
