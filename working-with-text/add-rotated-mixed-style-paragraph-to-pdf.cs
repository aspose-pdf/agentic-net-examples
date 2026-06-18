using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least three pages
            if (doc.Pages.Count < 3)
            {
                Console.Error.WriteLine("The document contains fewer than 3 pages.");
                return;
            }

            // Get the third page (1‑based indexing)
            Page page = doc.Pages[3];

            // Create a TextParagraph
            TextParagraph paragraph = new TextParagraph();

            // Set the rectangle where the paragraph will be placed
            paragraph.Rectangle = new Aspose.Pdf.Rectangle(100, 500, 400, 700);

            // Rotate the paragraph by 45 degrees
            paragraph.Rotation = 45;

            // Define first text style (bold, red)
            TextState style1 = new TextState
            {
                Font = FontRepository.FindFont("Helvetica-Bold"),
                FontSize = 14,
                ForegroundColor = Aspose.Pdf.Color.Red
            };

            // Define second text style (italic, blue)
            TextState style2 = new TextState
            {
                Font = FontRepository.FindFont("Helvetica-Oblique"),
                FontSize = 12,
                ForegroundColor = Aspose.Pdf.Color.Blue
            };

            // Append lines with mixed styles
            paragraph.AppendLine("This is bold red text.", style1);
            paragraph.AppendLine("This is italic blue text.", style2);
            paragraph.AppendLine("This line uses the default style.");

            // Append the paragraph to the page using TextBuilder
            TextBuilder builder = new TextBuilder(page);
            builder.AppendParagraph(paragraph);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rotated mixed‑style paragraph added to page 3 and saved as '{outputPath}'.");
    }
}