using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Verify that page 8 exists (pages are 1‑based)
            if (doc.Pages.Count < 8)
            {
                Console.Error.WriteLine("The document has fewer than 8 pages.");
                return;
            }

            // Create a TextFragmentAbsorber to collect text fragments
            TextFragmentAbsorber absorber = new TextFragmentAbsorber
            {
                ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure)
            };

            // Apply the absorber only to page 8
            doc.Pages[8].Accept(absorber);

            // Reduce each fragment's font size by 30% (keep 70%)
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                fragment.TextState.FontSize *= 0.7f; // FontSize is a float, use float literal
            }

            // Extract the text content
            string extractedText = absorber.Text;

            // Write the extracted text to a plain‑text file
            File.WriteAllText(outputPath, extractedText);
        }

        Console.WriteLine($"Text extracted and saved to '{outputPath}'.");
    }
}
