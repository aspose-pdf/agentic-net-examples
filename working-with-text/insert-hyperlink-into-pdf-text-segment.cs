using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;   // required for TextFragment, TextSegment, WebHyperlink

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

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

            // Create a text fragment that will hold the main text
            TextFragment fragment = new TextFragment("Visit ");
            fragment.Position = new Position(100, 600); // place near the top-left

            // Create a text segment that will act as the hyperlink
            TextSegment linkSegment = new TextSegment("Aspose.Pdf");
            // Assign a WebHyperlink (URL) to the segment
            linkSegment.Hyperlink = new WebHyperlink("https://www.aspose.com/pdf");

            // Add the hyperlink segment to the fragment's segment collection
            fragment.Segments.Add(linkSegment);

            // Append the fragment (with the hyperlink segment) to the page
            page.Paragraphs.Add(fragment);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Hyperlinked PDF saved to '{outputPath}'.");
    }
}