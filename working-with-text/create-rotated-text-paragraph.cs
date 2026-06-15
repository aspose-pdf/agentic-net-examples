using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Create a TextParagraph instance
            TextParagraph paragraph = new TextParagraph();

            // Define the rectangle where the paragraph will be placed
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            paragraph.Rectangle = new Aspose.Pdf.Rectangle(100, 600, 300, 800);

            // Create several TextFragment objects with different text
            TextFragment fragment1 = new TextFragment("First line of text");
            TextFragment fragment2 = new TextFragment("Second line of text");
            TextFragment fragment3 = new TextFragment("Third line of text");

            // Optional: set common text properties (font size, color, etc.)
            fragment1.TextState.FontSize = 12;
            fragment2.TextState.FontSize = 12;
            fragment3.TextState.FontSize = 12;

            // Append the fragments to the paragraph as separate lines
            paragraph.AppendLine(fragment1);
            paragraph.AppendLine(fragment2);
            paragraph.AppendLine(fragment3);

            // Apply a 30-degree rotation to the entire paragraph
            paragraph.Rotation = 30;

            // Use TextBuilder to add the paragraph to the page
            TextBuilder textBuilder = new TextBuilder(page);
            textBuilder.AppendParagraph(paragraph);

            // Save the resulting PDF
            doc.Save("output.pdf");
        }
    }
}