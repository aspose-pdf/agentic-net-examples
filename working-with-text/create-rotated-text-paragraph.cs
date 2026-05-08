using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";

        // Document lifecycle must be wrapped in a using block for proper disposal
        using (Document doc = new Document())
        {
            // Add a new page to the document
            Page page = doc.Pages.Add();

            // Create a TextParagraph instance
            TextParagraph paragraph = new TextParagraph();

            // Define the rectangle where the paragraph will be placed
            paragraph.Rectangle = new Aspose.Pdf.Rectangle(100, 500, 400, 700);

            // Apply a 30-degree rotation to the entire paragraph
            paragraph.Rotation = 30;

            // First text fragment
            TextFragment frag1 = new TextFragment("First fragment");
            frag1.TextState.FontSize = 12;
            frag1.TextState.Font = FontRepository.FindFont("Helvetica");
            frag1.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;

            // Second text fragment
            TextFragment frag2 = new TextFragment("Second fragment");
            frag2.TextState.FontSize = 12;
            frag2.TextState.Font = FontRepository.FindFont("Helvetica");
            frag2.TextState.ForegroundColor = Aspose.Pdf.Color.Green;

            // Append the fragments as separate lines in the paragraph
            paragraph.AppendLine(frag1);
            paragraph.AppendLine(frag2);

            // Use TextBuilder to add the paragraph to the page
            TextBuilder builder = new TextBuilder(page);
            builder.AppendParagraph(paragraph);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}