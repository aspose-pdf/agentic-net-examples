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
        const string outputJsonPath = "linePositions.json";

        // Create a new PDF document with a single page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Multi‑line text (explicit line breaks)
            string multiLineText = "First line\nSecond line\nThird line";

            // Create a TextFragment and set its position and font size
            TextFragment fragment = new TextFragment(multiLineText);
            fragment.Position = new Position(50, 750); // start position on the page
            fragment.TextState.FontSize = 12;

            // Add the fragment to the page
            page.Paragraphs.Add(fragment);

            // After layout, each line is represented by a TextSegment
            List<double> lineYPositions = new List<double>();
            foreach (TextSegment segment in fragment.Segments)
            {
                // Use the lower‑left Y coordinate of the segment rectangle as the line position
                Aspose.Pdf.Rectangle rect = segment.Rectangle;
                lineYPositions.Add(rect.LLY);
            }

            // Serialize the Y positions to a JSON array
            string json = JsonSerializer.Serialize(lineYPositions, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(outputJsonPath, json);

            Console.WriteLine($"Line break positions saved to '{outputJsonPath}'.");
        }
    }
}