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

        // Load the PDF document (lifecycle rule: using block)
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least three pages (1‑based indexing)
            if (doc.Pages.Count < 3)
            {
                Console.Error.WriteLine("The document contains fewer than 3 pages.");
                return;
            }

            // Get the third page
            Page page = doc.Pages[3];

            // Create a TextParagraph
            TextParagraph paragraph = new TextParagraph();

            // Set the rectangle where the paragraph will be drawn (fully qualified to avoid ambiguity)
            paragraph.Rectangle = new Aspose.Pdf.Rectangle(100, 500, 400, 700);

            // Rotate the paragraph by 45 degrees
            paragraph.Rotation = 45;

            // Define first text style (bold, red, size 20)
            TextState style1 = new TextState
            {
                Font = FontRepository.FindFont("Helvetica-Bold"),
                FontSize = 20,
                ForegroundColor = Aspose.Pdf.Color.Red
            };

            // Define second text style (italic, blue, size 16)
            TextState style2 = new TextState
            {
                Font = FontRepository.FindFont("Helvetica-Oblique"),
                FontSize = 16,
                ForegroundColor = Aspose.Pdf.Color.Blue
            };

            // Append lines with mixed styles
            paragraph.AppendLine("First line – bold red", style1);
            paragraph.AppendLine("Second line – italic blue", style2);
            paragraph.AppendLine("Third line – default style"); // uses default formatting

            // Append the paragraph to the page using TextBuilder
            TextBuilder builder = new TextBuilder(page);
            builder.AppendParagraph(paragraph);

            // Save the modified PDF (lifecycle rule: using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rotated mixed‑style paragraph added to page 3 and saved as '{outputPath}'.");
    }
}