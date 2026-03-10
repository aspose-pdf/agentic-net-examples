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

        // Load the PDF document (lifecycle: load)
        using (Document doc = new Document(inputPath))
        {
            // Configure extraction to preserve formatting (superscript/subscript flags)
            TextExtractionOptions extractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);

            // Absorb all text fragments from the document
            TextFragmentAbsorber absorber = new TextFragmentAbsorber();
            absorber.ExtractionOptions = extractionOptions;
            doc.Pages.Accept(absorber);

            // Iterate through each fragment and its segments
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                foreach (TextSegment segment in fragment.Segments)
                {
                    // Superscript detection (TextFragmentState.Superscript)
                    if (segment.TextState.Superscript)
                    {
                        Console.WriteLine($"Superscript found on page {fragment.Page.Number}: \"{segment.Text}\"");
                    }

                    // Subscript detection (TextFragmentState.Subscript)
                    if (segment.TextState.Subscript)
                    {
                        Console.WriteLine($"Subscript found on page {fragment.Page.Number}: \"{segment.Text}\"");
                    }
                }
            }
        }
    }
}