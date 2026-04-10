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

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Access the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Create a TextParagraph object
            TextParagraph paragraph = new TextParagraph();

            // Define the rectangle where the paragraph will be placed
            paragraph.Rectangle = new Aspose.Pdf.Rectangle(100, 600, 500, 800);

            // Set indentation: first line indent = 20 points, subsequent lines indent = 10 points
            paragraph.FirstLineIndent = 20;
            paragraph.SubsequentLinesIndent = 10;

            // Append lines with additional line spacing (5 points after each line)
            paragraph.AppendLine("Lorem ipsum dolor sit amet, consectetur adipiscing elit.", 5);
            paragraph.AppendLine("Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.", 5);
            paragraph.AppendLine("Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris.", 5);

            // Append the paragraph to the page using TextBuilder
            TextBuilder builder = new TextBuilder(page);
            builder.AppendParagraph(paragraph);

            // Save the modified PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Paragraph with indentation saved to '{outputPath}'.");
    }
}