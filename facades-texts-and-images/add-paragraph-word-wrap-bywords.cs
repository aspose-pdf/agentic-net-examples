using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";

        // Create a new PDF document with a single page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Define the rectangle that limits the paragraph width
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(50, 500, 300, 700);

            // Create a TextParagraph and set word‑wrap mode to ByWords
            TextParagraph paragraph = new TextParagraph();
            paragraph.Rectangle = rect;
            paragraph.FormattingOptions.WrapMode = TextFormattingOptions.WordWrapMode.ByWords;

            // Add a long line that exceeds the rectangle width
            string longText = "The quick brown fox jumps over the lazy dog while the sun shines brightly over the hills and valleys.";
            paragraph.AppendLine(longText);

            // Append the paragraph to the page
            TextBuilder textBuilder = new TextBuilder(page);
            textBuilder.AppendParagraph(paragraph);

            // Save the resulting PDF
            doc.Save(outputPath);
        }

        Console.WriteLine("Paragraph with WordWrapMode.ByWords saved to 'output.pdf'.");
    }
}
