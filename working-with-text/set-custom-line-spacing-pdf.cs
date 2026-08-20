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

        // Load the source PDF inside a using block (lifecycle rule)
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Create a TextParagraph that will hold the new lines
            TextParagraph paragraph = new TextParagraph();

            // Define the rectangle where the paragraph will be placed
            paragraph.Rectangle = new Rectangle(100, 600, 400, 800);

            // Optional: enable word wrapping
            paragraph.FormattingOptions.WrapMode = TextFormattingOptions.WordWrapMode.ByWords;

            // Create a TextState and set the desired line spacing (Leading)
            // In Aspose.Pdf the property controlling line spacing is LineSpacing.
            // Setting it influences the leading used when the TextState is applied.
            TextState textState = new TextState();
            textState.Font = FontRepository.FindFont("Helvetica");
            textState.FontSize = 12;
            textState.LineSpacing = 20f; // Desired leading (extra space between lines)

            // Append lines using the TextState with custom spacing
            paragraph.AppendLine("First line with custom leading.", textState);
            paragraph.AppendLine("Second line follows the same leading.", textState);
            paragraph.AppendLine("Third line continues the spacing.", textState);

            // Add the paragraph to the page using TextBuilder
            TextBuilder builder = new TextBuilder(page);
            builder.AppendParagraph(paragraph);

            // Save the modified PDF (lifecycle rule)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}