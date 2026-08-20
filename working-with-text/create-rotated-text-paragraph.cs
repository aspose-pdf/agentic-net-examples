using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Output PDF path
        const string outputPath = "RotatedParagraph.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page before accessing it (required for evaluation mode & safety)
            Page page = doc.Pages.Add();

            // Create a TextParagraph
            TextParagraph paragraph = new TextParagraph();

            // Define the rectangle where the paragraph will be placed (fully qualified to avoid ambiguity)
            paragraph.Rectangle = new Aspose.Pdf.Rectangle(100, 500, 400, 700);

            // Apply a 30-degree rotation to the entire paragraph
            paragraph.Rotation = 30; // degrees

            // First TextFragment
            TextFragment fragment1 = new TextFragment("First line of text");
            fragment1.TextState.FontSize = 12;
            fragment1.TextState.Font = FontRepository.FindFont("Helvetica");
            fragment1.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;

            // Second TextFragment
            TextFragment fragment2 = new TextFragment("Second line of text");
            fragment2.TextState.FontSize = 12;
            fragment2.TextState.Font = FontRepository.FindFont("Helvetica");
            fragment2.TextState.ForegroundColor = Aspose.Pdf.Color.Green;

            // Append the TextFragments as separate lines to the paragraph
            paragraph.AppendLine(fragment1);
            paragraph.AppendLine(fragment2);

            // Use TextBuilder to add the paragraph to the page
            TextBuilder textBuilder = new TextBuilder(page);
            textBuilder.AppendParagraph(paragraph);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
