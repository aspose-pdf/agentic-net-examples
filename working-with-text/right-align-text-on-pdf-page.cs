using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "right_aligned.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Create a text fragment with the desired content
            TextFragment text = new TextFragment("Right aligned text");

            // Set the horizontal alignment of the text to Right
            text.TextState.HorizontalAlignment = HorizontalAlignment.Right;

            // Position the fragment near the top‑right corner of the page.
            // The X coordinate is the page width; the alignment will push the text
            // leftwards so that its right edge aligns with the page margin.
            text.Position = new Position(page.PageInfo.Width - 10, page.PageInfo.Height - 50);

            // Add the text fragment to the page
            page.Paragraphs.Add(text);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
