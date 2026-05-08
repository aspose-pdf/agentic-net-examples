using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page (1‑based indexing)
            Page page = doc.Pages.Add();

            // Define a rectangle where the paragraph will be placed
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);

            // Create a TextParagraph and assign its rectangle
            TextParagraph paragraph = new TextParagraph
            {
                Rectangle = rect
            };

            // Configure a TextState with desired font, size and line spacing (leading)
            TextState textState = new TextState
            {
                Font = FontRepository.FindFont("Helvetica"),
                FontSize = 12,
                // LineSpacing corresponds to the leading between lines
                LineSpacing = 20 // adjust this value as needed
            };

            // Append lines using the TextState so the custom leading is applied
            paragraph.AppendLine("First line of text", textState);
            paragraph.AppendLine("Second line of text", textState);
            paragraph.AppendLine("Third line of text", textState);

            // Render the paragraph onto the page
            TextBuilder builder = new TextBuilder(page);
            builder.AppendParagraph(paragraph);

            // Save the PDF
            doc.Save("output.pdf");
        }

        Console.WriteLine("PDF created with custom line spacing.");
    }
}