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

            // Define the rectangle where the paragraph will be drawn
            paragraph.Rectangle = new Aspose.Pdf.Rectangle(100, 600, 400, 800);

            // Enable word wrapping by words
            paragraph.FormattingOptions.WrapMode = TextFormattingOptions.WordWrapMode.ByWords;

            // Optional visual settings
            paragraph.HorizontalAlignment = HorizontalAlignment.Center;
            paragraph.VerticalAlignment = VerticalAlignment.Top;
            paragraph.Margin = new MarginInfo { Top = 10, Bottom = 10, Left = 5, Right = 5 };

            // Begin edit for better performance while adding lines
            paragraph.BeginEdit();

            // Append multiple lines of text
            paragraph.AppendLine("The quick brown fox jumps over the lazy dog.");
            paragraph.AppendLine("Line two of the paragraph.");
            paragraph.AppendLine("Line three of the paragraph.");

            // End edit to finalize layout calculations
            paragraph.EndEdit();

            // Use TextBuilder to place the paragraph on the page
            TextBuilder builder = new TextBuilder(page);
            builder.AppendParagraph(paragraph);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}