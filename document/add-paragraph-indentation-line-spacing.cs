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

        // Load the existing PDF document if it exists; otherwise create a new one with a blank page.
        Document doc;
        if (File.Exists(inputPath))
        {
            doc = new Document(inputPath);
        }
        else
        {
            doc = new Document();
            // Add a blank page so we have a target for the paragraph.
            doc.Pages.Add();
        }

        // Ensure the document is disposed correctly.
        using (doc)
        {
            // Get the first page (pages are 1‑based)
            Page page = doc.Pages[1];

            // Create a new text paragraph
            TextParagraph paragraph = new TextParagraph();

            // Define the rectangle where the paragraph will be drawn.
            // Use the fully‑qualified Aspose.Pdf.Rectangle to avoid ambiguity with other Rectangle types.
            // (llx, lly, urx, ury) – left, bottom, right, top.
            paragraph.Rectangle = new Aspose.Pdf.Rectangle(100, 200, 400, 500);

            // Set indentation values (points)
            paragraph.FirstLineIndent      = 20; // indent for the first line
            paragraph.SubsequentLinesIndent = 10; // indent for all following lines

            // Append lines of text.
            // The overload with a float adds extra line spacing after the line.
            paragraph.AppendLine("Lorem ipsum dolor sit amet, consectetur adipiscing elit.", 5);
            paragraph.AppendLine("Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.");
            paragraph.AppendLine("Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris.", 10);

            // Append the paragraph to the page using TextBuilder
            TextBuilder textBuilder = new TextBuilder(page);
            textBuilder.AppendParagraph(paragraph);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Paragraph with indentation and line spacing saved to '{outputPath}'.");
    }
}
