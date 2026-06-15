using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "custom_line_height.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Define the rectangle where the text paragraph will be placed
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(50, 700, 550, 800);
            
            // Create a TextParagraph and assign its rectangle
            TextParagraph paragraph = new TextParagraph
            {
                Rectangle = rect
            };

            // Create a TextState to define font, size, color and custom line height
            TextState textState = new TextState
            {
                Font = FontRepository.FindFont("Helvetica"),
                FontSize = 14,
                ForegroundColor = Aspose.Pdf.Color.Black,
                // Set custom line height (line spacing). For example, 1.5 times the font size.
                LineSpacing = 21f   // 14 * 1.5 = 21
            };

            // Append lines using the TextState with the custom line height
            paragraph.AppendLine("First line with custom line height.", textState);
            paragraph.AppendLine("Second line follows the same spacing.", textState);
            paragraph.AppendLine("Third line continues the pattern.", textState);

            // Add the paragraph to the page using TextBuilder
            TextBuilder builder = new TextBuilder(page);
            builder.AppendParagraph(paragraph);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}