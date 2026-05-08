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
        const string url = "https://www.example.com";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Work with the first page
            Page page = doc.Pages[1];

            // Create a text fragment that will hold the segment
            TextFragment tf = new TextFragment("Placeholder");
            tf.Position = new Position(100, 600);
            tf.TextState.FontSize = 12;
            tf.TextState.Font = FontRepository.FindFont("Helvetica");
            tf.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;

            // Create a text segment and assign a web hyperlink to it
            TextSegment segment = new TextSegment("Visit Example");
            segment.Hyperlink = new WebHyperlink(url);

            // Add the segment (with hyperlink) to the fragment's segment collection
            tf.Segments.Add(segment);

            // Append the fragment to the page
            TextBuilder builder = new TextBuilder(page);
            builder.AppendText(tf);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Hyperlinked PDF saved to '{outputPath}'.");
    }
}