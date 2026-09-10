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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create an absorber that extracts all text fragments on each page
            TextFragmentAbsorber absorber = new TextFragmentAbsorber();

            // Accept the absorber for every page in the document
            foreach (Page page in doc.Pages)
            {
                page.Accept(absorber);
            }

            // Iterate over all found text fragments
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                // Basic information about the fragment
                string text = fragment.Text ?? string.Empty;
                double x = fragment.Position?.XIndent ?? 0;
                double y = fragment.Position?.YIndent ?? 0;

                // Log the fragment text and its position (used to infer line breaks)
                Console.WriteLine($"Fragment: \"{text}\"");
                Console.WriteLine($"  Position -> X: {x}, Y: {y}");

                // Additional details: rectangle bounds of the fragment
                if (fragment.Rectangle != null)
                {
                    Console.WriteLine($"  Rectangle -> LLX:{fragment.Rectangle.LLX}, LLY:{fragment.Rectangle.LLY}, " +
                                      $"URX:{fragment.Rectangle.URX}, URY:{fragment.Rectangle.URY}");
                }

                // If needed, you can also inspect individual text segments
                foreach (TextSegment segment in fragment.Segments)
                {
                    Console.WriteLine($"    Segment text: \"{segment.Text}\"");
                    Console.WriteLine($"    Segment position -> X: {segment.Position?.XIndent}, Y: {segment.Position?.YIndent}");
                }

                Console.WriteLine(); // blank line between fragments
            }
        }
    }
}