using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputMd = "output.md";

        // Verify the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document (lifecycle rule: use Document constructor)
        using (Document doc = new Document(inputPdf))
        {
            // Configure Markdown save options
            var mdOptions = new MarkdownSaveOptions
            {
                // Use heuristic heading recognition based on font size
                HeadingRecognitionStrategy = HeadingRecognitionStrategy.Heuristic,
                // Define custom heading level mapping with a threshold
                HeadingLevels = new HeadingLevels(0.05)
            };

            // Save the document as Markdown using the configured options
            doc.Save(outputMd, mdOptions);
        }

        Console.WriteLine($"Markdown saved to '{outputMd}'.");
    }
}