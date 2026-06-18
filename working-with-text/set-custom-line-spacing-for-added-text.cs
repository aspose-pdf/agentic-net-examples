using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing;

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
            // Work with the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Create a TextState and set the desired line spacing (leading)
            TextState textState = new TextState();
            textState.Font = FontRepository.FindFont("Helvetica");
            textState.FontSize = 12;
            textState.LineSpacing = 15; // leading in points

            // Create a TextParagraph and define its bounding rectangle
            TextParagraph paragraph = new TextParagraph();
            paragraph.Rectangle = new Aspose.Pdf.Rectangle(100, 600, 300, 800);
            paragraph.FormattingOptions.WrapMode = TextFormattingOptions.WordWrapMode.ByWords;

            // Append lines using the TextState with custom leading
            paragraph.AppendLine("First line with custom leading.", textState);
            paragraph.AppendLine("Second line follows the same leading.", textState);
            paragraph.AppendLine("Third line.", textState);

            // Add the paragraph to the page
            TextBuilder builder = new TextBuilder(page);
            builder.AppendParagraph(paragraph);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with modified line spacing to '{outputPath}'.");
    }
}