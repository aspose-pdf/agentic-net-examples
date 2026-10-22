using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single page to the document
            Page page = doc.Pages.Add();

            // Create a TextFragment which can hold multiple TextSegments (acts like a paragraph)
            TextFragment paragraph = new TextFragment();

            // Position of the paragraph on the page
            paragraph.Position = new Position(100, 500);

            // Apply a 30-degree rotation to the entire paragraph
            paragraph.TextState.Rotation = 30; // rotation in degrees (clockwise)

            // First text segment
            TextSegment segment1 = new TextSegment("Hello, ");
            segment1.TextState.FontSize = 12;
            segment1.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

            // Second text segment
            TextSegment segment2 = new TextSegment("world!");
            segment2.TextState.FontSize = 12;
            segment2.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;

            // Add the segments to the fragment (paragraph)
            paragraph.Segments.Add(segment1);
            paragraph.Segments.Add(segment2);

            // Add the fragment to the page's paragraph collection
            page.Paragraphs.Add(paragraph);

            // Save the document to a PDF file
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
