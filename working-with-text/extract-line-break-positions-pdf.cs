using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputJsonPath = "line_breaks.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document (lifecycle: load)
        using (Document doc = new Document(inputPdfPath))
        {
            // Extract all text fragments from the document
            TextFragmentAbsorber absorber = new TextFragmentAbsorber();
            doc.Pages.Accept(absorber);

            // Dictionary to hold line‑break Y positions for each fragment (key = fragment index)
            var fragmentLines = new Dictionary<int, List<double>>();

            int fragmentIndex = 1;
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                // Each TextFragment may consist of multiple TextSegment objects.
                // The YIndent of a segment's Position represents the baseline of that segment.
                // Distinct YIndent values correspond to separate lines.
                List<double> lineYPositions = fragment.Segments
                    .Select(seg => seg.Position.YIndent)
                    .Distinct()
                    .OrderByDescending(y => y) // topmost line first (PDF origin is bottom‑left)
                    .ToList();

                fragmentLines[fragmentIndex] = lineYPositions;
                fragmentIndex++;
            }

            // Serialize the result to JSON (array of arrays)
            string json = JsonSerializer.Serialize(fragmentLines, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(outputJsonPath, json);
            Console.WriteLine($"Line‑break positions saved to '{outputJsonPath}'.");
        }
    }
}