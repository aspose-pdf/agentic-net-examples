using System;
using System.IO;
using Aspose.Pdf;

class PdfToMarkdownConverter
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputMdPath = "output.md";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Configure Markdown save options with custom heading mapping
            var mdOptions = new MarkdownSaveOptions
            {
                // Use heuristic heading recognition based on font size
                HeadingRecognitionStrategy = HeadingRecognitionStrategy.Heuristic,
                // Custom heading level mapping – adjust the threshold as needed
                HeadingLevels = new HeadingLevels(threshold: 0.02)
            };

            // Save the document as Markdown using the configured options
            pdfDoc.Save(outputMdPath, mdOptions);
        }

        Console.WriteLine($"PDF successfully converted to Markdown: {outputMdPath}");
    }
}