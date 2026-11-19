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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based page indexing
            Page page = doc.Pages[1];

            // Multi‑line text using various line‑break characters
            string multiLineText = "First line\nSecond line\r\nThird line";

            // Create a TextFragment containing the multi‑line text
            TextFragment tf = new TextFragment(multiLineText);

            // Set the fragment's position on the page (optional)
            tf.Position = new Position(100, 700); // Position is in Aspose.Pdf.Text namespace

            // Append the fragment to the page's paragraph collection
            page.Paragraphs.Add(tf);

            // Retrieve line‑break information from the fragment's segments
            Console.WriteLine("Line break information per segment:");
            int index = 0;
            int segmentCount = tf.Segments.Count;
            foreach (TextSegment segment in tf.Segments)
            {
                // In newer Aspose.Pdf versions TextSegment no longer exposes IsNewLine.
                // A line break exists after every segment except the last one.
                bool hasLineBreak = index < segmentCount - 1;
                Console.WriteLine($"Segment {index}: \"{segment.Text}\"  NewLine: {hasLineBreak}");
                index++;
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
