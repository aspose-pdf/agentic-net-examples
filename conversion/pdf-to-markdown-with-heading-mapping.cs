using System;
using System.IO;
using Aspose.Pdf;

class PdfToMarkdownConverter
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputMd = "output.md";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Configure Markdown conversion options. All related types are in the Aspose.Pdf namespace.
        var mdOptions = new MarkdownSaveOptions
        {
            HeadingRecognitionStrategy = HeadingRecognitionStrategy.Heuristic,
            HeadingLevels = new HeadingLevels()
        };

        using (var pdfDoc = new Document(inputPdf))
        {
            pdfDoc.Save(outputMd, mdOptions);
        }

        Console.WriteLine($"PDF successfully converted to Markdown: {outputMd}");
    }
}
