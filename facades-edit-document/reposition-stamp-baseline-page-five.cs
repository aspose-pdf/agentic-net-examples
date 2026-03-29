using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("Input file not found: " + inputPath);
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Get the baseline Y coordinate of the first text fragment on page 5
            Page pageFive = doc.Pages[5];

            // Use TextFragmentAbsorber to access TextFragments collection
            TextFragmentAbsorber absorber = new TextFragmentAbsorber();
            absorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);
            pageFive.Accept(absorber);
            TextFragmentCollection fragments = absorber.TextFragments;
            if (fragments.Count == 0)
            {
                Console.Error.WriteLine("No text found on page 5.");
                return;
            }

            // The collection is zero‑based; take the first fragment
            TextFragment firstFragment = fragments[0];
            // Use the fragment's rectangle lower‑left Y (LLY) as a reliable baseline coordinate
            double baselineY = firstFragment.Rectangle.LLY;

            // Create a text stamp and position it on the baseline
            TextStamp stamp = new TextStamp("Aligned Stamp");
            stamp.TextState.FontSize = 12;
            stamp.TextState.Font = FontRepository.FindFont("Helvetica");
            stamp.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;
            stamp.XIndent = 100;          // arbitrary X position
            stamp.YIndent = baselineY;    // align with baseline

            pageFive.AddStamp(stamp);

            // No need for PdfContentEditor.MoveStampById – the stamp is already placed correctly
            doc.Save(outputPath);
        }

        Console.WriteLine("Stamp repositioned and saved to " + outputPath);
    }
}
