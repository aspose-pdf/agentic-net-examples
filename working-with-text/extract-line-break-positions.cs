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
        const string inputPdf = "input.pdf";
        const string outputJson = "linebreaks.json";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document (lifecycle rule: use Document constructor and using block)
        using (Document doc = new Document(inputPdf))
        {
            // Extract all text fragments from the document
            TextFragmentAbsorber absorber = new TextFragmentAbsorber();
            doc.Pages.Accept(absorber);

            // List to hold Y‑coordinates where each line starts (baseline positions)
            List<float> lineBreakPositions = new List<float>();

            // Process each TextFragment that contains line breaks (i.e., '\n')
            foreach (TextFragment tf in absorber.TextFragments)
            {
                if (string.IsNullOrEmpty(tf.Text) || !tf.Text.Contains("\n"))
                    continue; // Skip fragments that are not multi‑line

                // Group the segments by their baseline Y coordinate (Position.YIndent)
                var groups = tf.Segments
                               .GroupBy(seg => seg.Position.YIndent)
                               .OrderByDescending(g => g.Key); // PDF origin is bottom‑left

                foreach (var group in groups)
                {
                    // Each group corresponds to a line; store its baseline Y value (cast to float)
                    lineBreakPositions.Add((float)group.Key);
                }
            }

            // Remove duplicates and sort from top to bottom (higher Y first)
            var distinctSorted = lineBreakPositions
                                 .Distinct()
                                 .OrderByDescending(y => y)
                                 .ToList();

            // Serialize the Y positions to JSON array
            string json = JsonSerializer.Serialize(distinctSorted, new JsonSerializerOptions { WriteIndented = true });

            // Write JSON to file
            File.WriteAllText(outputJson, json);

            Console.WriteLine($"Line‑break positions saved to '{outputJson}'.");
        }
    }
}
