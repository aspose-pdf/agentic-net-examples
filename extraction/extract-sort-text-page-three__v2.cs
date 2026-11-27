using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputTxtPath = "page3_text.txt";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Aspose.Pdf uses 1‑based page indexing; page 3 is accessed with index 3
            if (doc.Pages.Count < 3)
            {
                Console.Error.WriteLine("The document has fewer than 3 pages.");
                return;
            }

            Page pageThree = doc.Pages[3];

            // Use TextFragmentAbsorber to obtain individual text fragments
            TextFragmentAbsorber absorber = new TextFragmentAbsorber();
            absorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);
            pageThree.Accept(absorber);

            // Get the collection of text fragments from the absorber
            TextFragmentCollection fragments = absorber.TextFragments;

            // Sort fragments by their baseline Y coordinate (lower‑left Y of the rectangle)
            var sortedFragments = fragments
                .OrderBy(f => f.Rectangle.LLY) // LLY = lower‑left Y; adjust ordering if needed
                .ToList();

            // Write the sorted text to a plain‑text file
            using (StreamWriter writer = new StreamWriter(outputTxtPath, false))
            {
                foreach (TextFragment fragment in sortedFragments)
                {
                    writer.WriteLine(fragment.Text);
                }
            }

            Console.WriteLine($"Extracted text from page 3 saved to '{outputTxtPath}'.");
        }
    }
}
