using System;
using Aspose.Pdf;
using Aspose.Pdf.Text; // needed for TextBuilder, TextParagraph, Position, etc.

class Program
{
    static void Main()
    {
        const string outputPath = "output_pdfa1b.pdf";
        const string logPath    = "conversion_log.xml";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page (1‑based indexing)
            Page page = doc.Pages.Add();

            // Create a text paragraph
            TextParagraph paragraph = new TextParagraph();

            // Define the rectangle where the paragraph will be placed
            // Use fully qualified type to avoid ambiguity with System.Drawing.Rectangle
            paragraph.Rectangle = new Aspose.Pdf.Rectangle(100, 600, 500, 700);

            // Enable word wrapping (optional)
            paragraph.FormattingOptions.WrapMode = TextFormattingOptions.WordWrapMode.ByWords;

            // Add lines of text
            paragraph.AppendLine("The quick brown fox jumps over the lazy dog.");
            paragraph.AppendLine("Second line of the paragraph.");
            paragraph.AppendLine("Third line of the paragraph.");

            // Append the paragraph to the page using TextBuilder
            TextBuilder builder = new TextBuilder(page);
            builder.AppendParagraph(paragraph);

            // Convert the document to PDF/A‑1b (PDF/A‑1b compliance)
            // The Convert method writes conversion errors to the specified log file.
            doc.Convert(logPath, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);

            // Save the PDF/A‑1b document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF/A‑1b document saved to '{outputPath}'.");
    }
}
