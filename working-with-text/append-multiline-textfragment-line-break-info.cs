using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";

        // Create a new PDF document and add a blank page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Multi‑line text (lines separated by '\n')
            string multiLineText = "First line\nSecond line\nThird line";

            // Create a TextFragment with the multi‑line content
            TextFragment tf = new TextFragment(multiLineText);
            tf.Position = new Position(100, 700); // baseline start point

            // Set common text appearance
            tf.TextState.FontSize = 12;
            tf.TextState.Font = FontRepository.FindFont("Helvetica");
            tf.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

            // Append the fragment to the page
            TextBuilder builder = new TextBuilder(page);
            builder.AppendText(tf);

            // Retrieve line‑break information from the generated segments
            Console.WriteLine("Line break information:");
            foreach (TextSegment segment in tf.Segments)
            {
                // Each segment corresponds to a line; YIndent decreases for subsequent lines
                Console.WriteLine($"Line: \"{segment.Text}\"  Y = {segment.Position.YIndent}");
            }

            // Save the document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}