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
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Extract all text fragments from the document
            TextFragmentAbsorber absorber = new TextFragmentAbsorber();
            doc.Pages.Accept(absorber);

            // Prepare a collection to hold line‑break Y positions for each fragment
            List<List<double>> fragmentsLineBreaks = new List<List<double>>();

            foreach (TextFragment fragment in absorber.TextFragments)
            {
                // Gather baseline Y positions of all segments belonging to the fragment
                List<double> yPositions = fragment.Segments
                                                   .Select(seg => seg.Position.YIndent)
                                                   .ToList();

                // Distinct Y positions represent separate lines (tolerance can be added if needed)
                List<double> distinctLines = yPositions
                                             .Distinct()
                                             .OrderBy(y => y) // optional: sort from top to bottom
                                             .ToList();

                fragmentsLineBreaks.Add(distinctLines);
            }

            // Serialize the result to JSON
            string json = JsonSerializer.Serialize(fragmentsLineBreaks, new JsonSerializerOptions { WriteIndented = true });
            Console.WriteLine(json);
        }
    }
}