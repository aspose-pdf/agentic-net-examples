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

        // Ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Create a TextFragment that will hold the main text
            TextFragment tf = new TextFragment("Click here");
            tf.Position = new Position(100, 600); // Position on the page

            // Optional: set visual appearance of the fragment
            tf.TextState.Font = Aspose.Pdf.Text.FontRepository.FindFont("Helvetica");
            tf.TextState.FontSize = 12;
            tf.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;

            // Create a TextSegment (optional, could use the default segment)
            TextSegment segment = new TextSegment("Click here");
            // Assign a web hyperlink to the segment
            segment.Hyperlink = new WebHyperlink("https://www.example.com");

            // Replace the default segment with the hyperlink segment
            tf.Segments.Clear();               // remove the auto‑created segment
            tf.Segments.Add(segment);          // add our hyperlink segment

            // Append the TextFragment to the page using TextBuilder
            TextBuilder builder = new TextBuilder(page);
            builder.AppendText(tf);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with clickable text: '{outputPath}'");
    }
}