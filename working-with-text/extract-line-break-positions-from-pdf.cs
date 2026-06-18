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
        const string inputPath = "input.pdf";          // PDF containing the multi‑line TextFragment
        const string outputJsonPath = "lineBreaks.json";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Absorb all text fragments on all pages
            TextFragmentAbsorber absorber = new TextFragmentAbsorber();
            doc.Pages.Accept(absorber);

            // Prepare a list that will hold line‑break Y positions for each fragment
            List<List<double>> fragmentsLineBreaks = new List<List<double>>();

            foreach (TextFragment fragment in absorber.TextFragments)
            {
                // Each TextFragment may consist of several TextSegment objects.
                // Segments that share the same baseline Y coordinate belong to the same visual line.
                List<double> lineYPositions = fragment.Segments
                    .Select(seg => seg.Position.YIndent)   // baseline Y of the segment
                    .Distinct()                           // unique lines
                    .OrderByDescending(y => y)            // optional: top‑to‑bottom order
                    .ToList();

                fragmentsLineBreaks.Add(lineYPositions);
            }

            // Serialize the result to JSON
            string json = JsonSerializer.Serialize(fragmentsLineBreaks, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(outputJsonPath, json);

            Console.WriteLine($"Line‑break positions saved to '{outputJsonPath}'.");
        }
    }
}