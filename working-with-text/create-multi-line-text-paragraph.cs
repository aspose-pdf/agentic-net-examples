using System;
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
            // Add a blank page (Pages collection is 1‑based)
            doc.Pages.Add();
            Page page = doc.Pages[1];

            // Create a TextParagraph instance
            TextParagraph paragraph = new TextParagraph();

            // Define the rectangle where the paragraph will be drawn
            // Fully qualify Rectangle to avoid ambiguity with System.Drawing.Rectangle
            paragraph.Rectangle = new Aspose.Pdf.Rectangle(100, 600, 300, 750);

            // Enable word wrapping by words
            paragraph.FormattingOptions.WrapMode = TextFormattingOptions.WordWrapMode.ByWords;

            // Optional visual settings
            paragraph.HorizontalAlignment = HorizontalAlignment.Center;
            paragraph.Margin = new MarginInfo { Top = 5, Bottom = 5, Left = 5, Right = 5 };

            // Append multiple lines of text
            paragraph.AppendLine("The quick brown fox jumps over the lazy dog.");
            paragraph.AppendLine("Line two of the paragraph.");
            paragraph.AppendLine("Line three of the paragraph.");

            // Use TextBuilder to render the paragraph onto the page
            TextBuilder builder = new TextBuilder(page);
            builder.AppendParagraph(paragraph);

            // Save the document as PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}