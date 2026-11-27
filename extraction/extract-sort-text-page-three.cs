using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "page3_text.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            if (doc.Pages.Count < 3)
            {
                Console.Error.WriteLine("Document has fewer than 3 pages.");
                return;
            }

            // Use TextFragmentAbsorber to obtain individual text fragments
            var absorber = new TextFragmentAbsorber
            {
                ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure)
            };

            // Extract text only from page 3
            doc.Pages[3].Accept(absorber);

            // Sort fragments by their horizontal (X) position
            var sortedFragments = absorber.TextFragments
                                          .OrderBy(f => f.Rectangle.LLX) // lower‑left X coordinate
                                          .ToList();

            // Combine the sorted fragment texts
            string combinedText = string.Join(" ", sortedFragments.Select(f => f.Text));

            // Write the result to a plain‑text file
            File.WriteAllText(outputPath, combinedText);
        }

        Console.WriteLine($"Extracted text saved to '{outputPath}'.");
    }
}