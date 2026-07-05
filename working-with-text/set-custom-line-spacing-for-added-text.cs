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

        // Load the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // Define a TextState with custom line spacing (leading)
            TextState textState = new TextState
            {
                Font = FontRepository.FindFont("Helvetica"),
                FontSize = 12,
                LineSpacing = 20f // desired leading in points
            };

            // Create a TextParagraph and set its bounding rectangle
            TextParagraph paragraph = new TextParagraph
            {
                Rectangle = new Aspose.Pdf.Rectangle(100, 600, 400, 800)
            };

            // Append lines using the TextState that carries the custom leading
            paragraph.AppendLine("First line with custom leading", textState);
            paragraph.AppendLine("Second line follows", textState);
            paragraph.AppendLine("Third line", textState);

            // Add the paragraph to the first page
            Page page = doc.Pages[1];
            TextBuilder builder = new TextBuilder(page);
            builder.AppendParagraph(paragraph);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}