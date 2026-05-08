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
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create an absorber that extracts all text fragments from the document
            TextFragmentAbsorber absorber = new TextFragmentAbsorber();

            // Perform the extraction on the whole document
            doc.Pages.Accept(absorber);

            // Iterate over each extracted text fragment
            int fragmentIndex = 1;
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                Console.WriteLine($"--- TextFragment #{fragmentIndex} ---");
                Console.WriteLine($"Full Text: {fragment.Text}");

                // Group the segments by their Y coordinate to determine logical lines
                var lineGroups = fragment.Segments
                    .Cast<TextSegment>()
                    .GroupBy(seg => Math.Round(seg.Position.YIndent, 2)) // tolerance of 0.01
                    .OrderByDescending(g => g.Key); // higher Y = upper line

                int lineNumber = 1;
                foreach (var lineGroup in lineGroups)
                {
                    // Concatenate the texts of all segments that belong to the same line
                    string lineText = string.Concat(lineGroup.Select(s => s.Text));

                    Console.WriteLine($"Line {lineNumber}:");
                    Console.WriteLine($"  YIndent : {lineGroup.Key}");
                    Console.WriteLine($"  Text    : {lineText}");

                    // Optional: output segment‑level details
                    int segIdx = 1;
                    foreach (TextSegment seg in lineGroup)
                    {
                        Console.WriteLine($"    Segment {segIdx}:");
                        Console.WriteLine($"      Text       : {seg.Text}");
                        Console.WriteLine($"      XIndent    : {seg.Position.XIndent}");
                        Console.WriteLine($"      YIndent    : {seg.Position.YIndent}");
                        segIdx++;
                    }

                    lineNumber++;
                }

                fragmentIndex++;
                Console.WriteLine();
            }
        }
    }
}