using System;
using System.Collections.Generic;
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

        // Load the PDF document
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPath))
        {
            // Create an absorber that will collect all text fragments
            Aspose.Pdf.Text.TextFragmentAbsorber absorber = new Aspose.Pdf.Text.TextFragmentAbsorber();

            // Iterate through each page and extract line‑break information
            foreach (Aspose.Pdf.Page page in doc.Pages)
            {
                page.Accept(absorber);

                foreach (Aspose.Pdf.Text.TextFragment fragment in absorber.TextFragments)
                {
                    Console.WriteLine($"Page {page.Number}, Fragment Text: \"{fragment.Text}\"");

                    // Retrieve line‑break details for the current fragment
                    List<SegmentLineInfo> lineInfo = GetLineBreakInfo(fragment);

                    foreach (SegmentLineInfo info in lineInfo)
                    {
                        Console.WriteLine($"  Segment {info.SegmentIndex}: NewLine = {info.IsNewLine}, YIndent = {info.YIndent}");
                    }
                }

                // Reset absorber for the next page
                absorber.Reset();
            }
        }
    }

    // Simple structure to hold line‑break details of a segment
    private struct SegmentLineInfo
    {
        public int SegmentIndex;
        public bool IsNewLine;
        public double YIndent;
    }

    // Determines line‑breaks by comparing Y positions of consecutive segments
    private static List<SegmentLineInfo> GetLineBreakInfo(Aspose.Pdf.Text.TextFragment fragment)
    {
        var result = new List<SegmentLineInfo>();
        double? previousY = null;
        int index = 0;

        foreach (Aspose.Pdf.Text.TextSegment segment in fragment.Segments)
        {
            double currentY = segment.Position.YIndent;
            bool isNewLine = previousY.HasValue && Math.Abs(currentY - previousY.Value) > 0.1; // tolerance for Y change

            result.Add(new SegmentLineInfo
            {
                SegmentIndex = index,
                IsNewLine = isNewLine,
                YIndent = currentY
            });

            previousY = currentY;
            index++;
        }

        return result;
    }
}