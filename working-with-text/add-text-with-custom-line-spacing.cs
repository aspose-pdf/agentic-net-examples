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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a TextState with desired font and line spacing (leading)
            TextState textState = new TextState
            {
                Font = FontRepository.FindFont("Helvetica"),
                FontSize = 12,
                LineSpacing = 8f   // extra spacing added after each line
            };

            // Create a TextParagraph and define its placement rectangle
            TextParagraph paragraph = new TextParagraph
            {
                Rectangle = new Rectangle(100, 600, 300, 800) // llx, lly, urx, ury
            };

            // Append lines using the same TextState (line spacing applied)
            paragraph.AppendLine("First line of text", textState);
            paragraph.AppendLine("Second line of text", textState);
            paragraph.AppendLine("Third line of text", textState);

            // Add the paragraph to the first page using TextBuilder
            Page page = doc.Pages[1];
            TextBuilder builder = new TextBuilder(page);
            builder.AppendParagraph(paragraph);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with custom line spacing to '{outputPath}'.");
    }
}