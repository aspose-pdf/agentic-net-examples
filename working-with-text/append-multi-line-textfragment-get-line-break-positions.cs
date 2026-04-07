using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // existing PDF or blank template
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Create a multi‑line TextFragment (lines separated by '\n')
            TextFragment tf = new TextFragment("First line\nSecond line\nThird line");
            tf.Position = new Position(100, 700); // starting position (baseline)

            // Optional: set common text properties
            tf.TextState.FontSize = 12;
            tf.TextState.Font = FontRepository.FindFont("Helvetica");
            tf.TextState.ForegroundColor = Color.Black;

            // Append the fragment to the page using TextBuilder
            TextBuilder builder = new TextBuilder(page);
            builder.AppendText(tf);

            // After appending, each line is represented by a separate TextSegment.
            // Retrieve line‑break information (text and Y position) for custom rendering.
            Console.WriteLine("Line‑break information:");
            foreach (TextSegment segment in tf.Segments)
            {
                // YIndent is the baseline Y coordinate of the segment
                Console.WriteLine($"Text: \"{segment.Text}\", Y baseline: {segment.Position.YIndent}");
            }

            // Save the modified document
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Document saved to '{outputPdf}'.");
    }
}