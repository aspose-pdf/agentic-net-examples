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

        // Load the PDF document (lifecycle rule: use using)
        using (Document doc = new Document(inputPdf))
        {
            // Absorb all text fragments from the document
            TextFragmentAbsorber absorber = new TextFragmentAbsorber();
            doc.Pages.Accept(absorber);

            // Use a set to collect unique Y positions (baseline) representing line breaks
            HashSet<double> lineYPositions = new HashSet<double>();

            // Iterate over all absorbed text fragments (no need for PageNumber property)
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                // Baseline Y coordinate of the fragment
                double y = fragment.Position.YIndent;
                lineYPositions.Add(y);
            }

            // Convert the set to a sorted list (top to bottom: higher Y first)
            List<double> sortedLines = new List<double>(lineYPositions);
            sortedLines.Sort((a, b) => b.CompareTo(a));

            // Serialize the list to JSON
            string json = JsonSerializer.Serialize(sortedLines, new JsonSerializerOptions { WriteIndented = true });

            // Write JSON to output file
            File.WriteAllText(outputJson, json);
        }

        Console.WriteLine($"Line break positions saved to '{outputJson}'.");
    }
}
