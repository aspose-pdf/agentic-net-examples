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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Use the parameter‑less constructor of TextFragmentAbsorber.
            // The older Aspose.Pdf versions do not expose TextExtractionOptions
            // or the TextFormattingMode enum.
            TextFragmentAbsorber absorber = new TextFragmentAbsorber();

            // Extract all text fragments from all pages
            doc.Pages.Accept(absorber);

            // Group fragments by their Y coordinate (top) to identify lines.
            // A tolerance is used because floating point values may differ slightly.
            const double yTolerance = 0.5;
            var lines = new List<LineInfo>();
            var processed = new HashSet<TextFragment>();

            foreach (var fragment in absorber.TextFragments)
            {
                if (processed.Contains(fragment))
                    continue;

                // Determine the Y coordinate (top) of the current fragment.
                // Use the lower‑left Y (LLY) of the fragment's rectangle – this is
                // available across all supported Aspose.Pdf versions.
                double lineY = fragment.Rectangle.LLY;

                // Find all fragments that belong to the same line (within tolerance)
                var lineFragments = absorber.TextFragments
                    .Where(f => Math.Abs(f.Rectangle.LLY - lineY) <= yTolerance)
                    .ToList();

                // Mark them as processed
                foreach (var f in lineFragments)
                    processed.Add(f);

                // Determine the leftmost and rightmost X positions on this line.
                // Use the fragment rectangle's LLX (left) and URX (right) values.
                double minX = lineFragments.Min(f => f.Rectangle.LLX);
                double maxX = lineFragments.Max(f => f.Rectangle.URX);

                // Store the line information
                lines.Add(new LineInfo
                {
                    Index = lines.Count + 1,
                    Y = lineY,
                    StartX = minX,
                    EndX = maxX
                });
            }

            // Serialize the line information to JSON
            JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(lines, jsonOptions);
            File.WriteAllText(outputJsonPath, json);
            Console.WriteLine($"Line break positions saved to '{outputJsonPath}'.");
        }
    }

    // Simple DTO for JSON serialization
    private class LineInfo
    {
        public int Index { get; set; }
        public double Y { get; set; }
        public double StartX { get; set; }
        public double EndX { get; set; }
    }
}
