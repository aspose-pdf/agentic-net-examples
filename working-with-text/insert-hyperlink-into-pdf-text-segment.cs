using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Annotations; // Actions and Highlighting live here

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "hyperlink_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the existing PDF (or create a new one if you prefer)
        using (Document doc = new Document(inputPath))
        {
            // Ensure we have at least one page; add one if the document is empty
            if (doc.Pages.Count == 0)
                doc.Pages.Add();

            // Use the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Create a TextFragment to hold the hyperlink text
            TextFragment fragment = new TextFragment();

            // Create a TextSegment with the visible text
            TextSegment segment = new TextSegment("Click here to visit Aspose");

            // Optional: style the hyperlink (blue and underlined)
            segment.TextState.ForegroundColor = Color.FromRgb(0, 0, 255); // blue
            segment.TextState.Underline = true;

            // Add the segment to the fragment
            fragment.Segments.Add(segment);

            // Position the fragment on the page (optional)
            fragment.Position = new Position(100, 700); // X=100, Y=700
            page.Paragraphs.Add(fragment);

            // Define a rectangle that roughly covers the text (adjust as needed)
            var rect = new Aspose.Pdf.Rectangle(100, 690, 300, 710); // left, bottom, right, top

            // Create a link annotation and assign a URI action
            LinkAnnotation link = new LinkAnnotation(page, rect);
            link.Action = new GoToURIAction("https://www.aspose.com");
            link.Contents = "Visit Aspose website"; // tooltip text

            // Add the annotation to the page
            page.Annotations.Add(link);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Hyperlink added and saved to '{outputPath}'.");
    }
}
