using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";

        // Create a new PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document())
        {
            // Add a page first – a newly created Document has no pages in the collection.
            // Evaluation mode limit: maximum 4 pages; we add only one.
            Page page = doc.Pages.Add();

            // Create a TextParagraph
            TextParagraph paragraph = new TextParagraph
            {
                // Define the rectangle where the paragraph will be placed
                Rectangle = new Aspose.Pdf.Rectangle(100, 500, 400, 700),
                // Apply a 30‑degree rotation to the whole paragraph
                Rotation = 30
            };

            // First TextFragment
            TextFragment fragment1 = new TextFragment("First fragment")
            {
                TextState =
                {
                    FontSize = 12,
                    Font = FontRepository.FindFont("Helvetica"),
                    ForegroundColor = Aspose.Pdf.Color.Blue
                }
            };

            // Second TextFragment
            TextFragment fragment2 = new TextFragment("Second fragment")
            {
                TextState =
                {
                    FontSize = 12,
                    Font = FontRepository.FindFont("Helvetica"),
                    ForegroundColor = Aspose.Pdf.Color.Green
                }
            };

            // Append the fragments to the paragraph as separate lines
            paragraph.AppendLine(fragment1);
            paragraph.AppendLine(fragment2);

            // Add the paragraph to the page using TextBuilder
            TextBuilder textBuilder = new TextBuilder(page);
            textBuilder.AppendParagraph(paragraph);

            // Save the document (lifecycle rule: save inside using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
