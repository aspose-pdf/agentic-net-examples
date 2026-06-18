using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;          // Facade API
using Aspose.Pdf.Text;            // TextParagraph, TextBuilder, formatting options

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

        // Bind the existing PDF with the Facade, modify it, then save.
        using (PdfFileMend mend = new PdfFileMend())
        {
            // Load the source PDF.
            mend.BindPdf(inputPath);

            // Access the underlying Document.
            Document doc = mend.Document;

            // Ensure the document has at least three pages.
            if (doc.Pages.Count < 3)
            {
                Console.Error.WriteLine("The document does not contain a third page.");
                return;
            }

            // Get page three (1‑based indexing).
            Page page = doc.Pages[3];

            // Create a multi‑line text paragraph.
            TextParagraph paragraph = new TextParagraph();

            // Position the paragraph at the left margin.
            // Rectangle(left, bottom, right, top) – using page height to place near the top.
            double pageHeight = page.PageInfo.Height;
            paragraph.Rectangle = new Aspose.Pdf.Rectangle(0, pageHeight - 200, 200, pageHeight - 50);

            // Enable word wrapping (optional, based on needs).
            paragraph.FormattingOptions.WrapMode = TextFormattingOptions.WordWrapMode.ByWords;

            // Append lines with custom line spacing (additional spacing after each line).
            paragraph.AppendLine("First line of the block", 5f);   // 5 points extra spacing
            paragraph.AppendLine("Second line with more space", 10f);
            paragraph.AppendLine("Third line", 5f);

            // Append the paragraph to the page using TextBuilder.
            TextBuilder builder = new TextBuilder(page);
            builder.AppendParagraph(paragraph);

            // Save the modified PDF via the Facade.
            mend.Save(outputPath);
        }

        Console.WriteLine($"Multi‑line text added to page 3 and saved as '{outputPath}'.");
    }
}