using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least 5 pages
            if (doc.Pages.Count < 5)
            {
                Console.Error.WriteLine("Document does not contain page 5.");
                return;
            }

            // Extract the first text fragment on page 5 to obtain its baseline coordinates
            TextFragmentAbsorber absorber = new TextFragmentAbsorber();
            // Accept the absorber only on page 5
            doc.Pages[5].Accept(absorber);

            // If no text is found, fall back to default positioning
            double targetX = 0;
            double targetY = 0;

            if (absorber.TextFragments.Count > 0)
            {
                // Take the first fragment (e.g., first paragraph)
                TextFragment fragment = absorber.TextFragments[1]; // 1‑based indexing
                // Position.XIndent and Position.YIndent give the lower‑left corner of the fragment
                targetX = fragment.Position.XIndent;
                targetY = fragment.Position.YIndent;
            }
            else
            {
                Console.WriteLine("No text found on page 5; using default stamp position.");
                // Default to 50 points from left and 50 points from bottom
                targetX = 50;
                targetY = 50;
            }

            // Create a stamp (example: a simple text stamp)
            TextStamp stamp = new TextStamp("★"); // any stamp content you need
            // Align the stamp with the baseline of the extracted text
            stamp.XIndent = targetX; // horizontal position
            stamp.YIndent = targetY; // vertical position (baseline)

            // Optional: match the font size of the surrounding text for visual consistency
            stamp.TextState.FontSize = 12;
            stamp.TextState.Font = FontRepository.FindFont("Arial");
            stamp.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

            // Add the stamp to page 5
            Page pageFive = doc.Pages[5];
            pageFive.AddStamp(stamp);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Stamp repositioned and saved to '{outputPath}'.");
    }
}