using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
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

        // Load the PDF using the Facades API
        using (PdfFileMend pdfMend = new PdfFileMend())
        {
            pdfMend.BindPdf(inputPath);

            // Access the underlying Document
            Document doc = pdfMend.Document;

            // Verify that page 3 exists (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count < 3)
            {
                Console.Error.WriteLine("The document has fewer than 3 pages.");
                return;
            }

            Page page = doc.Pages[3];

            // Define a rectangle that occupies the left margin of the page
            // (0,0) is lower‑left; use the full page height for the rectangle
            double pageHeight = page.PageInfo.Height;
            TextParagraph paragraph = new TextParagraph
            {
                // Left margin width of 150 units
                Rectangle = new Aspose.Pdf.Rectangle(0, 0, 150, pageHeight)
            };

            // Enable word‑wrap within the rectangle
            paragraph.FormattingOptions.WrapMode = TextFormattingOptions.WordWrapMode.ByWords;

            // Append multiple lines with custom line spacing (extra spacing after each line)
            paragraph.AppendLine("First line of text", 5f);      // 5 units extra spacing
            paragraph.AppendLine("Second line with more spacing", 10f); // 10 units extra spacing
            paragraph.AppendLine("Third line", 0f);             // default spacing

            // Append the paragraph to page 3
            TextBuilder builder = new TextBuilder(page);
            builder.AppendParagraph(paragraph);

            // Save the modified PDF using the Facades API
            pdfMend.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}