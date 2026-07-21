using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputJson = "linebreaks.json";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document (using the required create/load pattern)
        using (Document doc = new Document(inputPdf))
        {
            // Extract all text fragments from the first page (adjust as needed)
            TextFragmentAbsorber absorber = new TextFragmentAbsorber();
            doc.Pages[1].Accept(absorber);

            // List to hold line‑break Y positions for each multi‑line fragment
            var allLineBreaks = new List<object>();

            // Iterate over each found TextFragment
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                // Skip fragments that do not contain line breaks
                if (string.IsNullOrEmpty(fragment.Text) || !fragment.Text.Contains("\n"))
                    continue;

                // Group segment baselines by Y coordinate (within a tolerance)
                var linePositions = new List<double>();
                double tolerance = 0.5; // tolerance to treat close Y values as the same line

                foreach (TextSegment segment in fragment.Segments)
                {
                    double y = segment.Position.YIndent;

                    // If this Y is not close to any existing line position, add it as a new line start
                    bool exists = false;
                    foreach (double existing in linePositions)
                    {
                        if (Math.Abs(existing - y) <= tolerance)
                        {
                            exists = true;
                            break;
                        }
                    }

                    if (!exists)
                        linePositions.Add(y);
                }

                // Sort line positions from top to bottom (higher Y = higher on page)
                linePositions.Sort((a, b) => b.CompareTo(a));

                // Store result for this fragment
                allLineBreaks.Add(new
                {
                    Text = fragment.Text,
                    LineBreakYPositions = linePositions
                });
            }

            // Serialize the collection to JSON
            JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(allLineBreaks, jsonOptions);
            File.WriteAllText(outputJson, json);

            Console.WriteLine($"Line‑break positions written to '{outputJson}'.");
        }
    }
}