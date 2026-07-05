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

            // Create a TextParagraph instance
            TextParagraph paragraph = new TextParagraph();

            // Define the rectangle where the paragraph will be placed
            paragraph.Rectangle = new Aspose.Pdf.Rectangle(100, 500, 400, 700);

            // Apply a 30-degree rotation to the entire paragraph
            paragraph.Rotation = 30;

            // First text fragment
            TextFragment tf1 = new TextFragment("First fragment");
            tf1.TextState.FontSize = 12;
            tf1.TextState.Font = FontRepository.FindFont("Helvetica");
            tf1.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;

            // Second text fragment
            TextFragment tf2 = new TextFragment("Second fragment");
            tf2.TextState.FontSize = 12;
            tf2.TextState.Font = FontRepository.FindFont("Helvetica");
            tf2.TextState.ForegroundColor = Aspose.Pdf.Color.Green;

            // Append the fragments to the paragraph as separate lines
            paragraph.AppendLine(tf1);
            paragraph.AppendLine(tf2);

            // Use TextBuilder to add the paragraph to the page
            TextBuilder builder = new TextBuilder(page);
            builder.AppendParagraph(paragraph);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}