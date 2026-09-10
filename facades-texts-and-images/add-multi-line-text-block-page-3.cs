using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
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

        // Use PdfFileMend (a Facades class) to bind the existing PDF,
        // modify it with core API objects, and save the result.
        using (PdfFileMend mend = new PdfFileMend())
        {
            // Bind the source PDF document.
            mend.BindPdf(inputPath);

            // Access the underlying Document object.
            Document doc = mend.Document;

            // Page indexing in Aspose.Pdf is 1‑based.
            Page page3 = doc.Pages[3];

            // Create a multi‑line text paragraph.
            TextParagraph paragraph = new TextParagraph();

            // Position the paragraph at the left margin.
            // Rectangle(left, bottom, right, top)
            paragraph.Rectangle = new Aspose.Pdf.Rectangle(
                0,                                 // left (margin edge)
                0,                                 // bottom
                200,                               // right (width of the text block)
                page3.PageInfo.Height);           // top (full page height)

            // Enable word wrapping (optional, based on needs).
            paragraph.FormattingOptions.WrapMode = TextFormattingOptions.WordWrapMode.ByWords;

            // Append lines with custom line spacing (additional spacing after each line).
            paragraph.AppendLine("First line of text", 5f);   // 5 points extra spacing
            paragraph.AppendLine("Second line of text", 10f); // 10 points extra spacing
            paragraph.AppendLine("Third line of text", 0f);   // default spacing

            // Append the paragraph to page three.
            TextBuilder builder = new TextBuilder(page3);
            builder.AppendParagraph(paragraph);

            // Save the modified PDF using the facade.
            mend.Save(outputPath);
        }

        Console.WriteLine($"Multi‑line text added to page 3 and saved as '{outputPath}'.");
    }
}