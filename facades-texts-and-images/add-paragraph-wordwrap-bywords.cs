using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "wrapped_paragraph.pdf";

        using (Document doc = new Document())
        {
            // Add a new page to the document
            Page page = doc.Pages.Add();

            // Define a rectangle with a limited width (will cause overflow)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(50, 700, 250, 800);

            // Create a TextParagraph and assign the rectangle
            TextParagraph paragraph = new TextParagraph();
            paragraph.Rectangle = rect;

            // Configure word‑wrap mode to wrap by whole words
            paragraph.FormattingOptions = new TextFormattingOptions(TextFormattingOptions.WordWrapMode.ByWords);

            // Add a long line of text that exceeds the rectangle width
            paragraph.AppendLine("This is a very long line of text that will exceed the defined width of the rectangle and therefore should be wrapped by words automatically.");

            // Append the paragraph to the page using TextBuilder
            TextBuilder textBuilder = new TextBuilder(page);
            textBuilder.AppendParagraph(paragraph);

            // Save the resulting PDF
            doc.Save(outputPath);
        }

        Console.WriteLine("PDF saved to 'wrapped_paragraph.pdf'");
    }
}