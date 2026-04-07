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
        const string url        = "https://www.example.com";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Create a text fragment that will hold the clickable segment
            TextFragment tf = new TextFragment();
            tf.Position = new Position(100, 600);               // place on page
            tf.TextState.FontSize = 12;
            tf.TextState.Font = FontRepository.FindFont("Helvetica");
            tf.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;

            // Create a text segment with the display text
            TextSegment segment = new TextSegment("Visit Example");
            // Assign a web hyperlink to the segment (clickable URI)
            segment.Hyperlink = new WebHyperlink(url);

            // Add the segment to the fragment's segment collection
            tf.Segments.Add(segment);

            // Append the fragment (with the clickable segment) to the page
            TextBuilder builder = new TextBuilder(page);
            builder.AppendText(tf);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with clickable text at '{outputPath}'.");
    }
}