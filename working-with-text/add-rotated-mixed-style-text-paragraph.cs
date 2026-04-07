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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least three pages
            while (doc.Pages.Count < 3)
                doc.Pages.Add();

            // Get the third page (1‑based indexing)
            Page page = doc.Pages[3];

            // Create a TextParagraph
            TextParagraph paragraph = new TextParagraph();

            // Define the rectangle where the paragraph will be placed
            paragraph.Rectangle = new Aspose.Pdf.Rectangle(100, 500, 400, 700);

            // Rotate the paragraph by 45 degrees
            paragraph.Rotation = 45;

            // First line – bold font
            TextState boldState = new TextState
            {
                Font = FontRepository.FindFont("Helvetica-Bold"),
                FontSize = 14,
                ForegroundColor = Aspose.Pdf.Color.Blue
            };
            paragraph.AppendLine("This is a bold line.", boldState);

            // Second line – italic font
            TextState italicState = new TextState
            {
                Font = FontRepository.FindFont("Helvetica-Oblique"),
                FontSize = 14,
                ForegroundColor = Aspose.Pdf.Color.Green
            };
            paragraph.AppendLine("This line is italic.", italicState);

            // Third line – regular font with different color
            TextState regularState = new TextState
            {
                Font = FontRepository.FindFont("Helvetica"),
                FontSize = 14,
                ForegroundColor = Aspose.Pdf.Color.Red
            };
            paragraph.AppendLine("Regular style line.", regularState);

            // Append the paragraph to the page using TextBuilder
            TextBuilder builder = new TextBuilder(page);
            builder.AppendParagraph(paragraph);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with rotated mixed‑style paragraph on page 3: {outputPath}");
    }
}