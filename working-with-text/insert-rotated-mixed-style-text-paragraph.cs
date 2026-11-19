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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least three pages (Aspose.Pdf uses 1‑based indexing)
            while (doc.Pages.Count < 3)
            {
                doc.Pages.Add();
            }

            // Get the third page
            Page page = doc.Pages[3];

            // Create a TextFragment that will contain multiple styled text segments
            TextFragment mixedFragment = new TextFragment();

            // First segment: bold, red, 20pt
            TextSegment segment1 = new TextSegment("Bold Red ");
            segment1.TextState.Font = FontRepository.FindFont("Arial");
            segment1.TextState.FontSize = 20;
            segment1.TextState.FontStyle = FontStyles.Bold;
            segment1.TextState.ForegroundColor = Aspose.Pdf.Color.FromRgb(1, 0, 0); // red

            // Second segment: italic, blue, 20pt
            TextSegment segment2 = new TextSegment("Italic Blue");
            segment2.TextState.Font = FontRepository.FindFont("Arial");
            segment2.TextState.FontSize = 20;
            segment2.TextState.FontStyle = FontStyles.Italic;
            segment2.TextState.ForegroundColor = Aspose.Pdf.Color.FromRgb(0, 0, 1); // blue

            // Add the styled segments to the fragment
            mixedFragment.Segments.Add(segment1);
            mixedFragment.Segments.Add(segment2);

            // Rotate the entire fragment 45 degrees
            mixedFragment.TextState.Rotation = 45;

            // Optional: set the position of the fragment on the page
            mixedFragment.Position = new Position(100, 500);

            // Insert the fragment into the page's content
            page.Paragraphs.Add(mixedFragment);

            // Save the modified PDF (save inside the using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
