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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least three pages
            while (doc.Pages.Count < 3)
            {
                doc.Pages.Add();
            }

            // Get the third page (1‑based indexing)
            Page page = doc.Pages[3];

            // Create a TextFragment that will hold mixed‑style segments
            TextFragment mixedFragment = new TextFragment();
            mixedFragment.TextState.Font = FontRepository.FindFont("Arial");
            mixedFragment.TextState.FontSize = 12;
            // Rotate the whole fragment 45 degrees
            mixedFragment.TextState.Rotation = 45;

            // First segment – bold
            TextSegment segBold = new TextSegment("Bold Text ");
            segBold.TextState.FontStyle = FontStyles.Bold;

            // Second segment – italic
            TextSegment segItalic = new TextSegment("Italic Text ");
            segItalic.TextState.FontStyle = FontStyles.Italic;

            // Third segment – regular
            TextSegment segRegular = new TextSegment("Regular Text");
            segRegular.TextState.FontStyle = FontStyles.Regular;

            // Add segments to the fragment
            mixedFragment.Segments.Add(segBold);
            mixedFragment.Segments.Add(segItalic);
            mixedFragment.Segments.Add(segRegular);

            // Insert the fragment into the third page
            page.Paragraphs.Add(mixedFragment);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
