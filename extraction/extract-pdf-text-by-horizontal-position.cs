using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputFolder = "ExtractedPages";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate pages using 1‑based indexing (Aspose.Pdf requirement)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                // Extract text fragments from the current page using TextFragmentAbsorber
                var absorber = new TextFragmentAbsorber();
                absorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);
                doc.Pages[pageIndex].Accept(absorber);

                // Sort fragments by their horizontal (X) position – use the lower‑left X coordinate of the fragment rectangle
                List<TextFragment> sortedFragments = absorber.TextFragments
                    .OrderBy(f => f.Rectangle?.LLX ?? 0)
                    .ToList();

                // Concatenate fragment texts preserving the sorted order
                string pageText = string.Join(" ", sortedFragments.Select(f => f.Text));

                // Write the ordered text to a per‑page .txt file
                string outputPath = Path.Combine(outputFolder, $"Page_{pageIndex}.txt");
                File.WriteAllText(outputPath, pageText);
                Console.WriteLine($"Page {pageIndex} extracted to {outputPath}");
            }
        }
    }
}
