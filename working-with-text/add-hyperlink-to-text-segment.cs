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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Create a text fragment that will hold the segment
            TextFragment fragment = new TextFragment("Main text ");
            fragment.Position = new Position(100, 600);
            fragment.TextState.FontSize = 12;
            fragment.TextState.Font = FontRepository.FindFont("Helvetica");
            fragment.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;

            // Create a text segment and assign a web hyperlink to it
            TextSegment segment = new TextSegment("Visit Aspose");
            segment.Hyperlink = new WebHyperlink("https://www.aspose.com");

            // Add the segment to the fragment's segment collection
            fragment.Segments.Add(segment);

            // Add the fragment (which now contains the hyperlinked segment) to the page
            page.Paragraphs.Add(fragment);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Hyperlinked PDF saved to '{outputPath}'.");
    }
}