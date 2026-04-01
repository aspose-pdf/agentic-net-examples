using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Define the rectangle where the paragraph will be placed (limited width)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(50, 700, 550, 800);

            // Create a TextParagraph and assign the rectangle
            TextParagraph paragraph = new TextParagraph();
            paragraph.Rectangle = rect;

            // Configure word‑wrap mode to wrap by whole words
            paragraph.FormattingOptions.WrapMode = TextFormattingOptions.WordWrapMode.ByWords;

            // Append a long line that exceeds the rectangle width
            paragraph.AppendLine("Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.");
            paragraph.AppendLine("Second line of text that also might be long enough to require wrapping.");

            // Add the paragraph to the page using TextBuilder
            TextBuilder textBuilder = new TextBuilder(page);
            textBuilder.AppendParagraph(paragraph);

            // Save the resulting PDF
            doc.Save(outputPath);
        }

        Console.WriteLine("PDF with word‑wrapped paragraph saved to 'output.pdf'.");
    }
}