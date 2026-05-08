using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";   // source PDF
        const string outputPath = "output.pdf";  // result PDF

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF and ensure deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least three pages (1‑based indexing)
            while (doc.Pages.Count < 3)
                doc.Pages.Add();

            // Get the third page
            Page page = doc.Pages[3];

            // Create a TextParagraph
            TextParagraph paragraph = new TextParagraph();

            // Define the rectangle where the paragraph will be placed
            paragraph.Rectangle = new Aspose.Pdf.Rectangle(100, 500, 400, 700);

            // Rotate the paragraph (degrees)
            paragraph.Rotation = 45;

            // Append a normal line
            paragraph.AppendLine("Normal text line");

            // Append a bold line with its own TextState
            TextState boldState = new TextState
            {
                Font = FontRepository.FindFont("Helvetica-Bold"),
                FontSize = 12,
                ForegroundColor = Aspose.Pdf.Color.Blue
            };
            paragraph.AppendLine("Bold text line", boldState);

            // Append an italic line with its own TextState
            TextState italicState = new TextState
            {
                Font = FontRepository.FindFont("Helvetica-Oblique"),
                FontSize = 12,
                ForegroundColor = Aspose.Pdf.Color.Green
            };
            paragraph.AppendLine("Italic text line", italicState);

            // Use TextBuilder to add the paragraph to the page
            TextBuilder builder = new TextBuilder(page);
            builder.AppendParagraph(paragraph);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}