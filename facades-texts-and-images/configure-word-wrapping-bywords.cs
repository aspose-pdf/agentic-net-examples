using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "wrapped_paragraph.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Define a rectangle with limited width to force wrapping
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(50, 750, 250, 650); // llx, lly, urx, ury

            // Create a TextParagraph and assign the rectangle
            TextParagraph paragraph = new TextParagraph
            {
                Rectangle = rect
            };

            // Set word wrap mode to ByWords (wrap whole words only)
            paragraph.FormattingOptions.WrapMode = TextFormattingOptions.WordWrapMode.ByWords;

            // Add a long line of text that exceeds the rectangle width
            paragraph.AppendLine(
                "This is a very long sentence that will not fit within the defined rectangle width and therefore should wrap by whole words according to the ByWords setting.");

            // Append the paragraph to the page using TextBuilder
            TextBuilder builder = new TextBuilder(page);
            builder.AppendParagraph(paragraph);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}