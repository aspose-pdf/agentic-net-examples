using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Page number to work on (1‑based as Aspose uses)
            int pageNumber = 5;
            if (doc.Pages.Count < pageNumber)
            {
                Console.Error.WriteLine($"Document has only {doc.Pages.Count} pages.");
                return;
            }

            // Use TextFragmentAbsorber to obtain text fragments (TextAbsorber does not expose them)
            TextFragmentAbsorber absorber = new TextFragmentAbsorber();
            absorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);
            doc.Pages[pageNumber].Accept(absorber);

            if (absorber.TextFragments.Count == 0)
            {
                Console.Error.WriteLine("No text found on page 5.");
                return;
            }

            // TextFragments collection is 0‑based
            TextFragment firstFragment = absorber.TextFragments[0];
            double baselineY = firstFragment.Position.YIndent; // baseline (will be treated as such by the stamp)
            double baselineX = firstFragment.Position.XIndent;

            // Create a text stamp and align its baseline with the paragraph baseline
            TextStamp stamp = new TextStamp("Aligned Stamp");
            stamp.TextState.FontSize = 12;
            stamp.TextState.Font = FontRepository.FindFont("Helvetica");
            stamp.XIndent = baselineX;               // align horizontally with the paragraph start
            stamp.YIndent = baselineY;               // align baseline vertically
            stamp.TreatYIndentAsBaseLine = true;     // ensure YIndent is treated as baseline

            // Add the stamp to page five
            doc.Pages[pageNumber].AddStamp(stamp);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Stamp repositioned and saved to '{outputPath}'.");
    }
}
