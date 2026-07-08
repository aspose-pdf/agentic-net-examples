using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "output_pdfa1b.pdf";

        // Create a new PDF document (initially empty)
        using (Document doc = new Document())
        {
            // Add the first page (Aspose.Pdf does NOT create a default page for a new document)
            Page page = doc.Pages.Add();

            // Build a text paragraph
            TextParagraph paragraph = new TextParagraph();

            // Define the rectangle where the paragraph will be placed
            paragraph.Rectangle = new Aspose.Pdf.Rectangle(100, 600, 500, 700);

            // Enable word wrapping by words
            paragraph.FormattingOptions.WrapMode = TextFormattingOptions.WordWrapMode.ByWords;

            // Add lines of text to the paragraph
            paragraph.AppendLine("The quick brown fox jumps over the lazy dog.");
            paragraph.AppendLine("Second line of the paragraph.");

            // Append the paragraph to the page using TextBuilder
            TextBuilder builder = new TextBuilder(page);
            builder.AppendParagraph(paragraph);

            // Convert the document to PDF/A‑1b (PDF/A‑1b compliance)
            // Optional log file for conversion errors
            string logPath = "conversion_log.xml";
            doc.Convert(logPath, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);

            // Save the PDF/A‑1b document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF/A‑1b document saved to '{outputPath}'.");
    }
}
