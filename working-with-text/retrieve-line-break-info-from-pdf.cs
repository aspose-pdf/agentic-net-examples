using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Facades; // Facades namespace is included as requested

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

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Use TextFragmentAbsorber to collect all text fragments from the document
            TextFragmentAbsorber absorber = new TextFragmentAbsorber();
            doc.Pages.Accept(absorber);

            int fragmentNumber = 0;
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                fragmentNumber++;
                Console.WriteLine($"Fragment #{fragmentNumber}:");

                // The Text property may contain line‑break characters.
                // Split the text to obtain individual logical lines.
                string[] lines = fragment.Text.Split(new[] { "\r\n", "\n", "\r" }, StringSplitOptions.None);
                for (int i = 0; i < lines.Length; i++)
                {
                    Console.WriteLine($"  Line {i + 1}: \"{lines[i]}\"");
                }

                // Additionally, output the position of each segment.
                // This can help correlate line breaks with their physical locations.
                int segmentNumber = 0;
                foreach (TextSegment segment in fragment.Segments)
                {
                    segmentNumber++;
                    Console.WriteLine($"  Segment #{segmentNumber} Position: X={segment.Position.XIndent}, Y={segment.Position.YIndent}");
                }
            }
        }
    }
}