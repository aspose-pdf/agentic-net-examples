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
            // Add a page first (Aspose.Pdf starts with an empty Pages collection)
            Page page = doc.Pages.Add();

            // Create a TextParagraph instance
            TextParagraph paragraph = new TextParagraph();

            // Define the rectangle where the paragraph will be placed
            paragraph.Rectangle = new Aspose.Pdf.Rectangle(100, 600, 300, 750);

            // Set word‑wrap mode to wrap by words
            paragraph.FormattingOptions.WrapMode = TextFormattingOptions.WordWrapMode.ByWords;

            // Optional visual properties
            paragraph.HorizontalAlignment = HorizontalAlignment.Center;
            paragraph.VerticalAlignment = VerticalAlignment.Top;
            paragraph.Margin = new MarginInfo { Top = 5, Bottom = 5, Left = 5, Right = 5 };

            // Begin edit to improve performance while adding lines
            paragraph.BeginEdit();
            paragraph.AppendLine("The quick brown fox jumps over the lazy dog.");
            paragraph.AppendLine("Second line of the paragraph.");
            paragraph.AppendLine("Third line, demonstrating multi‑line text.");
            paragraph.EndEdit();

            // Append the paragraph to the page using TextBuilder
            TextBuilder builder = new TextBuilder(page);
            builder.AppendParagraph(paragraph);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
