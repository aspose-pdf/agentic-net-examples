using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least three pages
            if (doc.Pages.Count < 3)
            {
                Console.Error.WriteLine("The document must contain at least three pages.");
                return;
            }

            // Get the third page (1‑based indexing)
            Page page = doc.Pages[3];

            // Create a TextParagraph
            TextParagraph paragraph = new TextParagraph();

            // Define the rectangle where the paragraph will be placed
            paragraph.Rectangle = new Aspose.Pdf.Rectangle(100, 500, 400, 700);

            // Rotate the paragraph by 45 degrees
            paragraph.Rotation = 45;

            // Create TextState objects for different styles
            TextState boldState = new TextState
            {
                Font = FontRepository.FindFont("Helvetica-Bold"),
                FontSize = 14,
                ForegroundColor = Aspose.Pdf.Color.Blue
            };

            TextState italicState = new TextState
            {
                Font = FontRepository.FindFont("Helvetica-Oblique"),
                FontSize = 12,
                ForegroundColor = Aspose.Pdf.Color.Green
            };

            // Append lines with mixed styles
            paragraph.AppendLine("This line is bold and blue.", boldState);
            paragraph.AppendLine("This line is italic and green.", italicState);
            paragraph.AppendLine("Normal line with default style.");

            // Append the paragraph to the page using TextBuilder
            TextBuilder builder = new TextBuilder(page);
            builder.AppendParagraph(paragraph);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}