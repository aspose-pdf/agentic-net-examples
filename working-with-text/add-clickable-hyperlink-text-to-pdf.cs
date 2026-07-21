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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (pages are 1‑based)
            Page page = doc.Pages[1];

            // Create a TextFragment that will hold the clickable segment
            TextFragment fragment = new TextFragment("Click here");
            fragment.Position = new Position(100, 600); // place on page
            fragment.TextState.FontSize = 12;
            fragment.TextState.Font = FontRepository.FindFont("Helvetica");
            fragment.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;

            // Create a TextSegment (optional, can also use the default segment)
            TextSegment segment = new TextSegment("Click here");
            // Assign a web hyperlink to the segment
            segment.Hyperlink = new WebHyperlink("https://www.example.com");
            // Replace the default segment with our customized one
            fragment.Segments.Clear();
            fragment.Segments.Add(segment);

            // Append the fragment to the page
            TextBuilder builder = new TextBuilder(page);
            builder.AppendText(fragment);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Clickable text added and saved to '{outputPath}'.");
    }
}