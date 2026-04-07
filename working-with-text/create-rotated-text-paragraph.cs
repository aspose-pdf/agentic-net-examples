using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Create a TextParagraph instance
            TextParagraph paragraph = new TextParagraph();

            // Define the rectangle area where the paragraph will be placed
            paragraph.Rectangle = new Aspose.Pdf.Rectangle(100, 500, 400, 700);

            // Apply a 30-degree rotation to the entire paragraph
            paragraph.Rotation = 30;

            // First text fragment
            TextFragment fragment1 = new TextFragment("First fragment");
            fragment1.TextState.FontSize = 12;
            fragment1.TextState.Font = FontRepository.FindFont("Helvetica");
            fragment1.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;

            // Second text fragment
            TextFragment fragment2 = new TextFragment("Second fragment");
            fragment2.TextState.FontSize = 12;
            fragment2.TextState.Font = FontRepository.FindFont("Helvetica");
            fragment2.TextState.ForegroundColor = Aspose.Pdf.Color.Green;

            // Append the fragments to the paragraph as separate lines
            paragraph.AppendLine(fragment1);
            paragraph.AppendLine(fragment2);

            // Use TextBuilder to add the paragraph to the page
            TextBuilder builder = new TextBuilder(page);
            builder.AppendParagraph(paragraph);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}