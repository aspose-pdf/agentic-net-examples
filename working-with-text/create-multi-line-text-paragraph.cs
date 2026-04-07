using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Create a multi‑line TextParagraph
            TextParagraph paragraph = new TextParagraph();

            // Define the rectangle where the paragraph will be placed
            paragraph.Rectangle = new Aspose.Pdf.Rectangle(100, 600, 300, 750);

            // Enable word‑wrap by words
            paragraph.FormattingOptions.WrapMode = TextFormattingOptions.WordWrapMode.ByWords;

            // Optional visual settings
            paragraph.HorizontalAlignment = HorizontalAlignment.Center;
            paragraph.VerticalAlignment   = VerticalAlignment.Top;
            // Use MarginInfo (not the non‑existent Margin class)
            paragraph.Margin = new MarginInfo(5, 5, 5, 5);

            // Populate the paragraph with several lines
            paragraph.BeginEdit();
            paragraph.AppendLine("The quick brown fox jumps over the lazy dog.");
            paragraph.AppendLine("Second line of the paragraph.");
            paragraph.AppendLine("Third line with more text.");
            paragraph.EndEdit();

            // Append the paragraph to the page using TextBuilder
            TextBuilder builder = new TextBuilder(page);
            builder.AppendParagraph(paragraph);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}