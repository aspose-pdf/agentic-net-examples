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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // 1‑based page indexing
            Page page = doc.Pages[1];

            // Create a multi‑line TextFragment
            TextFragment tf = new TextFragment("First line\nSecond line\nThird line");
            tf.Position = new Position(100, 700); // baseline start point

            // Set visual properties (use Aspose.Pdf.Color, not System.Drawing)
            tf.TextState.FontSize = 12;
            tf.TextState.Font = FontRepository.FindFont("Helvetica");
            tf.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

            // Append the fragment to the page
            TextBuilder builder = new TextBuilder(page);
            builder.AppendText(tf);

            // Retrieve line‑break information from the fragment's segments
            Console.WriteLine("Line break information:");
            int lineNumber = 1;
            foreach (TextSegment segment in tf.Segments)
            {
                // Each segment corresponds to a line after the fragment is appended
                Console.WriteLine($"Line {lineNumber}: \"{segment.Text}\" at Y = {segment.Position.YIndent}");
                lineNumber++;
            }

            // Save the modified PDF (PDF format by default)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved to '{outputPath}'.");
    }
}