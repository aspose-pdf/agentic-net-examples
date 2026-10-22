using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "hyperlink.pdf";

        // Ensure the output directory exists
        string outDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outDir) && !Directory.Exists(outDir))
            Directory.CreateDirectory(outDir);

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a single page
            Page page = doc.Pages.Add();

            // Create a text fragment (container for text segments)
            TextFragment fragment = new TextFragment();

            // Create a text segment with the visible text
            TextSegment segment = new TextSegment("Visit Aspose");

            // Style the segment (blue, underlined, Arial 12pt)
            segment.TextState.Font = FontRepository.FindFont("Arial");
            segment.TextState.FontSize = 12;
            segment.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;
            segment.TextState.Underline = true;

            // Add the segment to the fragment
            fragment.Segments.Add(segment);

            // Add the fragment to the page (wrapped in a paragraph automatically)
            page.Paragraphs.Add(fragment);

            // Create a hyperlink annotation. A zero‑size rectangle is sufficient for external URIs.
            LinkAnnotation link = new LinkAnnotation(page, new Aspose.Pdf.Rectangle(0, 0, 0, 0))
            {
                Action = new GoToURIAction("https://www.aspose.com")
            };
            page.Annotations.Add(link);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with hyperlink saved to '{outputPath}'.");
    }
}
