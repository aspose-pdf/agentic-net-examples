using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input and output PDF paths
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF document (create rule)
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (pages are 1‑based)
            Page page = doc.Pages[1];

            // Create a text fragment that will hold the segment
            TextFragment tf = new TextFragment();
            tf.Position = new Position(100, 600); // place the text on the page

            // Create a text segment with the visible text
            TextSegment segment = new TextSegment("Click here");
            // Assign a web hyperlink to the segment (URI action)
            segment.Hyperlink = new WebHyperlink("https://www.example.com");

            // Add the segment to the fragment's segment collection
            tf.Segments.Add(segment);

            // Append the fragment (with the clickable segment) to the page
            TextBuilder builder = new TextBuilder(page);
            builder.AppendText(tf);

            // Save the modified PDF (save rule)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with clickable text: {outputPath}");
    }
}