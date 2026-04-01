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

        using (Document doc = new Document(inputPath))
        {
            // Verify that the document has at least three pages
            if (doc.Pages.Count < 3)
            {
                Console.Error.WriteLine("Document contains fewer than 3 pages.");
                return;
            }

            Page page = doc.Pages[3];

            // Create a multi‑line text paragraph
            TextParagraph paragraph = new TextParagraph();

            // Position the paragraph at the left margin (50 points from left)
            paragraph.Rectangle = new Aspose.Pdf.Rectangle(50, 700, 250, 500);

            // Enable word wrapping (optional)
            paragraph.FormattingOptions.WrapMode = TextFormattingOptions.WordWrapMode.ByWords;

            // Append lines with custom additional spacing (in points)
            paragraph.AppendLine("First line of text", 5.0f);
            paragraph.AppendLine("Second line with more spacing", 10.0f);
            paragraph.AppendLine("Third line", 5.0f);

            // Add the paragraph to the page
            TextBuilder textBuilder = new TextBuilder(page);
            textBuilder.AppendParagraph(paragraph);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Multi‑line text added to page 3 and saved as '{outputPath}'.");
    }
}