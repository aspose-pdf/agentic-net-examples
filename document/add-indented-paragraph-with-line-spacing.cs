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

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Create a TextParagraph object
            TextParagraph paragraph = new TextParagraph();

            // Define the rectangle where the paragraph will be placed
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            paragraph.Rectangle = new Aspose.Pdf.Rectangle(100, 600, 500, 800);

            // Set indentation values (in points)
            paragraph.FirstLineIndent      = 30; // indent for the first line
            paragraph.SubsequentLinesIndent = 15; // indent for subsequent lines

            // Optional: set word‑wrap mode
            paragraph.FormattingOptions.WrapMode = TextFormattingOptions.WordWrapMode.ByWords;

            // Append lines with custom line spacing (additional spacing in points)
            paragraph.AppendLine("Lorem ipsum dolor sit amet, consectetur adipiscing elit.", 5); // 5pt extra spacing after this line
            paragraph.AppendLine("Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.", 5);
            paragraph.AppendLine("Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris.", 5);
            paragraph.AppendLine("Nisi ut aliquip ex ea commodo consequat.", 5);

            // Append the paragraph to the page using TextBuilder
            TextBuilder textBuilder = new TextBuilder(page);
            textBuilder.AppendParagraph(paragraph);

            // Save the modified PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with formatted paragraph to '{outputPath}'.");
    }
}