using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "output_pdfa.pdf";
        const string logPath    = "conversion.log";

        // Create a new PDF document. Add a page before accessing it (evaluation mode may start with zero pages).
        using (Document doc = new Document())
        {
            // Add a page explicitly to avoid IndexOutOfRangeException.
            Page page = doc.Pages.Add();

            // Create a text paragraph.
            TextParagraph paragraph = new TextParagraph();

            // Define the rectangle where the paragraph will be placed (fully qualified to avoid ambiguity).
            paragraph.Rectangle = new Aspose.Pdf.Rectangle(100, 600, 500, 700);

            // Enable word wrapping by words.
            paragraph.FormattingOptions.WrapMode = TextFormattingOptions.WordWrapMode.ByWords;

            // Add lines of text to the paragraph.
            paragraph.AppendLine("Hello, Aspose.Pdf!");
            paragraph.AppendLine("This PDF is saved as PDF/A‑1b.");

            // Append the paragraph to the page using TextBuilder.
            TextBuilder builder = new TextBuilder(page);
            builder.AppendParagraph(paragraph);

            // Convert the document to PDF/A‑1b compliance. The log file records any conversion issues.
            doc.Convert(logPath, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);

            // Save the PDF/A‑1b document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF/A‑1b file saved to '{outputPath}'.");
    }
}
