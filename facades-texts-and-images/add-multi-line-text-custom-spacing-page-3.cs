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
        using (PdfFileMend mend = new PdfFileMend())
        {
            mend.BindPdf(inputPath);               // initialize facade with source PDF
            Document doc = mend.Document;          // underlying Document object

            // Verify that page 3 exists (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count < 3)
            {
                Console.Error.WriteLine("The document has fewer than 3 pages.");
                return;
            }

            Page page = doc.Pages[3];              // target page

            // Create a multi‑line text paragraph
            TextParagraph paragraph = new TextParagraph();

            // Position the paragraph at the left margin (adjust coordinates as needed)
            paragraph.Rectangle = new Aspose.Pdf.Rectangle(0, 500, 150, 700);

            // Optional: enable word‑wrap
            paragraph.FormattingOptions.WrapMode = TextFormattingOptions.WordWrapMode.ByWords;

            // Append lines with custom additional line spacing (in points)
            paragraph.AppendLine("First line of text", 5f);
            paragraph.AppendLine("Second line with extra spacing", 10f);
            paragraph.AppendLine("Third line", 5f);

            // Add the paragraph to the page
            TextBuilder builder = new TextBuilder(page);
            builder.AppendParagraph(paragraph);

            // Save the modified PDF via the facade
            mend.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}