using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;   // TextBuilder, TextParagraph, Rectangle

class Program
{
    static void Main()
    {
        // Output file paths
        const string outputPdfA = "output_pdfa1b.pdf";
        const string conversionLog = "conversion_log.xml";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page (first page)
            Page page = doc.Pages.Add();

            // Create a text paragraph
            TextParagraph paragraph = new TextParagraph();

            // Define the rectangle where the paragraph will be placed
            // (llx, lly, urx, ury) in points (1 inch = 72 points)
            paragraph.Rectangle = new Rectangle(100, 700, 500, 800);

            // Optional: enable word wrapping
            paragraph.FormattingOptions.WrapMode = TextFormattingOptions.WordWrapMode.ByWords;

            // Add a line of text
            paragraph.AppendLine("Hello, PDF/A‑1b world!");

            // Append the paragraph to the page using TextBuilder
            TextBuilder builder = new TextBuilder(page);
            builder.AppendParagraph(paragraph);

            // Convert the document to PDF/A‑1b (PDF/A-1b) compliance
            // The conversion writes a log file; you can ignore it if not needed
            doc.Convert(conversionLog, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);

            // Save the PDF/A‑1b compliant document
            doc.Save(outputPdfA);
        }

        Console.WriteLine($"PDF/A‑1b document saved to '{outputPdfA}'.");
    }
}