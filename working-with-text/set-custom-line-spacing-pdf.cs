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
            // Add a blank page
            Page page = doc.Pages.Add();

            // Create a TextParagraph that will hold multiple lines
            TextParagraph paragraph = new TextParagraph();

            // Define the rectangle area where the paragraph will be placed
            paragraph.Rectangle = new Aspose.Pdf.Rectangle(50, 700, 550, 100);

            // Configure a TextState with desired font, size, and line spacing (leading)
            TextState textState = new TextState();
            textState.Font = FontRepository.FindFont("Helvetica");
            textState.FontSize = 12;
            textState.LineSpacing = 20; // leading of 20 points

            // Append lines using the same TextState to apply the custom leading
            paragraph.AppendLine("First line with custom leading.", textState);
            paragraph.AppendLine("Second line with the same leading.", textState);
            paragraph.AppendLine("Third line continues the spacing.", textState);

            // Render the paragraph onto the page
            TextBuilder builder = new TextBuilder(page);
            builder.AppendParagraph(paragraph);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}