using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "output_pdfa1b.pdf";
        const string logPath = "conversion_log.xml";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page (Aspose.Pdf uses 1‑based page indexing)
            doc.Pages.Add();

            // Get the first page
            Page page = doc.Pages[1];

            // Create a text paragraph
            TextParagraph paragraph = new TextParagraph();

            // Define the rectangle where the paragraph will be placed
            paragraph.Rectangle = new Rectangle(100, 600, 500, 700);

            // Enable word wrapping by words
            paragraph.FormattingOptions.WrapMode = TextFormattingOptions.WordWrapMode.ByWords;

            // Add lines of text to the paragraph
            paragraph.AppendLine("The quick brown fox jumps over the lazy dog.");
            paragraph.AppendLine("Second line of the paragraph.");

            // Append the paragraph to the page using TextBuilder
            TextBuilder builder = new TextBuilder(page);
            builder.AppendParagraph(paragraph);

            // Convert the document to PDF/A‑1b (PDF/A‑1b compliance)
            doc.Convert(logPath, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);

            // Save the PDF/A‑1b document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF/A‑1b file saved to '{outputPath}'.");
    }
}