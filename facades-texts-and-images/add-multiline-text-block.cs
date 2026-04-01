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
            if (doc.Pages.Count < 3)
            {
                Console.Error.WriteLine("The document has fewer than 3 pages.");
                return;
            }

            Page page = doc.Pages[3];

            TextParagraph paragraph = new TextParagraph();
            // Position the paragraph at the left margin (0‑150 points width, full page height)
            paragraph.Rectangle = new Aspose.Pdf.Rectangle(0, 0, 150, page.PageInfo.Height);
            // Enable word wrapping
            paragraph.FormattingOptions.WrapMode = TextFormattingOptions.WordWrapMode.ByWords;

            // Append lines with custom additional line spacing (5 points)
            paragraph.AppendLine("First line of text", 5.0f);
            paragraph.AppendLine("Second line of text", 5.0f);
            paragraph.AppendLine("Third line of text", 5.0f);

            TextBuilder textBuilder = new TextBuilder(page);
            textBuilder.AppendParagraph(paragraph);

            doc.Save(outputPath);
        }

        Console.WriteLine($"Multi-line text added to page 3 and saved as '{outputPath}'.");
    }
}