using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Create a TextParagraph object
            TextParagraph paragraph = new TextParagraph();

            // Define the rectangle where the paragraph will be placed
            // Fully qualify to avoid ambiguity with System.Drawing.Rectangle
            paragraph.Rectangle = new Aspose.Pdf.Rectangle(100, 500, 500, 700);

            // Set indentation values (in points)
            paragraph.FirstLineIndent      = 20; // indent for the first line
            paragraph.SubsequentLinesIndent = 10; // indent for subsequent lines

            // Optional: enable word wrapping
            paragraph.FormattingOptions.WrapMode = TextFormattingOptions.WordWrapMode.ByWords;

            // Append lines with custom line spacing (additional spacing after each line)
            // The second argument is the extra spacing in points
            paragraph.AppendLine("Lorem ipsum dolor sit amet, consectetur adipiscing elit.", 5);
            paragraph.AppendLine("Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.", 5);
            paragraph.AppendLine("Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris.", 5);
            paragraph.AppendLine("Nisi ut aliquip ex ea commodo consequat.", 5);

            // Use TextBuilder to add the paragraph to the page
            TextBuilder builder = new TextBuilder(page);
            builder.AppendParagraph(paragraph);

            // Save the modified PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with formatted paragraph: {outputPath}");
    }
}