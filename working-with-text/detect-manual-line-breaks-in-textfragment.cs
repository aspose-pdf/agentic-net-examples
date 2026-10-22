using System;
using System.IO;
using Aspose.Pdf;
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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a multi‑line text fragment (lines separated by \n or \r\n)
            TextFragment fragment = new TextFragment("First line\nSecond line\r\nThird line");

            // Add the fragment to the first page
            doc.Pages[1].Paragraphs.Add(fragment);

            // Iterate over the text segments of the fragment.
            // Each segment represents a contiguous run of text; line breaks are
            // reflected in the segment text (e.g., containing "\n" or "\r").
            foreach (TextSegment segment in fragment.Segments)
            {
                bool hasLineBreak = segment.Text.Contains("\n") || segment.Text.Contains("\r");
                Console.WriteLine($"Segment: \"{segment.Text}\"  HasLineBreak: {hasLineBreak}");
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}