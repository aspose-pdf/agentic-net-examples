using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document; using ensures deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // Create a TextFragmentAbsorber to capture text fragments with their positions.
            TextFragmentAbsorber absorber = new TextFragmentAbsorber();

            // Use Flatten mode to preserve original layout coordinates.
            absorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Flatten);

            // Apply the absorber to all pages.
            doc.Pages.Accept(absorber);

            // Iterate over each captured fragment.
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                // The fragment's Rectangle provides its bounding box on the page.
                Aspose.Pdf.Rectangle rect = fragment.Rectangle;

                Console.WriteLine($"Page {fragment.Page.Number}: \"{fragment.Text}\"");
                Console.WriteLine($"  Bounding Box: Left={rect.LLX}, Bottom={rect.LLY}, Right={rect.URX}, Top={rect.URY}");

                // Each fragment may consist of multiple segments; output their individual boxes.
                foreach (TextSegment segment in fragment.Segments)
                {
                    Aspose.Pdf.Rectangle segRect = segment.Rectangle;
                    Console.WriteLine($"    Segment \"{segment.Text}\" Box: Left={segRect.LLX}, Bottom={segRect.LLY}, Right={segRect.URX}, Top={segRect.URY}");
                }
            }
        }
    }
}