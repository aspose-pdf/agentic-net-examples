using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";   // source PDF (can be empty or existing)
        const string outputPath = "output.pdf";  // result PDF

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Create a multi‑line text fragment.
            // Newline characters create separate lines when the fragment is rendered.
            string multiLineText = "First line\nSecond line\nThird line";

            TextFragment tf = new TextFragment(multiLineText);
            tf.Position = new Position(100, 700); // starting position (bottom‑left corner)

            // Set visual appearance
            tf.TextState.FontSize = 12;
            tf.TextState.Font = FontRepository.FindFont("Helvetica");
            tf.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;
            tf.TextState.BackgroundColor = Aspose.Pdf.Color.LightGray;

            // Append the fragment to the page using TextBuilder
            TextBuilder builder = new TextBuilder(page);
            builder.AppendText(tf);

            // After appending, the TextFragment still holds its segments.
            // Each segment corresponds to a continuous run of text.
            // Newline characters cause a new segment, so we can treat them as line breaks.
            Console.WriteLine("Line‑break information (segment positions):");
            int lineIndex = 1;
            foreach (TextSegment segment in tf.Segments)
            {
                // Position.YIndent is the baseline Y coordinate of the segment.
                // Position.XIndent is the baseline X coordinate.
                Console.WriteLine($"Line {lineIndex}:");
                Console.WriteLine($"  Text   : {segment.Text}");
                Console.WriteLine($"  XIndent: {segment.Position.XIndent}");
                Console.WriteLine($"  YIndent: {segment.Position.YIndent}");
                lineIndex++;
            }

            // Save the modified document (PDF format, no extra SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved to '{outputPath}'.");
    }
}