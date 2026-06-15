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

        // Load the PDF document (1‑based page indexing)
        using (Document doc = new Document(inputPath))
        {
            Page page = doc.Pages[1];

            // Create a text paragraph
            TextParagraph paragraph = new TextParagraph();

            // Define the rectangle where the paragraph will be placed
            paragraph.Rectangle = new Aspose.Pdf.Rectangle(50, 500, 550, 750);

            // Set indentation values (points)
            paragraph.FirstLineIndent = 20;          // indent first line
            paragraph.SubsequentLinesIndent = 10;   // indent subsequent lines

            // Enable word wrapping
            paragraph.FormattingOptions.WrapMode = TextFormattingOptions.WordWrapMode.ByWords;

            // Append lines; the second parameter adds extra line spacing after the line
            paragraph.AppendLine("Lorem ipsum dolor sit amet, consectetur adipiscing elit.", 5);
            paragraph.AppendLine("Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.");
            paragraph.AppendLine("Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris.");

            // Append the paragraph to the page
            TextBuilder builder = new TextBuilder(page);
            builder.AppendParagraph(paragraph);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}