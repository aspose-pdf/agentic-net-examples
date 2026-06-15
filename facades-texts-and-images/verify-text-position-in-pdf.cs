using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class PdfTextPositionTests
{
    static void Main()
    {
        const string testText = "Hello Aspose";
        const double expectedX = 100.0;
        const double expectedY = 500.0;
        const double tolerance = 0.5; // acceptable deviation in points

        // Create a new PDF document with a single blank page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Build a text fragment positioned at the expected coordinates
            TextFragment tf = new TextFragment(testText);
            tf.Position = new Aspose.Pdf.Text.Position(expectedX, expectedY);
            tf.TextState.FontSize = 12;
            tf.TextState.Font = FontRepository.FindFont("Helvetica");
            tf.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

            // Append the fragment to the page
            TextBuilder builder = new TextBuilder(page);
            builder.AppendText(tf);

            // Extract the same text to verify its coordinates
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(testText);
            page.Accept(absorber);

            if (absorber.TextFragments.Count == 0)
            {
                Console.Error.WriteLine("Failed to locate the added text.");
                return;
            }

            // TextFragmentAbsorber collections are 1‑based
            TextFragment extracted = absorber.TextFragments[1];
            double actualX = extracted.Position.XIndent;
            double actualY = extracted.Position.YIndent;

            bool xMatches = Math.Abs(actualX - expectedX) <= tolerance;
            bool yMatches = Math.Abs(actualY - expectedY) <= tolerance;

            Console.WriteLine($"Expected X: {expectedX}, Actual X: {actualX}, Match: {xMatches}");
            Console.WriteLine($"Expected Y: {expectedY}, Actual Y: {actualY}, Match: {yMatches}");

            Console.WriteLine(xMatches && yMatches ? "Test passed." : "Test failed.");
        }
    }
}