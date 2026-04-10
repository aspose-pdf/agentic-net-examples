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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Initialize PdfFileMend facade and bind it to the document
            PdfFileMend mend = new PdfFileMend();
            mend.BindPdf(doc);

            // Configure word‑wrap settings: enable wrapping and use ByWords mode
            mend.IsWordWrap = true;
            mend.WrapMode = Aspose.Pdf.Facades.WordWrapMode.ByWords;

            // Create a text paragraph that exceeds the rectangle width
            TextParagraph paragraph = new TextParagraph();

            // Define the rectangle where the text will be placed (left, bottom, right, top)
            paragraph.Rectangle = new Aspose.Pdf.Rectangle(50, 700, 550, 750);

            // Set the paragraph's formatting options to wrap by words
            paragraph.FormattingOptions.WrapMode = Aspose.Pdf.Text.TextFormattingOptions.WordWrapMode.ByWords;

            // Append a long line of text that will need wrapping
            paragraph.AppendLine(
                "This is a very long line of text that will exceed the defined rectangle width and should be wrapped by words according to the configured wrap mode.");

            // Add the paragraph to the first page using TextBuilder
            Page page = doc.Pages[1];
            TextBuilder textBuilder = new TextBuilder(page);
            textBuilder.AppendParagraph(paragraph);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with word‑wrapped paragraph: {outputPath}");
    }
}